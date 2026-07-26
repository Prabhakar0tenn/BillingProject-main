using backend2.Models;
using MongoDB.Driver;
using backend2.Data;
using backend2.DTOs;

namespace backend2.Services{
    public class ProductService:IProductService{

        private readonly  MongoDBContext _context;

        public ProductService(MongoDBContext context){
            _context=context;
        }



        public async Task<List<Product>> GetAllProductsAsync(){

            return await _context.Products.Find(x=>true).ToListAsync();

        }

        public async Task<Product?> GetProductByIdAsync(string id){
            return await _context.Products.Find(x=> x.Id==id).FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteProductByIdAsync(string id){
            DeleteResult result= await _context.Products.DeleteOneAsync(x=>x.Id==id);

            return result.DeletedCount>0;
        }
       public async  Task<Product> CreateProductAsync(ProductDto productdto){

            var product=new Product{
                Name=productdto.name,
                Price=productdto.price,
                Stock=productdto.stock,
                CreatedAt=DateTime.UtcNow
            };
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> UpdateProductStockAsync(string id , int stock){

            if (stock <= 0)
                {
                    throw new InvalidOperationException("Stock must be greater than zero.");
                }

            var result=await _context.Products.UpdateOneAsync(x=>x.Id==id,new UpdateDefinitionBuilder<Product>().Inc(x=>x.Stock,stock));   
            return result.ModifiedCount>0;
        }   

        public async Task<bool> UpdateProductPriceAsync(string id, decimal newPrice){
            var product = await _context.Products.Find(x=> x.Id==id).FirstOrDefaultAsync();
            if(product==null){
                return false;
            }
            product.Price=newPrice;
            await _context.Products.ReplaceOneAsync(x=> x.Id==id,product);
            return true;
        }

        
    } 
}