using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO.Login;
using SW_MES_API.Services.Login;

namespace SW_MES_API.Controllers.Login
{
    // 클라이언트가 로그인 요청을 보내면 가장 먼저 도착하는 곳
    // 내부적으로 LoginAsync() 호출해서 로그인 성공/실패 판단!!
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;
        
        public AccountController(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        [AllowAnonymous] // 로그인 API는 인증 없이 접근 가능
        //IActionResult는 다양한 HTTP 응답 결과를 반환할 수 있는 ASP.NET Core의 기본 타입
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var user = await _userService.LoginAsync(request.EmployeeID);
            if (user == null || !user.IsActive)
                return Unauthorized(new {message = "사번 또는 계정이 비활성화 상태입니다."});

            var token = _jwtService.GenerateToken(user); // JWT 발급

            return Ok(new {
                message = "로그인 성공",
                token,
                user
            });
        }
    }
}
