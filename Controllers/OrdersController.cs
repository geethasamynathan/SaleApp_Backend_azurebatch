using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;
using SaleApp_Backend.Services;

namespace SaleApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Orders/OrderbyEmployee/firstname
        [HttpGet("OrderbyEmployee/{firstName}")]
        public async Task<List<Order>> GetOrdersByEmployeeFirstNameAsync(string firstName)
        {
            return await _orderService.GetOrdersByEmployeeFirstNameAsync(firstName);
        }

        // GET: api/Orders/shipdetails/5
        [HttpGet("shipdetails/{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetShipDetailsByOrderID(int id)
        {
            var order = await _orderService.GetShipDetailsByOrderIDAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // GET: api/Orders/BetweenDate/fromDate/toDate
        [HttpGet("BetweenDate/{fromDate}/{toDate}")]
        public async Task<IEnumerable<dynamic>> GetOrdersBetweenDatesAsync(DateTime fromDate, DateTime toDate)
        {
            return await _orderService.GetOrdersBetweenDatesAsync(fromDate, toDate);
        }


        // GET: api/Orders/allshipdetails
        [HttpGet("allshipdetails")]
        public async Task<ActionResult<IEnumerable<object>>> GetOrdersShipDetails()
        {
            var result = await _orderService.GetAllShipDetailsAsync();
            return Ok(result);

        }


        // GET: api/Orders/numberoforderbyeachemployee
        [HttpGet("numberoforderbyeachemployee")]
        public async Task<IEnumerable<object>> GetTotalOrdersByEmployeeAsync()
        {
            return await _orderService.GetTotalOrdersByEmployeeAsync();
        }

    }
}
