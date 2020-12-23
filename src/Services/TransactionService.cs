using PSI_FRONT.Helpers;
using PSI_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IHttpService _httpService;

        public TransactionService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Transaction>> GetUserTransactions(int userId)
        {
            List<Transaction> cartList = new List<Transaction>();
            try
            {
                var response = await _httpService.Get<List<Transaction>>($"api/transaction/search/member/{userId}");
                if (response.Success)
                {
                    cartList = response.Response;
                }
            }
            catch (Exception)
            {
                return cartList;
            }
            return cartList;
        }

        public async Task<List<int>> PostInsertUserCarts(List<Cart> userCarts)
        {
            List<int> result = new List<int>();

            try
            {
                var response = await _httpService.Post<List<Cart>, List<int>>($"api/transaction/insert", userCarts);
                if (response.Success)
                {
                    result = response.Response;
                }
            }
            catch(Exception)
            {
                return result;
            }
            return result;
        }

    }
}
