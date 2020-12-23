using PSI_FRONT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> SearchProduct(string searchItem);
        Task<Product> GetProductByName(string prodName);
        Task<int> PostProduct(Product prod);
        Task<Product> GetProductById(int prodId);
    }
}