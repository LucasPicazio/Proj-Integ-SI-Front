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
            var productResultList = new List<Product>();
            try
            {
                var response = await _httpService.Get<List<Product>>("api/product/search");
                if (response.Success)
                {
                    productResultList = response.Response;
                }
            }
            catch (Exception)
            {
                return productResultList;
            }
            return productResultList;
        }

        public async Task<List<Product>> SearchProduct(string searchItem)
        {
            var productResultList = new List<Product>();
            try
            {
                var response = await _httpService.Get<List<Product>>($"api/product/search/{searchItem}");
                if (response.Success)
                {
                    productResultList = response.Response;
                }
            }
            catch (Exception)
            {
                return productResultList;
            }
            return productResultList.OrderBy(p => p.ProductType).ThenBy(p => p.Price).ToList();
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

        public async Task<Product> GetProductById(int prodId)
        {
            var result = new Product();
            try
            {
                var response = await _httpService.Get<Product>($"api/product/search/id/{prodId}");
                if (response.Success)
                {
                    result = response.Response;
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

        public async Task<int> PostProduct(Product prod)
        {
            try
            {
                var response = await _httpService.Post<Product, int>("api/product/save", prod);
                return response.Response;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
