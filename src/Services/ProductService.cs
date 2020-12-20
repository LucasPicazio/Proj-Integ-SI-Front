using PSI_FRONT.Helpers;
using PSI_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpService _httpService;

        public ProductService(IHttpService httpService)
        {
            _httpService = httpService;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            var response = await _httpService.Get<List<Product>>("api/product/search");
            if (response.Success)
            {
                return response.Response;
            }
            return null;
        }

        public async Task<List<Product>> SearchProduct(string searchItem)
        {
            var response = await _httpService.Get<List<Product>>($"api/product/search/{searchItem}");
            if (response.Success)
            {
                var productResultList = response.Response;
                return productResultList.OrderBy(p => p.ProductType).ThenBy(p => p.Price).ToList();
            }
            return null;
        }

        public async Task<Product> GetProductByName(string prodName)
        {
            var response = await _httpService.Get<List<Product>>($"api/product/search/{prodName}");
            if (response.Success)
            {
                var productResultList = response.Response;
                return productResultList.OrderBy(p => p.ProductType).ThenBy(p => p.Price).ToList().FirstOrDefault();
            }
            return null;
        }

        public async Task<bool> PostProduct(Product prod)
        {
            try
            {
                var response = await _httpService.Post("api/product/save", prod);
                return response.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
