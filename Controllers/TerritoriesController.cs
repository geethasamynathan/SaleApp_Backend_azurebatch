using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;
using SaleApp_Backend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SaleApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITerritoryService _territoryService;
        public TerritoriesController(ITerritoryService territoryService)
        {
           // _mapper = mapper;
            _territoryService = territoryService;
        }

        // GET: api/<TerritoriesController>
        [HttpGet]
        public async Task<IActionResult> GetTerritories()
        {
            return Ok(await _territoryService.GetAllTerritories());          
        }

        // GET api/<TerritoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TerritoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TerritoryDTORequest territoryDTORequest)
        {
            try
            {
                var territory = await _territoryService.CreateNewTerritory(territoryDTORequest);
               
                    return CreatedAtAction(
                        actionName: nameof(GetTerritories),
                        routeValues: new { id = territory.TerritoryId },
                        value: "Record created successfully");
                
            }
            catch (Exception ex) {
                if(await _territoryService.GetTerritoryById(territoryDTORequest.TerritoryId) != null)
                {
                    return Conflict("Given id exist in DB");
                }
            else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while creating territory");
                }
            }
        }

        // PUT api/<TerritoriesController>/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutTerritory(string id, [FromBody] TerritoryDTORequest territoryDTORequest)
        {
            if (id != territoryDTORequest.TerritoryId)
            {
                return BadRequest();
            }
            try
            {
                Territory territory=_mapper.Map<Territory>(territoryDTORequest);
                await _territoryService.UpdateTerritory(territory);
            }
            catch (Exception)
            {

                if (await _territoryService.GetTerritoryById(id) == null)
                {
                    return NotFound();
                }
                else
                    throw;
            }
            return NoContent();

        }
       

        // DELETE api/<TerritoriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
