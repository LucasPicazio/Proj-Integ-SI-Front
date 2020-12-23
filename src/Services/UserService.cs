using PSI_FRONT.Models;
using System;
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
        private readonly IHttpService _httpService;
        private readonly string TOKENKEY = "TOKENKEY";

        private AuthenticationState Anonymous =>
            new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public UserService(IJSRuntime js, IHttpService httpService)
        {
            this.js = js;
            _httpService = httpService;
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
                var response = await _httpService.Post<User, int>($"api/members/login", user);
                if (response.Success)
                {
                    var userDTO = new UserToken()
                    {
                        UserId = response.Response.ToString(),
                        Username = user.Username,
                        Password = user.Password
                    };
                    await SetLoginAuthenticationAsync(JsonSerializer.Serialize(userDTO, new JsonSerializerOptions { IgnoreNullValues = true }));
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                await Logout();
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
            _httpService.SetAuthorizationHeader(null);
            await js.RemoveItem(TOKENKEY);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }


        public async Task<bool> AddUserAsync(User newUser)
        {
            try
            {
                var response = await _httpService.Post<User>($"api/members/register", newUser);
                return response.Success;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<int> GetUserIdFromToken()
        {
            var token = await js.GetFromLocalStorage(TOKENKEY);
            if (!string.IsNullOrEmpty(token)) {
                var userTkn = JsonSerializer.Deserialize<UserToken>(token);
                return Convert.ToInt32(userTkn.UserId);
            }
            return -1;
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _httpService.SetAuthorizationHeader(new AuthenticationHeaderValue("bearer", token));
            var identityClaims = new ClaimsIdentity(ParseClaimsFromToken(token), "jwt");
            var principalClaims = new ClaimsPrincipal(identityClaims);
            return new AuthenticationState(principalClaims);

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
