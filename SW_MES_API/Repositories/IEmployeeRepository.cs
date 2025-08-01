using SW_MES_API.DTO.Common;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeResponseDTO>> GetAllEmployeesByDepartmentAsync(string departmentName);
    }
}
