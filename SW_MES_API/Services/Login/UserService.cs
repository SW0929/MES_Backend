using SW_MES_API.DTO.Login;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Login;

namespace SW_MES_API.Services.Login
{
    // Repository에서 사용자 정보를 가져오고
    // 로직을 담당
    // 나중에 토큰 발급도 여기서 처리 가능!!!
    public class UserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        // 요청DTO 를 사용해서 정상적으로 값이 있으면 응답DTO를 반환한다.
        public async Task<LoginResponseDTO?> LoginAsync(int EmployeeID)
        {
            var user = await _repository.GetByUserInfoAsync(EmployeeID); // 여기서 DB에 접근해서 사용자 정보를 가져온다.
            /*
             SELECT TOP 1 * FROM MESUsers
             WHERE EmployeeID = 990929 AND IsActive = 1 이런 쿼리문이 EF Core에 의해 자동으로 생성된다.
            */
            
            if (user == null) return null;
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
