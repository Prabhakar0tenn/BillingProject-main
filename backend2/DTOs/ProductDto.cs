using backend2.Models;
using System.ComponentModel.DataAnnotations;

namespace backend2.DTOs{

    public class ProductDto{

        [Required(ErrorMessage="Name is Required ")]
        public string name{get;set;} = string.Empty;

        [Range(0, 10000000, ErrorMessage="Price must be positive")]
        public decimal price{get;set;} = 0m;

        [Range(0, 100000, ErrorMessage="Stock must be positive")]
        public int stock{get;set;} = 0;

        //public DateTime createdAT{get;set;} = DateTime.Now;
    }
}