using PSI_FRONT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public interface ICartService
    {
        Task<int> AddCartAsync(Cart ProductCart);
        Task<List<Cart>> GetUserCarts(int userId);
        Task<bool> RemoveCartAsync(int cartId);
        Task<bool> RemoveUserCarts(int userid);
    }
}
