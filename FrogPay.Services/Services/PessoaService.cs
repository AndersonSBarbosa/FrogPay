using FluentValidation;
using FrogPay.Domain.Entities;
using FrogPay.Repository.Interfaces;
using FrogPay.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogPay.Services.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IValidator<Pessoa> _validator;

        public PessoaService(IPessoaRepository pessoaRepository, IValidator<Pessoa> validator)
        {
            _pessoaRepository = pessoaRepository;
            _validator = validator;
        }

        public async Task CreatePessoaAsync(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa));
            }
            if (pessoa.Id == Guid.Empty)
            {
                pessoa.Id = Guid.NewGuid();
            }

            var validation = await _validator.ValidateAsync(pessoa);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            //Garantir a data de nascimento apenas dia/mes/ano
            pessoa.DataNascimento = pessoa.DataNascimento.Date;

            try
            {
                await _pessoaRepository.CreateAsync(pessoa);
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar o registro de pessoa.", ex);
            }
        }

        public async Task<Pessoa> GetPessoaByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID inválido", nameof(id));
            }

            try
            {
                return await _pessoaRepository.GetAsync(id);
            }
            catch (Exception ex)
            {
                // Pode modificar para apresentar um retorno de erro mais detalhado
                throw new ApplicationException("Erro ao buscar a pessoa pelo ID.", ex);
            }
        }

        public async Task<Pessoa> GetPessoaByNameAsync(string name)
        {
            try
            {
                return await _pessoaRepository.GetPessoaByNameAsync(name);
            }
            catch (Exception ex)
            {
                // Pode modificar para apresentar um retorno de erro mais detalhado
                throw new ApplicationException("Erro ao buscar o nome", ex);
            }
        }

        public async Task UpdatePessoaAsync(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa));
            }

            try
            {
                await _pessoaRepository.UpdateAsync(pessoa);
            }
            catch (Exception ex)
            {
                // Pode modificar para apresentar um retorno de erro mais detalhado
                throw new ApplicationException("Erro ao atualizar a pessoa.", ex);
            }
        }
    }
}
