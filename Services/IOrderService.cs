using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<Object>> GetShipDetailsByOrderIDAsync(int id);
        Task<IEnumerable<object>> GetAllShipDetailsAsync();
        Task<IEnumerable<object>> GetTotalOrdersByEmployeeAsync();
        Task<List<Order>> GetOrdersByEmployeeFirstNameAsync(string firstName);
        Task<IEnumerable<dynamic>> GetOrdersBetweenDatesAsync(DateTime fromDate, DateTime toDate);
    }
}
