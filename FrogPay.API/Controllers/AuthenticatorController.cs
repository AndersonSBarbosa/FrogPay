using FrogPay.API.Token;
using FrogPay.API.Utilities;
using FrogPay.Domain.Entities;
using FrogPay.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace FrogPay.API.Controllers
{
    public class AuthenticatorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticatorController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator=tokenGenerator;
        }

        [HttpPost]
        [Route("/api/auth/login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (IsValidUser(login))
            {
                
                return Ok(new ResultViewModel
                {
                    Message = "Usuário autenticado com sucesso!",
                    Success = true,
                    Data = new
                    {
                        Token = _tokenGenerator.GenerateToken(),
                        TokenExpires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]))
                    }
                });

            }
            else
            {
                return StatusCode(401, Responses.UnauthorizedErrorMessage());
            }
            
        }

        private bool IsValidUser(Login login)
        {
            // Validação do usuário (ex: verificar credenciais no banco de dados)
            return login.Username == "admin" && login.Password == "dunha";
        }
    }
}
