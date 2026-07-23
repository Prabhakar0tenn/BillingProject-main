using backend2.Models;
using backend2.Data;
using MongoDB.Driver;


namespace backend2.Services{
    public class InvoiceService:IInvoiceService{

        private readonly MongoDBContext _context;

        public InvoiceService(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync(){
            return await _context.Invoices.Find(x=>true).ToListAsync();
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(string id){
            return await _context.Invoices.Find(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<Invoice> CreateInvoiceAsync(CreateInvoiceRequestDto invoiceRequest){

            var count = await _context.Invoices.CountDocumentsAsync(x => true);
            string invoiceNumber= "INV-"+(count+1);


        

            var party = await _context.Parties.Find(x=>x.Id==invoiceRequest.PartyId).FirstOrDefaultAsync();

                if (party == null)
                {
                    throw new InvalidOperationException("Party not found");
                }
            string partyname= party.Name;
            string partyid= invoiceRequest.PartyId;

            string partyphone= party.Phone;
            string partygst= party.Gstin;

            decimal totalamount=0;

            List<InvoiceItem> item=new List<InvoiceItem>();

            foreach(var itm in invoiceRequest.Items){
                string productid= itm.ProductId;
                var product=await _context.Products.Find(x=>x.Id==productid).FirstOrDefaultAsync();

                if(product==null){
                    throw new InvalidOperationException("Product with "+productid+" not found");
                }
                string productname =product.Name;
                decimal price =product.Price;



                int quantity=itm.Quantity;

                if(quantity>product.Stock){
                    throw new InvalidOperationException("Product with "+productname+" out of stock");
                }
                product.Stock -=quantity;
                await _context.Products.ReplaceOneAsync(x=>x.Id==productid,product);
                
                decimal totalproductamount=price*quantity;
                totalamount+=totalproductamount;

                item.Add(new InvoiceItem{
                    ProductId=productid,
                    ProductName=productname,
                    Quantity=quantity,
                    Rate=price,
                    TotalAmount=totalproductamount
                })  ;

            }


          
            
            var invoice =new Invoice {
                InvoiceNumber = invoiceNumber,
                PartyId = partyid,
                PartyName = partyname,
                PartyPhone = partyphone,
                PartyGstin = partygst,
                Items = item,
                SubTotal = totalamount,
                Gst = totalamount * 0.05m,
                GrandTotal = totalamount * 1.05m,
                InvoiceDate = DateTime.UtcNow
            }; 

            await _context.Invoices.InsertOneAsync(invoice);

            return invoice;
 
        }

        public async Task<bool> DeleteInvoiceAsync(string id){
            DeleteResult result= await _context.Invoices.DeleteOneAsync(x=>x.Id==id);
            return result.DeletedCount>0;
        }
    }
}