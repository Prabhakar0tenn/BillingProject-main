using backend2.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend2.Controllers{

    [ApiController]
    [Route("api/[Controller]")]
    public class InvoiceController:ControllerBase{

        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService){

            _invoiceService = invoiceService;

        }


        [HttpGet]
        public async Task<IActionResult> GetAllInvoicesAsync(){

            try{
                var invoices = await _invoiceService.GetAllInvoicesAsync();
                return Ok(invoices);

            }
            catch{
                return BadRequest("Unable to fetch invoices");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceByIdAsync(string id){

            try{
                var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
                if(invoice==null){
                    return NotFound("Invoice not found");
                }
                return Ok(invoice);
            }
            catch{
                return BadRequest("Unable to fetch invoice");
            }
        }

        [HttpPost]

        public async Task<IActionResult> CreateInvoiceAsync(CreateInvoiceRequestDto request){

            try{
                var created = await _invoiceService.CreateInvoiceAsync(request);
                return Ok(created);

            }
            catch{
                return BadRequest("Unable to create invoice");
            }
        }

        
        




    }
    
}