using FrogPay.API.Utilities;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using FrogPay.Services.Services;
using FrogPay.Services.ViewModels;
using FrogPay.Services.ViewModels.Endereco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        // GET: api/v1/endereco/{id}
        [HttpGet]
        public async Task<IActionResult> GetEnderecoByIdPessoa(long id)
        {
            try
            {
                var endereco = await _enderecoService.GetEnderecoByIdPessoa(id);
                if (endereco == null)
                {
                    return NotFound("Endereço não encontrado.");
                }
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/v1/endereco
        [HttpPost]
        public async Task<IActionResult> CreateEndereco([FromBody] Endereco endereco)
        {
            try
            {
                var itemCreated = await _enderecoService.CreateEnderecoAsync(endereco);
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

        // PUT: api/v1/endereco/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateEndereco([FromBody] UpdateEnderecoViewModel endereco)
        {
            try
            {
                var itemUpdate = await _enderecoService.UpdateEnderecoAsync(endereco);

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

        // GET: api/v1/endereco/name/{name}
        [HttpGet("{name}")]
        public async Task<IActionResult> GetEnderecoByName(string name)
        {
            try
            {
                var endereco = await _enderecoService.GetEnderecoByName(name);
                if (endereco == null)
                {
                    return NotFound("Endereço não encontrado.");
                }
                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
