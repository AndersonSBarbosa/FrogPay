using FrogPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FrogPay.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticatorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] Login login)
        {
            if (IsValidUser(login))
            {
                var token = GenerateJwtToken();
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private bool IsValidUser(Login login)
        {
            // Validação do usuário (ex: verificar credenciais no banco de dados)
            return login.Username == "admin" && login.Password == "password";
        }

        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
