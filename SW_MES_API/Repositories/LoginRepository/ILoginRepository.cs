using SW_MES_API.DTO.Login;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.LoginRepository
{
    public interface ILoginRepository
    {
        // 사용자가 입력한 사번으로 로그인 정보를 조회
        public Task<User?> GetByUserInfoAsync(LoginRequestDTO request);
    }
}
