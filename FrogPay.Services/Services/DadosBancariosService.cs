using AutoMapper;
using FrogPay.Core.Exceptions;
using FrogPay.Domain.Entities;
using FrogPay.Repository.Interfaces;
using FrogPay.Services.Interfaces;
using FrogPay.Services.ViewModels.ContaBancaria;

namespace FrogPay.Services.Services
{
    public class DadosBancariosService : IDadosBancariosService
    {
        private readonly IDadosBancariosRepository _dadosBancariosRepository;
        private readonly IMapper _mapper;

        public DadosBancariosService(IDadosBancariosRepository dadosBancariosRepository, IMapper mapper)
        {
            _dadosBancariosRepository = dadosBancariosRepository;
            _mapper=mapper;
        }

        public async Task<DadosBancarios> CreateDadosBancariosAsync(CreateDadosBancariosViewModel dadosBancarios)
        {
            try
            {
                var item = _mapper.Map<DadosBancarios>(dadosBancarios);
                var ItemCreated = await _dadosBancariosRepository.CreateAsync(item);
                return ItemCreated;
            }

            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar o registro do Dados Bancarios.", ex);
            }
        }

        public async Task<DadosBancarios> GetDadosBancariosByIdPessoa(long id)
        {
            try
            {
                var dadosBancarios = await _dadosBancariosRepository.GetDadosBancariosByIdPessoaAsync(id);

                if (dadosBancarios == null)
                {
                    throw new InvalidOperationException("Dados bancários não encontrados");
                }

                return dadosBancarios;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar os dados bancários pelo ID da pessoa", ex);
            }
        }

        public async Task<DadosBancarios> UpdateDadosBancariosAsync(UpdateDadosBancariosViewModel dadosBancarios)
        {
            try
            {
                var itemExists = await GetDadosBancariosByIdPessoa(dadosBancarios.Id);

                if (itemExists == null)
                    throw new DomainExceptions("não existe Dados Bancarios com esse ID informado!");

                var itemUpdate = await _dadosBancariosRepository.UpdateAsync(_mapper.Map<DadosBancarios>(dadosBancarios));

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
