using PSI_FRONT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetUserTransactions(int userId);
        Task<List<int>> PostInsertUserCarts(List<Cart> userCarts);
    }
}
