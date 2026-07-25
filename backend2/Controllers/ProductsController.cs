using backend2.DTOs;
using backend2.Services;
using Microsoft.AspNetCore.MVC;



namespace backend2.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase{
        private readonly IProductService _productService;

        public ProductController(IProductService productService){
            _productService=productService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(){
            try{
            var products =await _productService.GetAllProductsAsync();

            return Ok(products);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync([FromRoute] string id){
            try{
                var product = await _productService.GetProductByIdAsync(id);

                if(product==null){
                    return NotFound("Product not Found");   
                }
                return Ok(product);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody]CreateProductDTOs product){
            try{
                var created=await _productService.CreateProductAsync(product);

                return Ok(created);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] string id){
            try{
                var deleted = await _productService.DeleteProductByIdAsync(id);
                if(!deleted){
                    return NotFound("Product Not Found");
                }
                return Ok("Product Deleted Successfully");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductStockAsync([FromRoute] string id, [FromBody]int stock){
            try{
                var updated = await _productService.UpdateProductStockAsync( id,stock );

                 if(updated){return Ok("Product stock updated");}
                 else{return NotFound("product not found");}
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

    }
}