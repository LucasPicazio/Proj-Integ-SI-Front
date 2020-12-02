using PSI_FRONT.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using PSI_FRONT.Helpers;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;

namespace PSI_FRONT.Services
{
    public class UserService : AuthenticationStateProvider, IUserService
    {
        private readonly IJSRuntime js;
        private readonly HttpClient _httpClient;
        private readonly string TOKENKEY = "TOKENKEY";

        private AuthenticationState Anonymous =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public UserService(IJSRuntime js, HttpClient httpClient)
        {
            this.js = js;
            _httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);

            if (string.IsNullOrEmpty(token))
            {
                return Anonymous;
            }

            return BuildAuthenticationState(token);
        }

        public async Task<bool> Login(User user)
        {
            try
            {
                var authUser = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/members/login", authUser);
                if (response.IsSuccessStatusCode)
                {
                    await SetLoginAuthenticationAsync(JsonSerializer.Serialize(user, new JsonSerializerOptions { IgnoreNullValues = true }));
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task SetLoginAuthenticationAsync(string token)
        {
            await js.SetInLocalStorage(TOKENKEY, token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Logout()
        {
            await js.RemoveItem(TOKENKEY);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }


        public async Task<bool> AddUserAsync(User newUser)
        {
            var user = new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync($"api/members/save", user);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromToken(token), "jwt")));
        }

        private IEnumerable<Claim> ParseClaimsFromToken(string token)
        {
            var claims = new List<Claim>();
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, string>>(token);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value)));
            return claims;
        }
    }
}
