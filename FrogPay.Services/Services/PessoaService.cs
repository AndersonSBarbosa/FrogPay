using AutoMapper;
using FluentValidation;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Repository.Interfaces;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels.Pessoa;

namespace FrogPay.Services.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IMapper _mapper;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IValidator<Pessoa> _validator;

        public PessoaService(IPessoaRepository pessoaRepository, IValidator<Pessoa> validator, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _validator = validator;
            _mapper=mapper;
        }

        public async Task<Pessoa> CreatePessoaAsync(CreatePessoaViewModel pessoa)
        {           
            //Garantir a data de nascimento apenas dia/mes/ano
            pessoa.DataNascimento = pessoa.DataNascimento.Date;

            try
            {
                var item = _mapper.Map<Pessoa>(pessoa);
                var ItemCreated =await _pessoaRepository.CreateAsync(item);
                return ItemCreated;
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar o registro de pessoa.", ex);
            }
        }

        public async Task<Pessoa> GetPessoaByIdAsync(long id)
        {
            //if (id == Guid.Empty)
            //{
            //    throw new ArgumentException("ID inválido", nameof(id));
            //}

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

        public async Task<Pessoa> UpdatePessoaAsync(UpdatePessoaViewModel pessoa)
        {
            try
            {
                var itemExists = await GetPessoaByIdAsync(pessoa.Id);

                if (itemExists == null)
                    throw new DomainExceptions("não existe usuario com esse ID informado!");

                var itemUpdate = await _pessoaRepository.UpdateAsync(_mapper.Map<Pessoa>(pessoa));

                return itemUpdate;
            }
            catch (Exception ex)
            {
                // Pode modificar para apresentar um retorno de erro mais detalhado
                throw new ApplicationException("Erro ao atualizar a pessoa.", ex);
            }
        }
    }
}
