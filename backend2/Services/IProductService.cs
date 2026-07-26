using backend2.Models;
using backend2.DTOs;

namespace backend2.Services{
    public interface IProductService{

        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(string id);
        Task<bool> DeleteProductByIdAsync(string id);
        Task<Product> CreateProductAsync(ProductDto product);
        Task<bool> UpdateProductStockAsync(string id, int stock);
        Task<bool> UpdateProductPriceAsync(string id, decimal newPrice);

    }
}