using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
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
        public async Task<IActionResult> GetEnderecoByIdPessoa(Guid id)
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
            if (endereco == null)
            {
                return BadRequest("Dados do endereço são obrigatórios.");
            }

            try
            {
                await _enderecoService.CreateEnderecoAsync(endereco);
                return CreatedAtAction(nameof(GetEnderecoByIdPessoa), new { id = endereco.PessoaId }, endereco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar o endereço: {ex.Message}");
            }
        }

        // PUT: api/v1/endereco/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateEndereco(Guid id, [FromBody] Endereco endereco)
        {
            if (endereco == null || endereco.PessoaId != id)
            {
                return BadRequest("Dados do endereço são inválidos.");
            }

            try
            {
                await _enderecoService.UpdateEnderecoAsync(endereco);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar o endereço: {ex.Message}");
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
