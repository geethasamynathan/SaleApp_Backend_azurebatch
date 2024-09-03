using Microsoft.EntityFrameworkCore;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly NorthwindPubsContext _context;

        public OrderService(NorthwindPubsContext context)
        {
            _context = context;
        }
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            var orderDtos = orders.Select(t => MapToDto(t)).ToList();
            return orderDtos;
        }

        public async Task<IEnumerable<object>> GetAllShipDetailsAsync()
        {
            return await _context.Orders.Select(s => new
            {
                ShipName = s.ShipName,
                ShipAddress = s.ShipAddress,
                ShipRegion = s.ShipRegion,
                ShipPostalCode = s.ShipPostalCode
            }).ToListAsync();
        }

        public async Task<IEnumerable<dynamic>> GetOrdersBetweenDatesAsync(DateTime fromDate, DateTime toDate)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate)
                .Select(o => new
                {
                    ShipName = o.ShipName,
                    ShipRegion = o.ShipRegion
                })
                .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetOrdersByEmployeeFirstNameAsync(string firstName)
        {
            var orders = await _context.Orders
                .Where(o => o.Employee.FirstName == firstName)
                .ToListAsync();

            return orders;
        }

        public async Task<IEnumerable<object>> GetShipDetailsByOrderIDAsync(int id)
        {
            return await _context.Orders.Where(s => s.OrderId == id).Select(s => new
            {
                ShipName = s.ShipName,
                ShipAddress = s.ShipAddress

            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetTotalOrdersByEmployeeAsync()
        {
            var totalOrdersByEmployee = await _context.Orders
                .GroupBy(o => o.EmployeeId)
                .Select(g => new
                {
                    EmployeeName = g.FirstOrDefault().Employee.FirstName,
                    TotalOrders = g.Count()
                })
                .ToListAsync();

            return totalOrdersByEmployee;
        }

        private OrderDto MapToDto(Order order)
        {
            if (order == null)
            {
                return null!;
            }

            return new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry,
            };
        }
    }

    }
