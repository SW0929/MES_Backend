using Microsoft.IdentityModel.Tokens;
using SW_MES_API.DTO.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SW_MES_API.Services.Login
{
    public class JwtService
    {
        // JWT 토큰을 생성하는 서비스
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(LoginResponseDTO user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.EmployeeID.ToString()),
            new Claim("name", user.Name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("isActive", user.IsActive.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // 토큰 ID
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),  // 유효 시간
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
