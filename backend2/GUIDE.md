# Simple B2B Billing System — Professional Clean Architecture Backend Guide

Welcome! Is guide mein hum Professional Best Practices (**Controller ➔ Service ➔ Data/Repository**) follow karte hue step-by-step backend banayenge.

---

## 👏 Kudos on Creating Separate DTO Files!

Aapne `DTOs/CreateInvoiceRequestDto.cs` aur `DTOs/CreateInvoiceItemDto.cs` ki separate files banayi hain — **Yeh 100% Professional Industry Standard hai!**
Har class ki apni alag file hone se code maintain karna aur samajhna bohot aasan ho jata hai.

---

## 🔍 C# Syntax Tip: Auto-Properties Spacing

C# compiler auto-properties parse karte waqt braces ke andar spaces aur semicolon expect karta hai:
- ❌ `public string Id{get;set}=string.Empty;`  (Compiler Error: CS8180)
- ✅ `public string Id { get; set; } = string.Empty;` (Proper C# Syntax)

---

## 🛠️ Quick Formatting Fixes for 0 Build Errors

### 1. `Models/Product.cs`
Line 10 par `set` ke baad `;` missing tha aur spaces adjust karein:
```csharp
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend2.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; } = 0m;

        [BsonElement("stock")]
        public int Stock { get; set; } = 0;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
```

---

### 2. `Models/Invoice.cs`
Properties ke andar spaces add karein: `{ get; set; }`
```csharp
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend2.Models
{
    public class InvoiceItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public decimal Rate { get; set; } = 0m;
        public decimal TotalAmount { get; set; } = 0m;
    }

    public class Invoice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("invoiceNumber")]
        public string InvoiceNumber { get; set; } = string.Empty;

        [BsonElement("partyId")]
        public string PartyId { get; set; } = string.Empty;

        [BsonElement("partyName")]
        public string PartyName { get; set; } = string.Empty;

        [BsonElement("partyPhone")]
        public string PartyPhone { get; set; } = string.Empty;

        [BsonElement("partyGstin")]
        public string PartyGstin { get; set; } = string.Empty;

        [BsonElement("items")]
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        [BsonElement("subTotal")]
        public decimal SubTotal { get; set; } = 0m;

        [BsonElement("gst")]
        public decimal Gst { get; set; } = 0m;

        [BsonElement("grandTotal")]
        public decimal GrandTotal { get; set; } = 0m;

        [BsonElement("invoiceDate")]
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
    }
}
```

---

## 📂 Layer 4: Controllers (Presentation Layer)

Folder banayein: `backend2/Controllers/`

### 1. `Controllers/PartiesController.cs`
```csharp
using backend2.Models;
using backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly IPartyService _partyService;

        public PartiesController(IPartyService partyService)
        {
            _partyService = partyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParties()
        {
            var parties = await _partyService.GetAllPartiesAsync();
            return Ok(parties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartyById(string id)
        {
            var party = await _partyService.GetPartyByIdAsync(id);
            if (party == null) return NotFound("Party not found");
            return Ok(party);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParty([FromBody] Party party)
        {
            var created = await _partyService.CreatePartyAsync(party);
            return Ok(created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParty(string id)
        {
            var deleted = await _partyService.DeletePartyAsync(id);
            if (!deleted) return NotFound("Party not found");
            return Ok(new { message = "Party deleted successfully" });
        }
    }
}
```

---

### 2. `Controllers/ProductsController.cs`
```csharp
using backend2.Models;
using backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var created = await _productService.CreateProductAsync(product);
            return Ok(created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var deleted = await _productService.DeleteProductByIdAsync(id);
            if (!deleted) return NotFound("Product not found");
            return Ok(new { message = "Product deleted successfully" });
        }
    }
}
```

---

### 3. `Controllers/InvoicesController.cs`
```csharp
using backend2.DTOs;
using backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceById(string id)
        {
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null) return NotFound("Invoice not found");
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequestDto dto)
        {
            try
            {
                var created = await _invoiceService.CreateInvoiceAsync(dto);
                return Ok(created);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(string id)
        {
            var deleted = await _invoiceService.DeleteInvoiceAsync(id);
            if (!deleted) return NotFound("Invoice not found");
            return Ok(new { message = "Invoice deleted successfully" });
        }
    }
}
```

---

## ⚙️ Final Step: `Program.cs` Dependency Injection (DI) & CORS Setup

### `Program.cs`
```csharp
using backend2.Data;
using backend2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 1. Register MongoDbContext
builder.Services.AddSingleton<MongoDBContext>();

// 2. Register Application Services (Dependency Injection)
builder.Services.AddScoped<IPartyService, PartyService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// 3. Enable CORS for React Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
```

---

## 📌 Agle Steps:
1. `Product.cs` aur `Invoice.cs` mein `{ get; set; }` spaces update karein.
2. `Controllers` folder banayein aur teeno Controllers (`PartiesController.cs`, `ProductsController.cs`, `InvoicesController.cs`) create karein.
3. `Program.cs` update karein.
4. Terminal mein `dotnet run` chalakar apne naye backend APIs launch karein! 🎉
