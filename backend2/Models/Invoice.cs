using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace backend2.Models{

    public class InvoiceItem{
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public decimal Rate { get; set; } = 0;
        public decimal TotalAmount { get; set; } = 0;
    }
    public class Invoice{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id{get;set}=string.Empty;
        

        [BsonElement("invoiceNumber")]
        public string InvoiceNumber{get;set}=string.Empty;

        [BsonElement("partyId")]
        public string PartyId{get;set; }=string.Empty;

        [BsonElement("partyName")]
        public string PartyName{get;set; }=string.Empty;

        [BsonElement("partyPhone")]
        public string PartyPhone{get;set; }=string.Empty;

        [BsonElement("partyGstin")]
        public string PartyGstin{get;set; }=string.Empty;
        
        [BsonElement("items")]
        public List<InvoiceItem> Items{get;set; }=new List<InvoiceItem>();

        [BsonElement("subTotal")]
        public decimal SubTotal{get;set; }=0m;

        [BsonElement("gst")]
        public decimal Gst{get;set; }=0m;

        [BsonElement("grandTotal")]
        public decimal GrandTotal{get;set; }=0m;

        public DateTime InvoiceDate{get;set; }=DateTime.UtcNow;

        

    }
}