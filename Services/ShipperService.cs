using Microsoft.EntityFrameworkCore;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public class ShipperService:IShipperService
    {
        private readonly NorthwindPubsContext _context;

        public ShipperService(NorthwindPubsContext context)
        {
            _context = context;
        }
        public async Task<Shipper> CreateShipperAsync(Shipper shipper)
        {
            var result = await _context.Shippers.AddAsync(shipper);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Shipper> DeleteShipperAsync(int id)
        {
            var result = await _context.Shippers.FirstOrDefaultAsync(c => c.ShipperId == id);
            if (result != null)
            {
                _context.Shippers.Remove(result);
                await _context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<List<ShipperDto>> GetAllShippersAsync()
        {
            var shippers = await _context.Shippers.ToListAsync();
            var shipperDtos = shippers.Select(t => MapToDto(t)).ToList();
            return shipperDtos;
        }

        public async Task<List<Shipper>> GetShipperByCompNameAsync(string CompanyName)
        {
            return await _context.Shippers.Where(s => s.CompanyName == CompanyName).ToListAsync();
        }

        public async Task<Shipper> GetShipperByIdAsync(int id)
        {
            return await _context.Shippers.FindAsync(id);
        }

        public async Task<IEnumerable<object>> GetTotalAmountEarnedByShipperAsync()
        {
            var totalAmountByShipper = await _context.Orders
                .GroupBy(o => o.ShipViaNavigation.CompanyName)
                .Select(g => new
                {
                    ShipperName = g.Key,
                    TotalAmount = g.SelectMany(o => o.OrderDetails).Sum(od => ((decimal)od.UnitPrice * (decimal)od.Quantity) * (1 - (decimal)od.Discount))
                })
                .ToListAsync();
            return totalAmountByShipper.Select(t => new { ShipperName = t.ShipperName, TotalAmount = t.TotalAmount });
        }

        public async Task<IEnumerable<object>> GetTotalAmountEarnedByShipperByDateAsync(DateTime shipdate)
        {
            var ordersForShipDateCount = await _context.Orders.CountAsync(o => o.ShippedDate == shipdate);
            if (ordersForShipDateCount == 0)
            {
                throw new Exception($"No ship date found : {shipdate}");
            }
            var totalAmountByShipper = await _context.Orders
                .Where(o => o.ShippedDate == shipdate)
                .GroupBy(o => o.ShipViaNavigation.CompanyName)
                .Select(g => new
                {
                    ShipperName = g.Key,
                    TotalAmount = g.SelectMany(o => o.OrderDetails).Sum(od => ((decimal)od.UnitPrice * (decimal)od.Quantity) * (1 - (decimal)od.Discount))
                })
                .ToListAsync();
            return totalAmountByShipper.Select(t => new { ShipperName = t.ShipperName, TotalAmount = t.TotalAmount });
        }

        public async Task<IEnumerable<object>> GetTotalAmountEarnedByShipperByYearAsync(int year)
        {
            var ordersForShipDateCount = await _context.Orders.CountAsync(o => o.ShippedDate.Value.Year == year);
            if (ordersForShipDateCount == 0)
            {
                throw new Exception($"No such year found : {year}");
            }
            var totalAmountByShipper = await _context.Orders
                .Where(o => o.ShippedDate.Value.Year == year)
                .GroupBy(o => o.ShipViaNavigation.CompanyName)
                .Select(g => new
                {
                    ShipperName = g.Key,
                    TotalAmount = g.SelectMany(o => o.OrderDetails).Sum(od => ((decimal)od.UnitPrice * (decimal)od.Quantity) * (1 - (decimal)od.Discount))
                })
                .ToListAsync();
            return totalAmountByShipper.Select(t => new { ShipperName = t.ShipperName, TotalAmount = t.TotalAmount });
        }

        public async Task<IEnumerable<object>> GetTotalShipmentsByShipperAsync()
        {
            var totalOrdersByShipper = await _context.Orders
                .GroupBy(o => o.ShipVia)
                .Select(g => new
                {
                    CompanyName = g.FirstOrDefault().ShipViaNavigation.CompanyName,
                    TotalOrders = g.Count()
                })
                .ToListAsync();

            return totalOrdersByShipper;
        }

        public async Task<Shipper> UpdateShipperAsync(Shipper customer)
        {
            var result = await _context.Shippers.FirstOrDefaultAsync(c => c.ShipperId == customer.ShipperId);
            if (result != null)
            {
                result.CompanyName = customer.CompanyName;
                result.Phone = customer.Phone;
                result.Email = customer.Email;
                result.Password = customer.Password;
                await _context.SaveChangesAsync();
                return result;

            }
            return null;
        }
        private ShipperDto MapToDto(Shipper shipper)
        {
            if (shipper == null)
            {
                return null!;
            }
            return new ShipperDto
            {
                ShipperId = shipper.ShipperId,
                CompanyName = shipper.CompanyName,
                Phone = shipper.Phone,
            };
        }
    }
}
