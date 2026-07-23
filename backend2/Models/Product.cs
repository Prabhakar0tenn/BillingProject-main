using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend2.Models{

    public class Product{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{ get;set; }=string.Empty;

        [BsonElement("name")]
        public string Name{ get;set; }=string.Empty;

        [BsonElement("price")]
        public decimal Price{ get;set; }=0m;

        [BsonElement("stock")]
        public int Stock{ get;set; }=0;

        [BsonElement("createdAt")]
        public DateTime CreatedAt{ get;set; }=DateTime.UtcNow; //set ki jageh init use kr skey bs object bnte time value de skte baki readonly rhega 


       
    }
}
