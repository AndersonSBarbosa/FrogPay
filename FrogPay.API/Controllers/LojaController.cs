using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LojaController : ControllerBase
    {
        private readonly ILojaService _lojaService;

        public LojaController(ILojaService lojaService)
        {
            _lojaService = lojaService;
        }

        // GET: api/loja
        [HttpGet]
        public async Task<IActionResult> GetLojaById(Guid id)
        {
            try
            {
                var loja = await _lojaService.GetLojaByIdAsync(id);
                if (loja == null)
                {
                    return NotFound("Loja não encontrada");
                }
                return Ok(loja);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/loja
        [HttpPost]
        public async Task<IActionResult> CreateLoja([FromBody] Loja loja)
        {
            if (loja == null)
            {
                return BadRequest("Dados da loja são obrigatórios");
            }

            try
            {
                await _lojaService.CreateLojaAsync(loja);
                return CreatedAtAction(nameof(GetLojaById), new { id = loja.Id }, loja);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar a loja: {ex.Message}");
            }
        }

        // PUT: api/loja
        [HttpPut]
        public async Task<IActionResult> UpdateLoja(Guid id, [FromBody] Loja loja)
        {
            if (loja == null || loja.Id != id)
            {
                return BadRequest("Dados da loja são inválidos");
            }

            try
            {
                await _lojaService.UpdateLojaAsync(loja);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar a loja: {ex.Message}");
            }
        }
    }
}
