using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
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
        public async Task<IActionResult> GetDadosBancariosByIdPessoa(Guid id)
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
        public async Task<IActionResult> CreateDadosBancarios([FromBody] DadosBancarios dadosBancarios)
        {
            if (dadosBancarios == null)
            {
                return BadRequest("Dados bancários são obrigatórios");
            }

            try
            {
                await _dadosBancariosService.CreateDadosBancariosAsync(dadosBancarios);
                return CreatedAtAction(nameof(GetDadosBancariosByIdPessoa), new { id = dadosBancarios.PessoaId }, dadosBancarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar os dados bancários: {ex.Message}");
            }
        }

        // PUT: api/v1/dadosbancarios/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateDadosBancarios(Guid id, [FromBody] DadosBancarios dadosBancarios)
        {
            if (dadosBancarios == null || dadosBancarios.PessoaId != id)
            {
                return BadRequest("Dados bancários são inválidos");
            }

            try
            {
                await _dadosBancariosService.UpdateDadosBancariosAsync(dadosBancarios);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar os dados bancários: {ex.Message}");
            }
        }
    }
}
