using AutoMapper;
using FrogPay.API.Utilities;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels;
using FrogPay.Services.ViewModels.Pessoa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public PessoaController(IPessoaService pessoaService, IMapper mapper)
        {
            _pessoaService = pessoaService;
            _mapper=mapper;
        }

        // GET: api/pessoa/{id}
        [HttpGet]
        public async Task<IActionResult> GetPessoaById(long id)
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
        public async Task<IActionResult> CreatePessoa([FromBody] CreatePessoaViewModel pessoa)
        {
            try 
            { 
                var userCreated = await _pessoaService.CreatePessoaAsync(pessoa);
                return Ok(new ResultViewModel
                {
                    Message = "Usuário criado com sucesso!",
                    Success = true,
                    Data = userCreated
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

        // PUT: api/pessoa/{id}
        [HttpPut]
        public async Task<IActionResult> UpdatePessoa([FromBody] UpdatePessoaViewModel pessoa)
        {
            try
            {
                var item = await _pessoaService.UpdatePessoaAsync(pessoa);

                return Ok(new ResultViewModel
                {
                    Message = "usuário atualizado com sucesso!",
                    Success = true,
                    Data = item
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
