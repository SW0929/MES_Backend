using SW_MES_API.DTO.Common;
using SW_MES_API.Repositories;

namespace SW_MES_API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<GetEmployeeListResponseDTO> GetEmployeesByDepartmentAsync(string departmentName)
        {
            var employees = await _employeeRepository.GetAllEmployeesByDepartmentAsync(departmentName);
            if (employees == null || employees.Count == 0)
            {
                return new GetEmployeeListResponseDTO
                {
                    Message = "조회 가능한 사원이 없습니다.",
                    Employees = []
                };
            }
            return new GetEmployeeListResponseDTO
            {
                Message = "사원 조회 성공",
                Employees = employees
            };
        }
    }
}
