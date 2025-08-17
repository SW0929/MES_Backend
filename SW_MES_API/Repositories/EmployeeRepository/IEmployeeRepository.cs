using Microsoft.EntityFrameworkCore;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Common;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        // 부서 별 직원 목록 조회
        Task<List<EmployeeResponseDTO>> GetAllEmployeesByDepartmentAsync(string departmentName);
    }
}
