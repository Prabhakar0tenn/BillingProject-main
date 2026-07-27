using backend2.Models;
using System.ComponentModel.DataAnnotations;

namespace backend2.DTOs{
    public class CreateInvoiceRequestDto{
        [Required(ErrorMessage = "Party ID is required")]
        public string PartyId {get;set;}=string.Empty;
       
        [Required(ErrorMessage = "Invoice items cannot be empty")]
        [MinLength(1, ErrorMessage = "Invoice must contain at least one item")]
        public List<CreateInvoiceItemDto> Items {get;set;}=new List<CreateInvoiceItemDto>();
    }
}