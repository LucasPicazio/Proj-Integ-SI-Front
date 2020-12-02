using PSI_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<Product>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync($"api/product/search");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Product>>(responseString); ;
            }
            return null;
        }

        public async Task<List<Product>> SearchProduct(string searchItem)
        {
            var response = await _httpClient.GetAsync($"api/product/search/{searchItem}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var resultList = JsonSerializer.Deserialize<List<Product>>(responseString); ;
                return resultList.OrderBy(p => p.ProductType).ThenBy(p => p.Price).ToList();
            }
            return null;
        }

        public async Task<Product> GetProduct(string prodName)
        {
            var response = await _httpClient.GetAsync($"api/product/search/{prodName}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var resultList = JsonSerializer.Deserialize<List<Product>>(responseString); ;
                return resultList.OrderBy(p => p.ProductType).ThenBy(p => p.Price).ToList().FirstOrDefault();
            }
            return null;
        }

        public async Task<bool> PostProduct(Product prod)
        {
            try
            {
                var productJson = new StringContent(JsonSerializer.Serialize(prod), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/product/save", productJson);

                if (response.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
