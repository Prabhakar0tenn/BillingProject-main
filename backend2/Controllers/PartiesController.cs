using backend2.Services;
using backend2.Modals;
using backend2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend2.Controllers{

    [ApiController]
    [Route("api/[controller]")]
    public class PartiesController:ControllerBase{

        private readonly IPartyService _partyService;

        public PartiesController(IPartyService partyService){
            _partyService=partyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPartiesAsync(){
            try{
                var parties = await _partyService.GetAllPartiesAsync();
                return Ok(parties);
            }
            catch(Exception e){
                 
                 return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartyAsync([FromBody] CreatePartyDTO party){

            try{
                var createdParty= await _partyService.CreatePartyAsync(party);
                return Ok(createdParty);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartyAsync( [FromRoute] string id){
            try{
                if(await _partyService.DeletePartyAsync(id)){
                    return Ok("Party Deleted");
                }
                else{
                    return NotFound("Party Not Found");
                }
            }
            catch(Exception e){
                return BadRequest(e.Message);

            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartyByIdAsync([FromRoute] string id){
            try{
                var party = await _partyService.GetPartyByIdAsync(id);
                if(party==null){
                    return NotFound("Party doesn't exist");
                }
                return Ok(party);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }




        

    }





}