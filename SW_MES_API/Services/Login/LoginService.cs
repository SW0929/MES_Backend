using SW_MES_API.DTO.Login;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Login;

namespace SW_MES_API.Services.Login
{
    // Repository에서 사용자 정보를 가져오고
    // 로직을 담당
    // 나중에 토큰 발급도 여기서 처리 가능!!!
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;
        public LoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        // 요청DTO 를 사용해서 정상적으로 값이 있으면 응답DTO를 반환한다.
        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
        {
            var user = await _repository.GetByUserInfoAsync(request); // 여기서 DB에 접근해서 사용자 정보를 가져온다.
            
            if (user == null || !user.IsActive)
                return null;

            return new LoginResponseDTO
            {   
                EmployeeID = user.EmployeeID,
                Name = user.Name,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }
    }
}
