using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        // GET: api/pessoa/{id}
        [HttpGet]
        public async Task<IActionResult> GetPessoaById(Guid id)
        {
            try
            {
                var pessoa = await _pessoaService.GetPessoaByIdAsync(id);
                if (pessoa == null)
                {
                    return NotFound("Pessoa não encontrada");
                }
                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro : {ex.Message}");
            }
        }

        // POST: api/pessoa
        [HttpPost]
        public async Task<IActionResult> CreatePessoa([FromBody] Pessoa pessoa)
        {
            if (pessoa == null)
            {
                return BadRequest("Dados da pessoa são obrigatórios");
            }

            try
            {
                await _pessoaService.CreatePessoaAsync(pessoa);
                return CreatedAtAction(nameof(GetPessoaById), new { id = pessoa.Id }, pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar a pessoa: {ex.Message}");
            }
        }

        // PUT: api/pessoa/{id}
        [HttpPut]
        public async Task<IActionResult> UpdatePessoa(Guid id, [FromBody] Pessoa pessoa)
        {
            if (pessoa == null || pessoa.Id != id)
            {
                return BadRequest("Dados Invalidos");
            }

            try
            {
                await _pessoaService.UpdatePessoaAsync(pessoa);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar: {ex.Message}");
            }
        }

        // GET: api/pessoa/name/{name}
        [HttpGet("{name}")]
        public async Task<IActionResult> GetPessoaByName(string name)
        {
            try
            {
                var pessoa = await _pessoaService.GetPessoaByNameAsync(name);
                if (pessoa == null)
                {
                    return NotFound("Pessoa não encontrada");
                }
                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }
    }
}
