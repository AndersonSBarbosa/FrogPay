using AutoMapper;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Repository.Interfaces;
using FrogPay.Repository.Repositories;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels.Endereco;

namespace FrogPay.Services.Services
{

        public class EnderecoService : IEnderecoService
        {
            private readonly IPessoaService _pessoaService;
            private readonly IEnderecoRepository _enderecoRepository;
            private readonly IMapper _mapper;

            public EnderecoService(IPessoaService pessoaService, IEnderecoRepository enderecoRepository, IMapper mapper)
            {
                _pessoaService = pessoaService;
                _enderecoRepository = enderecoRepository;
                _mapper=mapper;
            }

            public async Task<Endereco> CreateEnderecoAsync(CreateEnderecoViewModel endereco)
            {
                try
                {
                    var item = _mapper.Map<Endereco>(endereco);
                        var ItemCreated = await _enderecoRepository.CreateAsync(item);
                    return ItemCreated;
                }

                catch (Exception ex)
                {
                    throw new ApplicationException("Erro ao criar o registro de pessoa.", ex);
                }
            }

            public async Task<Endereco> GetEnderecoByIdPessoa(long id)
            {
                try
                {
                    var endereco = await _enderecoRepository.GetEnderecoByIdPessoaAsync(id);

                    if (endereco == null)
                    {
                        throw new InvalidOperationException("Endereço não encontrado");
                    }

                    return endereco;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Erro ao buscar o endereço pelo ID da pessoa", ex);
                }
            }

            public async Task<Endereco> GetEnderecoByName(string name)
            {
                if (name == null) throw new ArgumentNullException(nameof(name), "Nome não pode ser nulo");

                try
                {
                    var pessoa = await _pessoaService.GetPessoaByNameAsync(name);

                    if (pessoa == null)
                    {
                        throw new InvalidOperationException("Pessoa não encontrada");
                    }

                    return await _enderecoRepository.GetEnderecoByIdPessoaAsync(pessoa.Id);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Erro ao buscar o endereço pelo nome", ex);
                }
            }

            public async Task<Endereco> UpdateEnderecoAsync(UpdateEnderecoViewModel endereco)
            {
                try
                {
                    var itemExists = await GetEnderecoByIdPessoa(endereco.Id);

                    if (itemExists == null)
                        throw new DomainExceptions("não existe usuario com esse ID informado!");

                    var itemUpdate = await _enderecoRepository.UpdateAsync(_mapper.Map<Endereco>(endereco));

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
