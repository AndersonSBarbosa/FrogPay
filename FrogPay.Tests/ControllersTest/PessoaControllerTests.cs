using Bogus;
using FrogPay.API.Controllers;
using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels.Pessoa;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FrogPay.Tests.ControllersTest
{
    public class PessoaControllerTests
    {
        private readonly Mock<IPessoaService> _pessoaServiceMock;
        private readonly PessoaController _controller;

        public PessoaControllerTests()
        {
            _pessoaServiceMock = new Mock<IPessoaService>();
            _controller = new PessoaController(_pessoaServiceMock.Object);
        }

        [Fact]
        public async Task BuscarPessoaPorId_Existente_DeveRetornarStatusCode200()
        {
            // Arrange
            var pessoaId = new Randomizer().Long(0, 3000);
            var pessoa = new Pessoa { Id = pessoaId, Nome = "Pessoa Teste" };
            _pessoaServiceMock.Setup(service => service.GetPessoaByIdAsync(pessoaId)).ReturnsAsync(pessoa);

            // Act
            var resultado = await _controller.GetPessoaById(pessoaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var pessoaRetornada = Assert.IsType<Pessoa>(okResult.Value);
            Assert.Equal(pessoaId, pessoaRetornada.Id);
        }

        [Fact]
        public async Task BuscarPessoaPorId_NaoEncontrada_DeveRetornarStatusCode404()
        {
            // Arrange
            var pessoaId = new Randomizer().Long(0, 3000);
            _pessoaServiceMock.Setup(service => service.GetPessoaByIdAsync(pessoaId)).ReturnsAsync((Pessoa)null);

            // Act
            var resultado = await _controller.GetPessoaById(pessoaId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task CriarPessoa_Valida_DeveRetornarStatusCode201()
        {
            // Arrange
            var pessoa = new CreatePessoaViewModel { Nome = "Nova Pessoa" };

            // Act
            var resultado = await _controller.CreatePessoa(pessoa);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var pessoaRetornada = Assert.IsType<Pessoa>(createdAtActionResult.Value);
            Assert.Equal(0,pessoaRetornada.Id);
        }

        [Fact]
        public async Task CriarPessoa_Nula_DeveRetornarStatusCode400()
        {
            // Act
            var resultado = await _controller.CreatePessoa(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async Task AtualizarPessoa_Valida_DeveRetornarStatusCode204()
        {
            // Arrange
            var pessoaId = new Randomizer().Long(0, 3000);
            var pessoa = new UpdatePessoaViewModel { Id = pessoaId, Nome = "Pessoa Atualizada" };

            _pessoaServiceMock.Setup(service => service.UpdatePessoaAsync(pessoa)).Returns((Task<Pessoa>)Task.CompletedTask);

            // Act
            var resultado = await _controller.UpdatePessoa(pessoa);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task AtualizarPessoa_IdsDivergentes_DeveRetornarStatusCode400()
        {
            // Arrange
            var pessoaId = new Randomizer().Long(0, 3000);
            var pessoa = new UpdatePessoaViewModel { Id = new Randomizer().Long(0, 3000), Nome = "Pessoa Atualizada" };

            // Act
            var resultado = await _controller.UpdatePessoa(pessoa);

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }
    }
}
