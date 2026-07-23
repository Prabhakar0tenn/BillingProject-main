using backend2.Models;
using backend2.Data;
using MongoDB.Driver;


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

            return await _context.Parties.Find(x=> x.Id==id).FirstOrDefaultAsync();
 
         }


         public async Task<Party>CreatePartyAsync(Party party){

            await _context.Parties.InsertOneAsync(party);
            return party;

            
         }



         public async Task <bool>DeletePartyAsync(string id){

            DeleteResult result = await _context.Parties.DeleteOneAsync(x=>x.Id==id);
            return result.DeletedCount>0;   //many more properties like IsAcknowledged --> if false DeletedCount will throw an error but here IsAcknowledged is always true..
          
         }

        //   public Task<List<Party>> UpdatePartyAsync(string id){  //leave for now


            
        //  }


    }
}