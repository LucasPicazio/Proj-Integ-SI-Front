using PSI_FRONT.Models;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public interface IUserService
    {
        public Task SetLoginAuthenticationAsync(string token);
        public Task<bool> AddUserAsync(User newUser);
        Task Logout();
        Task<bool> Login(User user);
        Task<int> GetUserIdFromToken();
    }
}
