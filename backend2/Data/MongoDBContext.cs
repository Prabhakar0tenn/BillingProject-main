using backend2.Models;
using MongoDB.Driver;

namespace backend2.Data{
    public class MongoDBContext{
        private readonly IMongoDatabase _database;

        public MongoDBContext(IConfiguration configuration){
            var connectionString = configuration.GetConnectionString("MongoDb")?? throw new InvalidOperationException("Connection string 'MongoDb' not found.");
            var databaseName=configuration["DatabaseName"] ?? "Billingdb2";

            var client= new MongoClient(connectionString);
            _database=client.GetDatabase(databaseName);

        }

        public IMongoCollection<Party> Parties => _database.GetCollection<Party> ("Parties"); 

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

        public IMongoCollection<Invoice> Invoices => _database.GetCollection<Invoice>("Invoices");
    }
}
