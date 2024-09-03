using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public interface IEmployeeService
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<List<EmployeeDto>> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> PutEmployeeAsync(Employee id);
        Task<List<Employee>> SearchByTitleAsync(string title);
        Task<List<Employee>> SearchByCityAsync(string city);

        Task<List<Employee>> SearchByHireDateAsync(DateTime HireDate);

        Task<List<object>> HighestSaleByEmployeeByYearAsync(int year);
        Task<List<object>> HighestSaleByEmployeeByDateAsync(DateTime date);
        Task<List<object>> LowestSaleByEmployeeByYearAsync(int year);
        Task<List<object>> LowestSaleByEmployeeByDateAsync(DateTime date);
        Task<List<object>> SearchByEmployeeByDateAsync(int employeeId, DateTime date);
    }
}
