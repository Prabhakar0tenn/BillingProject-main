using backend2.Models;
using backend2.DTOs;

namespace backend2.Services{
    public interface IInvoiceService{

        Task<List<Invoice>> GetAllInvoicesAsync();

        Task<Invoice?> GetInvoiceByIdAsync(string id);

        Task<Invoice> CreateInvoiceAsync(CreateInvoiceRequestDto invoice);

        // Task<bool> DeleteInvoiceAsync(string id);
        
    }
} 