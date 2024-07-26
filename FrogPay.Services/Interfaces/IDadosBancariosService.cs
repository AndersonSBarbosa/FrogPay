using FrogPay.Domain.Entities;

namespace FrogPay.Services.Interfaces
{
    public interface IDadosBancariosService
    {
        Task<DadosBancarios> GetDadosBancariosByIdPessoa(Guid id);
        Task CreateDadosBancariosAsync(DadosBancarios dadosBancarios);
        Task UpdateDadosBancariosAsync(DadosBancarios dadosBancarios);
    }
}
