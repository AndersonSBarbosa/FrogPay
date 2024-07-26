using FrogPay.Domain.Entities;
using FrogPay.Repository.Interfaces;
using FrogPay.Services.Interfaces;

namespace FrogPay.Services.Services
{
    public class DadosBancariosService : IDadosBancariosService
    {
        private readonly IDadosBancariosRepository _dadosBancariosRepository;

        public DadosBancariosService(IDadosBancariosRepository dadosBancariosRepository)
        {
            _dadosBancariosRepository = dadosBancariosRepository;
        }

        public async Task CreateDadosBancariosAsync(DadosBancarios dadosBancarios)
        {
            if (dadosBancarios == null)
            {
                throw new ArgumentNullException(nameof(dadosBancarios), "Dados bancários não podem ser nulos");
            }

            try
            {
                await _dadosBancariosRepository.CreateAsync(dadosBancarios);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar os dados bancários", ex);
            }
        }

        public async Task<DadosBancarios> GetDadosBancariosByIdPessoa(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID inválido", nameof(id));
            }

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

        public async Task UpdateDadosBancariosAsync(DadosBancarios dadosBancarios)
        {
            if (dadosBancarios == null)
            {
                throw new ArgumentNullException(nameof(dadosBancarios), "Dados bancários não podem ser nulos");
            }

            try
            {
                await _dadosBancariosRepository.UpdateAsync(dadosBancarios);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao atualizar os dados bancários", ex);
            }
        }
    }
}
