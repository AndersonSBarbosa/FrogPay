using FrogPay.API.Controllers;
using FrogPay.Domain.Entities;
using Moq;
using Xunit;

namespace FrogPay.Tests.ControllersTest
{
    public class AuthenticatorControllerTests
    {
        private readonly AuthenticatorController _controller;
        private readonly Mock<Microsoft.Extensions.Configuration.IConfiguration> _configMock;

        public AuthenticatorControllerTests()
        {
            _configMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _configMock.Setup(config => config["Jwt:Key"]).Returns("dXzU3f7hG9mJc5kLq9vYz2wB8nP1sT3eW9xC7dQ4aR5vL1oM6z");
            _configMock.Setup(config => config["Jwt:Issuer"]).Returns("FrogPayAuthServer");
            _configMock.Setup(config => config["Jwt:Audience"]).Returns("FrogPayUsers");
            _configMock.Setup(config => config["Jwt:DurationInMinutes"]).Returns("5");

            _controller = new AuthenticatorController(_configMock.Object);
        }

        [Fact]
        public void GenerateToken_ValidUser_ReturnsToken()
        {
            // Arrange
            var userLogin = new Login { Username = "admin", Password = "password" };

            // Act
            var result = _controller.GenerateToken(userLogin);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateToken_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var userLogin = new Login { Username = "invalid", Password = "invalid" };

            // Act
            var result = _controller.GenerateToken(userLogin);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.UnauthorizedResult>(result);
        }
    }
}
