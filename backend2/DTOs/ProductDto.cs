using backend2.Models;
using System.ComponentModel.DataAnnotations;

namespace backend2.DTOs{

    public class ProductDto{

        [Required(ErrorMessage="Product Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Product Name must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s.\-]+$", ErrorMessage = "Product Name must not contain special characters")]
        public string name{get;set;} = string.Empty;

        [Required(ErrorMessage="Price is required")]
        [Range(0.01, 10000000.00, ErrorMessage="Price must be greater than 0")]
        public decimal price{get;set;} = 0m;

        [Required(ErrorMessage="Stock is required")]
        [Range(0, 1000000, ErrorMessage="Stock must be 0 or a positive integer")]
        public int stock{get;set;} = 0;

        //public DateTime createdAT{get;set;} = DateTime.Now;
    }
}