using backend2.Models;

namespace backend2.Services{
    public interface IInvoiceService{

        Task<List<Invoice>> GetAllInvoicesAsync();

        Task<Invoice?> GetInvoiceByIdAsync(string id);

        Task<Invoice> CreateInvoiceAsync(Invoice invoice);

        Task<bool> DeleteInvoiceAsync(string id);
        
    }
} 