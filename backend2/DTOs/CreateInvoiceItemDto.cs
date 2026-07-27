using backend2.Models;
using System.ComponentModel.DataAnnotations;

namespace backend2.DTOs{

    public class CreateInvoiceItemDto{

        [Required(ErrorMessage = "Product ID is required")]
        public string ProductId {get; set; }=string.Empty;

        [Range(1, 10000, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity {get; set; }=0;
        
    }
}