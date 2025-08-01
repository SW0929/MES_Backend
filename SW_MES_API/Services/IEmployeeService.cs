using SW_MES_API.DTO.Common;

namespace SW_MES_API.Services
{
    public interface IEmployeeService
    {
        Task<GetEmployeeListResponseDTO> GetEmployeesByDepartmentAsync(string departmentName);
    }
}
