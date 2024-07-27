using Bogus;
using FrogPay.API.Controllers;
using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels.Loja;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FrogPay.Tests.ControllersTest
{
    public class LojaControllerTests
    {
        private readonly Mock<ILojaService> _lojaServiceMock;
        private readonly LojaController _controller;

        public LojaControllerTests()
        {
            _lojaServiceMock = new Mock<ILojaService>();
            _controller = new LojaController(_lojaServiceMock.Object);
        }

        [Fact]
        public async Task BuscarLojaPorId_Existente_DeveRetornarStatusCode200()
        {
            // Arrange
            var lojaId = new Randomizer().Long(0, 3000);
            var loja = new Loja { Id = lojaId, NomeFantasia = "Loja Teste" };
            _lojaServiceMock.Setup(service => service.GetLojaByIdAsync(lojaId)).ReturnsAsync(loja);

            // Act
            var resultado = await _controller.GetLojaById(lojaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var lojaRetornada = Assert.IsType<Loja>(okResult.Value);
            Assert.Equal(lojaId, lojaRetornada.Id);
        }

        [Fact]
        public async Task BuscarLojaPorId_NaoEncontrada_DeveRetornarStatusCode404()
        {
            // Arrange
            var lojaId = new Randomizer().Long(0, 3000);
            _lojaServiceMock.Setup(service => service.GetLojaByIdAsync(lojaId)).ReturnsAsync((Loja)null);

            // Act
            var resultado = await _controller.GetLojaById(lojaId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task CriarLoja_Valida_DeveRetornarStatusCode201()
        {
            // Arrange
            var loja = new CreateLojaViewModel { NomeFantasia = "Nova Loja", PessoaId = new Randomizer().Long(0, 3000), RazaoSocial = "Razao Social", Cnpj = "12345678901234" };

            // Act
            var resultado = await _controller.CreateLoja(loja);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var lojaRetornada = Assert.IsType<Loja>(createdAtActionResult.Value);
            Assert.Equal(0,lojaRetornada.Id);
        }

        [Fact]
        public async Task CriarLoja_Nula_DeveRetornarStatusCode400()
        {
            // Act
            var resultado = await _controller.CreateLoja(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async Task AtualizarLoja_Valida_DeveRetornarStatusCode204()
        {
            // Arrange
            var lojaId = new Randomizer().Long(0, 3000);
            var loja = new UpdateLojaViewModel { Id = lojaId, NomeFantasia = "Loja Atualizada", PessoaId = new Randomizer().Long(0, 3000), RazaoSocial = "Razao Social Atualizada", Cnpj = "12345678901234" };

            _lojaServiceMock.Setup(service => service.UpdateLojaAsync(loja)).Returns((Task<Loja>)Task.CompletedTask);

            // Act
            var resultado = await _controller.UpdateLoja(loja);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task AtualizarLoja_IdsDivergentes_DeveRetornarStatusCode400()
        {
            // Arrange
            var lojaId = new Randomizer().Long(0, 3000);
            var loja = new UpdateLojaViewModel { Id = new Randomizer().Long(0, 3000), NomeFantasia = "Loja Atualizada", PessoaId = new Randomizer().Long(0, 3000), RazaoSocial = "Razao Social Atualizada", Cnpj = "12345678901234" };

            // Act
            var resultado = await _controller.UpdateLoja(loja);

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }
    }
}
