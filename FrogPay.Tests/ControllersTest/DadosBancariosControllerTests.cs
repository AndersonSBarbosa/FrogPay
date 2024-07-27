using Bogus;
using FrogPay.API.Controllers;
using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels.ContaBancaria;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FrogPay.Tests.ControllersTest
{
    public class DadosBancariosControllerTests
    {
        private readonly Mock<IDadosBancariosService> _dadosBancariosServiceMock;
        private readonly DadosBancariosController _controller;

        public DadosBancariosControllerTests()
        {
            _dadosBancariosServiceMock = new Mock<IDadosBancariosService>();
            _controller = new DadosBancariosController(_dadosBancariosServiceMock.Object);
        }

        [Fact]
        public async Task BuscarDadosBancariosPorIdPessoa_Existente_DeveRetornarStatusCode200()
        {
            // Arrange
            var pessoaId = new Randomizer().Long(0, 3000);
            var dadosBancarios = new DadosBancarios { PessoaId = pessoaId, Conta = "12345-6" };
            _dadosBancariosServiceMock.Setup(service => service.GetDadosBancariosByIdPessoa(pessoaId)).ReturnsAsync(dadosBancarios);

            // Act
            var resultado = await _controller.GetDadosBancariosByIdPessoa(pessoaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var dadosBancariosRetornados = Assert.IsType<DadosBancarios>(okResult.Value);
            Assert.Equal(pessoaId, dadosBancariosRetornados.PessoaId);
        }

        [Fact]
        public async Task BuscarDadosBancariosPorIdPessoa_NaoEncontrado_DeveRetornarStatusCode404()
        {
            // Arrange
            var pessoaId = new Randomizer().Long(0, 3000);
            _dadosBancariosServiceMock.Setup(service => service.GetDadosBancariosByIdPessoa(pessoaId)).ReturnsAsync((DadosBancarios)null);

            // Act
            var resultado = await _controller.GetDadosBancariosByIdPessoa(pessoaId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task CriarDadosBancarios_Valido_DeveRetornarStatusCode201()
        {
            // Arrange
            var dadosBancarios = new CreateDadosBancariosViewModel { PessoaId = new Randomizer().Long(0, 3000), Conta = "12345-6" };

            // Act
            var resultado = await _controller.CreateDadosBancarios(dadosBancarios);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(resultado);
            var dadosBancariosRetornados = Assert.IsType<DadosBancarios>(createdAtActionResult.Value);
            Assert.Equal(dadosBancarios.PessoaId, dadosBancariosRetornados.PessoaId);
        }

        [Fact]
        public async Task CriarDadosBancarios_Nulo_DeveRetornarStatusCode400()
        {
            // Act
            var resultado = await _controller.CreateDadosBancarios(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async Task AtualizarDadosBancarios_Valido_DeveRetornarStatusCode204()
        {
            // Arrange
            var dadosBancarios = new UpdateDadosBancariosViewModel { Id = new Randomizer().Long(0, 3000), PessoaId = new Randomizer().Long(0, 3000), Conta = "12345-6", Agencia ="0000", CodigoBanco="1111", DigitoConta="1" };

            _dadosBancariosServiceMock.Setup(service => service.UpdateDadosBancariosAsync(dadosBancarios)).Returns((Task<DadosBancarios>)Task.CompletedTask);

            // Act
            var resultado = await _controller.UpdateDadosBancarios(dadosBancarios);

            // Assert
            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public async Task AtualizarDadosBancarios_IdsDivergentes_DeveRetornarStatusCode400()
        {
            // Arrange
            var dadosBancarios = new UpdateDadosBancariosViewModel { Id = new Randomizer().Long(0, 3000),  PessoaId = new Randomizer().Long(0, 3000), Conta = "12345-6" , Agencia ="0000", CodigoBanco="1111", DigitoConta="1" };

            // Act
            var resultado = await _controller.UpdateDadosBancarios(dadosBancarios);

            // Assert
            Assert.IsType<BadRequestObjectResult>(resultado);
        }
    }
}
