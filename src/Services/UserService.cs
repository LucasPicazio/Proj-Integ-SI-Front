using PSI_FRONT.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> GetLoginAuthenticationAsync(User user)
        {
            try
            {
                var authUser = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/members/login", authUser);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddUserAsync(User newUser)
        {
            var user = new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");
            try { 
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

    }
}
