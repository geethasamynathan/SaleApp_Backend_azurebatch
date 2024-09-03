using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleApp_Backend.DTO;
using SaleApp_Backend.Models;
using SaleApp_Backend.Services;

namespace SaleApp_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployee()
        {
            return await _employeeService.GetAllEmployeeAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                throw new Exception("Employee id not found");
            }
            return Ok(employee);

        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutEmployee(Employee employee)
        {

            var result1 = _employeeService.GetEmployeeByIdAsync(employee.EmployeeId);
            if (result1 != null)
            {

                var res = await _employeeService.PutEmployeeAsync(employee);
                return Ok(new { Message = "Updated successfully.", InsertedRecord = res });
            }
            else
            {
                return BadRequest("Wrong Id");
            }

        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            await _employeeService.CreateEmployeeAsync(employee);
            return Ok(new { Message = "Record created successfully.", InsertedRecord = employee });
        }

        // GET: api/Employees/5
        [HttpGet("title/{title}")]
        public async Task<IActionResult> SearchByTitle(string title)
        {
            var res = await _employeeService.SearchByTitleAsync(title);
            if (res == null || !res.Any())
            {
                throw new Exception("Title not found");
            }
            return Ok(res);


        }
        // GET: api/Employees/5
        [HttpGet("city/{city}")]
        public async Task<IActionResult> SearchByCity(string city)
        {
            var employees = await _employeeService.SearchByCityAsync(city);
            if (employees == null || !employees.Any())
            {
                throw new NotImplementedException("City not found.");
            }
            return Ok(employees);

        }

        // GET: api/Employees/5
        [HttpGet("HireDate/{HireDate}")]
        public async Task<IActionResult> SearchByHireDate(DateTime HireDate)
        {
            var employees = await _employeeService.SearchByHireDateAsync(HireDate);
            if (employees == null || !employees.Any())
            {
                throw new Exception("HireDate not found.");
            }
            return Ok(employees);


        }

        //HighestsalebyYear
        [HttpGet("HighestSaleByEmployeeByYear/{year}")]
        public async Task<ActionResult<IEnumerable<Object>>> HighestSaleByEmployeeByYear(int year)
        {
            var res = await _employeeService.HighestSaleByEmployeeByYearAsync(year);
            return Ok(res);
        }
        //HighestsalebyDate
        [HttpGet("HighestSaleByEmployeeByDate/{date}")]
        public async Task<ActionResult<IEnumerable<Object>>> HighestSaleByEmployeeByDate(DateTime date)
        {
            var res = await _employeeService.HighestSaleByEmployeeByDateAsync(date);
            return Ok(res);
        }
        //LowestsaleByYear
        [HttpGet("LowestSaleByEmployeeByYear/{year}")]
        public async Task<ActionResult<IEnumerable<Object>>> LowestSaleByEmployeeByYear(int year)
        {
            var res = await _employeeService.LowestSaleByEmployeeByYearAsync(year);
            return Ok(res);
        }
        //LowestsaleByDate

        [HttpGet("LowestSaleByEmployeeByDate/{date}")]
        public async Task<ActionResult<IEnumerable<Object>>> LowestSaleByEmployeeByDate(DateTime date)
        {
            var res = await _employeeService.LowestSaleByEmployeeByDateAsync(date);
            return Ok(res);
        }
        //salesmadebyemployeebydate
        [HttpGet("SalemadeByEmployeeByDate/{date}")]
        public async Task<ActionResult<IEnumerable<Object>>> SearchByEmployeeByDate(int employeeId, DateTime date)
        {
            var res = await _employeeService.SearchByEmployeeByDateAsync(employeeId, date);
            return Ok(res);
        }
    }
}
