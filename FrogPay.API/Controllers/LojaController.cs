using FrogPay.API.Utilities;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using FrogPay.Services.Services;
using FrogPay.Services.ViewModels;
using FrogPay.Services.ViewModels.Loja;
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
        public async Task<IActionResult> GetLojaById(long id)
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
        public async Task<IActionResult> CreateLoja([FromBody] CreateLojaViewModel loja)
        {
            try
            {
                var itemCreated = await _lojaService.CreateLojaAsync(loja);
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

        // PUT: api/loja
        [HttpPut]
        public async Task<IActionResult> UpdateLoja([FromBody] UpdateLojaViewModel loja)
        {
            try
            {
                var itemUpdate = await _lojaService.UpdateLojaAsync(loja);

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
