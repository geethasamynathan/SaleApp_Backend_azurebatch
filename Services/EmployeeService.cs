using Microsoft.EntityFrameworkCore;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;

namespace SaleApp_Backend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly NorthwindPubsContext _context;
        public EmployeeService(NorthwindPubsContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeeAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            var employeesDtos = employees.Select(t => MapToDto(t)).ToList();
            return employeesDtos;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<List<object>> HighestSaleByEmployeeByDateAsync(DateTime date)
        {
            var result = await _context.Employees
               .Select(e => new
               {
                   EmployeeId = e.EmployeeId,
                   FirstName = e.FirstName,
                   LastName = e.LastName,
                   highestSale = e.Orders
                   .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == date.Date)
                   .SelectMany(o => o.OrderDetails)
                   .Sum(od => (decimal)(od.UnitPrice * (decimal)od.Quantity * (decimal)(1 - od.Discount)))
               })
               .Where(e => e.highestSale > 0)
               .OrderByDescending(e => e.highestSale)
               .ToListAsync<object>();
            return result;
        }

        public async Task<List<object>> HighestSaleByEmployeeByYearAsync(int year)
        {
            var result = await _context.Employees
                .Select(e => new
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    highestSale = e.Orders
                    .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == year)
                    .SelectMany(o => o.OrderDetails)
                    .Sum(od => (decimal)(od.UnitPrice * (decimal)od.Quantity * (decimal)(1 - od.Discount)))
                })
                .Where(e => e.highestSale > 0)
               .OrderByDescending(e => e.highestSale)
                .ToListAsync<object>();

            return result;
        }

        public async Task<List<object>> LowestSaleByEmployeeByDateAsync(DateTime date)
        {
            var result = await _context.Employees
               .Select(e => new
               {
                   EmployeeId = e.EmployeeId,
                   FirstName = e.FirstName,
                   LastName = e.LastName,
                   LowestSale = e.Orders
                   .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Date == date.Date)
                   .SelectMany(o => o.OrderDetails)
                   .Sum(od => (decimal)(od.UnitPrice * (decimal)od.Quantity * (decimal)(1 - od.Discount)))
               })
               .Where(e => e.LowestSale > 0)
               .OrderBy(e => e.LowestSale)
               .ToListAsync<object>();
            return result;
        }

        public async Task<List<object>> LowestSaleByEmployeeByYearAsync(int year)
        {
            var result = await _context.Employees
                .Select(e => new
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    LowestSale = e.Orders
                    .Where(o => o.OrderDate.HasValue && o.OrderDate.Value.Year == year)
                    .SelectMany(o => o.OrderDetails)
                    .Sum(od => (decimal)(od.UnitPrice * (decimal)od.Quantity * (decimal)(1 - od.Discount)))
                })
                .Where(e => e.LowestSale > 0)
                .OrderBy(e => e.LowestSale)
                .ToListAsync<object>();

            return result;
        }

        public async Task<Employee> PutEmployeeAsync(Employee id)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(c => c.EmployeeId == id.EmployeeId);
            if (result != null)
            {
                result.FirstName = id.FirstName;
                result.LastName = id.LastName;
                result.Title = id.Title;
                result.TitleOfCourtesy = id.TitleOfCourtesy;
                result.BirthDate = id.BirthDate;
                result.HireDate = id.HireDate;
                result.Address = id.Address;
                result.City = id.City;
                result.Region = id.Region;
                result.PostalCode = id.PostalCode;
                result.Country = id.Country;
                result.HomePhone = id.HomePhone;
                result.Extension = id.Extension;
                result.Notes = id.Notes;
                result.ReportsTo = id.ReportsTo;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<List<Employee>> SearchByCityAsync(string city)
        {
            return await _context.Employees.Where(s => s.City == city).ToListAsync();
        }

        public async Task<List<object>> SearchByEmployeeByDateAsync(int employeeId, DateTime date)
        {
            var result = await _context.Orders
                   .Where(o => o.EmployeeId == employeeId && o.OrderDate.HasValue && o.OrderDate.Value.Date == date.Date)
                   .Select(o => new
                   {
                       OrderId = o.OrderId,
                       CompanyName = o.Customer.CompanyName,
                       OrderDate = o.OrderDate
                   })
                   .ToListAsync<object>();

            return result.Cast<object>().ToList();
        }

        public async Task<List<Employee>> SearchByHireDateAsync(DateTime HireDate)
        {
            return await _context.Employees.Where(s => s.HireDate == HireDate).ToListAsync();
        }

        public async Task<List<Employee>> SearchByTitleAsync(string title)
        {
            return await _context.Employees.Where(s => s.Title == title).ToListAsync();
        }

        private EmployeeDto MapToDto(Employee employee)
        {
            if (employee == null)
            {
                return null!;
            }
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = employee.Photo,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
            };
        }

    }
}
