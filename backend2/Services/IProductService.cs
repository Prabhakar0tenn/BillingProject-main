using backend2.Models;

namespace backend2.Services{
    public interface IProductService{

        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(string id);
        Task<bool> DeleteProductByIdAsync(string id);
        Task<Product> CreateProductAsync(Product product);
    }
}