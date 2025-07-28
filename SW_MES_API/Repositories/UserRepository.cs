using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories
{
    // 순수하게 데이터 베이스 접근만 한다.
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<User?> GetByUserInfoAsync(int EmployeeID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.EmployeeID == EmployeeID && u.IsActive);
        }
    }
}
