using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Login;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Login
{
    // 순수하게 데이터 베이스 접근만 한다.
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;
        public LoginRepository(AppDbContext context) 
        {
            _context = context;
        }

        // 로그인 요청에 대한 사용자 정보를 가져오는 메서드
        public async Task<User?> GetByUserInfoAsync(LoginRequestDTO request)
        {
            /*
             SELECT TOP 1 * FROM MESUsers
             WHERE EmployeeID = 990929 이런 쿼리문이 EF Core에 의해 자동으로 생성된다.
            */
            return await _context.Users.FirstOrDefaultAsync(u => u.EmployeeID == request.EmployeeID);
        }
    }
}
