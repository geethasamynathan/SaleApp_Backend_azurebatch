using Microsoft.EntityFrameworkCore;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly NorthwindPubsContext _context;

        public OrderDetailsService(NorthwindPubsContext context)
        {
            _context = context;
        }
        public async Task<List<OrderDetailDto>> GetAllOrderDetailsAsync()
        {
            var orderDetails = await _context.OrderDetails.ToListAsync();
            var orderDetailsDtos = orderDetails.Select(t => MapToDto(t)).ToList();
            return orderDetailsDtos;
        }

        public async Task<IEnumerable<object>> GetBillAmountsByEmployeeAsync(int employeeId)
        {
            var billAmounts = await _context.OrderDetails
               .Where(od => od.Order.EmployeeId == employeeId)
               .GroupBy(od => od.OrderId)
               .Select(g => new
               {
                   OrderId = g.Key,
                   BillAmount = g.Sum(od => ((decimal)od.UnitPrice * (decimal)od.Quantity) - ((decimal)od.UnitPrice * (decimal)od.Quantity * (decimal)od.Discount))
               })
               .ToListAsync();

            return billAmounts;
        }

        private OrderDetailDto MapToDto(OrderDetail orderDetail)
        {
            if (orderDetail == null)
            {
                return null!;
            }

            return new OrderDetailDto
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                UnitPrice = orderDetail.UnitPrice,
                Quantity = orderDetail.Quantity,
                Discount = orderDetail.Discount,
            };
        }
    }

}
