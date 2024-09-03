using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;
using SaleApp_Backend.Services;

namespace SaleApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        // GET: api/Shippers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperDto>>> GetShippers()
        {
            var shippers = await _shipperService.GetAllShippersAsync();
            return Ok(shippers);

        }
        // GET: api/Shippers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shipper>> GetShipper(int id)
        {
            var shipper = await _shipperService.GetShipperByIdAsync(id);
            if (shipper == null)
            {
                return NotFound();
            }
            return Ok(shipper);

        }
        // PUT: api/Shippers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipper(int id, Shipper shipper)
        {
            var res = (await _shipperService.UpdateShipperAsync(shipper));
            if (res == null)
            {
                return NotFound();
            }
            return Ok(new { Message = "Record updated successfully.", UpdatedRecord = res });


        }

        // POST: api/Shippers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shipper>> PostShipper(Shipper shipper)
        {
            await _shipperService.CreateShipperAsync(shipper);
            return Ok(new { Message = "Record created successfully.", InsertedRecord = shipper });
        }

        // DELETE: api/Shippers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipper(int id)
        {

            var res = await _shipperService.DeleteShipperAsync(id);
            return Ok(new { Message = "Record Deleted Successfully" });

        }
        [HttpGet("company/{companyName}")]
        public async Task<IActionResult> GetShipperByName(string companyName)
        {

            var shipper = await _shipperService.GetShipperByCompNameAsync(companyName);
            if (shipper == null)
            {
                return NotFound();
            }
            return Ok(shipper);


        }
        [HttpGet("totalshipment")]
        public async Task<IEnumerable<object>> GetTotalShipmentsByShipperAsync()
        {
            return await _shipperService.GetTotalShipmentsByShipperAsync();
        }
        [HttpGet("totalamountearnedbyshipper")]
        public async Task<IEnumerable<object>> GetTotalAmountEarnedByShipperAsync()
        {
            return await _shipperService.GetTotalAmountEarnedByShipperAsync();
        }
        [HttpGet("totalamountearnedbyshipper/date")]
        public async Task<IEnumerable<object>> GetTotalAmountEarnedByShipperByDateAsync(DateTime shipdate)
        {
            var res = await _shipperService.GetTotalAmountEarnedByShipperByDateAsync(shipdate);

            return res;
        }
        [HttpGet("totalamountearnedbyshipper/{year}")]
        public async Task<IEnumerable<object>> GetTotalAmountEarnedByShipperByYearAsync(int year)
        {
            return await _shipperService.GetTotalAmountEarnedByShipperByYearAsync(year);
        }
    }
}
