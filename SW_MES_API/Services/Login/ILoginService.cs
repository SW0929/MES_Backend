using SW_MES_API.DTO.Login;

namespace SW_MES_API.Services.Login
{
    public interface ILoginService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
    }
}
