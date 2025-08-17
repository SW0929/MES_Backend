using SW_MES_API.DTO.Common;

namespace SW_MES_API.Services.Common.EmployeeService
{
    public interface IEmployeeService
    {
        Task<GetEmployeeListResponseDTO> GetEmployeesByDepartmentAsync(string departmentName);
    }
}
