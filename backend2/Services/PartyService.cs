using backend2.Models;
using backend2.Data;
using MongoDB.Driver;
using backend2.DTOs;


namespace backend2.Services{
    public class PartyService :IPartyService{

        private readonly MongoDBContext _context;
        public PartyService(MongoDBContext context){
            _context=context;
        }

         public async Task<List<Party>> GetAllPartiesAsync(){
                return await _context.Parties.Find(x=>true).ToListAsync();
         }

         public async Task<Party?> GetPartyByIdAsync(string id){
            if(string.IsNullOrEmpty(id)){
                return null;
            }
            return await _context.Parties.Find(x=> x.Id==id).FirstOrDefaultAsync();
 
         }


         public async Task<Party>CreatePartyAsync(PartyDto party){

            var newParty=new Party{
                PartyName=party.partyname,
                Phone=party.Phone,
                Gstin=party.Gstin,
                Address=party.address,
                CreatedAt=DateTime.Now,  
            };

            await _context.Parties.InsertOneAsync(newParty);
            return newParty;

            
         }



         public async Task <bool>DeletePartyAsync(string id){

            DeleteResult result = await _context.Parties.DeleteOneAsync(x=>x.Id==id);
            return result.DeletedCount>0;   //many more properties like IsAcknowledged --> if false DeletedCount will throw an error but here IsAcknowledged is always true..
          
         }

        //   public Task<List<Party>> UpdatePartyAsync(string id){  


            
        //  }


    }
}