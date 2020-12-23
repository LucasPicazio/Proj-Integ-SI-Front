using PSI_FRONT.Helpers;
using PSI_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpService _httpService;

        public CartService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<Cart>> GetUserCarts(int userId)
        {
            List<Cart> cartList = new List<Cart>();
            try
            {
                var response = await _httpService.Get<List<Cart>>($"api/cart/search/member/{userId}");
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

        public async Task<int> AddCartAsync(Cart ProductCart)
        {
            var response = await _httpService.Post<Cart, int>("api/cart/insert", ProductCart);
            if (response.Success)
            {
                return response.Response;
            }
            return -1;
        }

        public async Task<bool> RemoveCartAsync(int cartId)
        {
            try
            {
                var response = await _httpService.Post<int, bool>($"api/cart/{cartId}/remove", cartId);
                return response.Response;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveUserCarts(int userid)
        {
            try
            {
                var response = await _httpService.Post<int, bool>($"api/cart/member/{userid}/remove", userid);
                if (response.Success)
                {
                    return response.Response;
                }
                return response.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
