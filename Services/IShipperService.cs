using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public interface IShipperService
    {
        Task<List<ShipperDto>> GetAllShippersAsync();
        Task<Shipper> GetShipperByIdAsync(int id);
        Task<List<Shipper>> GetShipperByCompNameAsync(string CompanyName);
        Task<Shipper> CreateShipperAsync(Shipper customer);
        Task<Shipper> UpdateShipperAsync(Shipper customer);
        Task<Shipper> DeleteShipperAsync(int id);
        Task<IEnumerable<object>> GetTotalShipmentsByShipperAsync();
        Task<IEnumerable<object>> GetTotalAmountEarnedByShipperAsync();
        Task<IEnumerable<object>> GetTotalAmountEarnedByShipperByDateAsync(DateTime shipdate);
        Task<IEnumerable<object>> GetTotalAmountEarnedByShipperByYearAsync(int year);
    }
}
