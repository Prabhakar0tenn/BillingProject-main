using backend2.Models;

namespace backend2.DTOs{
    public class CreateInvoiceRequestDto{
        public string PartyId {get;set;}=string.Empty;
       
       public List<CreateInvoiceItemDto> Items {get;set;}=new List<CreateInvoiceItemDto>();
    }
}