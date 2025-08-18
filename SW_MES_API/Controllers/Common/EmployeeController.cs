using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO.Common;
using SW_MES_API.Services.Common.EmployeeService;

namespace SW_MES_API.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("department/{departmentName}")]
        public async Task<IActionResult> GetEmployeesByDepartment(string departmentName)
        {
            var employees = await _employeeService.GetEmployeesByDepartmentAsync(departmentName);

            if (employees == null || employees.Employees == null || employees.Employees.Count == 0)
                return NotFound("No employees found.");

            return Ok(new
            {
                message = employees.Message,
                employees = employees.Employees
            });
        }
    }
}
