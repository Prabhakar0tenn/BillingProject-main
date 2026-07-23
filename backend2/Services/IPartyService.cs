using backend2.Models;

namespace backend2.Services{
    public interface IPartyService
    {
        Task<List<Party>> GetAllPartiesAsync();
        Task<Party?> GetPartyByIdAsync(string id);
        Task<bool> DeletePartyAsync(String id);
        Task<Party> CreatePartyAsync(Party party);


    }
}
