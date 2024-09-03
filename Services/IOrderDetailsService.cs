using SaleApp_Backend.DTO;

namespace SaleApp_Backend.Services
{
    public interface IOrderDetailsService
    {
        Task<List<OrderDetailDto>> GetAllOrderDetailsAsync();
        Task<IEnumerable<object>> GetBillAmountsByEmployeeAsync(int employeeId);
    }
}
