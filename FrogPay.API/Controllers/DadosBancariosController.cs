using FrogPay.API.Utilities;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using FrogPay.Services.Services;
using FrogPay.Services.ViewModels;
using FrogPay.Services.ViewModels.ContaBancaria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DadosBancariosController : ControllerBase
    {
        private readonly IDadosBancariosService _dadosBancariosService;

        public DadosBancariosController(IDadosBancariosService dadosBancariosService)
        {
            _dadosBancariosService = dadosBancariosService;
        }

        // GET: api/v1/dadosbancarios/{id}
        [HttpGet]
        public async Task<IActionResult> GetDadosBancariosByIdPessoa(long id)
        {
            try
            {
                var dadosBancarios = await _dadosBancariosService.GetDadosBancariosByIdPessoa(id);
                if (dadosBancarios == null)
                {
                    return NotFound("Dados bancários não encontrados");
                }
                return Ok(dadosBancarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/v1/dadosbancarios
        [HttpPost]
        public async Task<IActionResult> CreateDadosBancarios([FromBody] CreateDadosBancariosViewModel dadosBancarios)
        {
            try
            {
                var itemCreated = await _dadosBancariosService.CreateDadosBancariosAsync(dadosBancarios);
                return Ok(new ResultViewModel
                {
                    Message = "Item criado com sucesso!",
                    Success = true,
                    Data = itemCreated
                });

            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        // PUT: api/v1/dadosbancarios/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateDadosBancarios([FromBody] UpdateDadosBancariosViewModel dadosBancarios)
        {
            try
            {
                var itemUpdate = await _dadosBancariosService.UpdatePessoaAsync(dadosBancarios);

                return Ok(new ResultViewModel
                {
                    Message = "Item atualizado com sucesso!",
                    Success = true,
                    Data = itemUpdate
                });
            }
            catch (DomainExceptions ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}
