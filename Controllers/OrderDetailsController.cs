using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;
using SaleApp_Backend.Services;

namespace SaleApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDto>>> GetOrderDetails()
        {
            var orderDetails = await _orderDetailsService.GetAllOrderDetailsAsync();
            return Ok(orderDetails);
        }
        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetailByEmployeeId(int id)
        {
            var billDetails = await _orderDetailsService.GetBillAmountsByEmployeeAsync(id);

            if (billDetails == null)
            {
                return NotFound();
            }

            return Ok(billDetails);
        }
    }
}
