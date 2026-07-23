using backend2.Models;


namespace backend2.DTOs{

    public class CreateInvoiceItemDto{

        public string ProductId {get; set; }=string.Empty;
        public int Quantity {get; set; }=0;
        
    }
}