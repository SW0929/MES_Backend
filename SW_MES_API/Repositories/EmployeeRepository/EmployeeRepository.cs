using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Common;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }


        // JWT 인증 사용하면 수정해야 함.
        // 부서 별 직원 목록 조회
        public async Task<List<EmployeeResponseDTO>> GetAllEmployeesByDepartmentAsync(string departmentName)
        {
            return await _context.Users
                    .Where(u => u.Department.Equals(departmentName)
                             && u.Role.Equals("Operator")
                             && u.IsActive == true)
                    .Select(u => new EmployeeResponseDTO
                    {
                        EmployeeID = u.EmployeeID,
                        Name = u.Name
                    })
                    .ToListAsync();
        }
    }
}
