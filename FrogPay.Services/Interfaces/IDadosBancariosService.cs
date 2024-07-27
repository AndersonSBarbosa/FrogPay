using FrogPay.Domain.Entities;
using FrogPay.Services.ViewModels.ContaBancaria;

namespace FrogPay.Services.Interfaces
{
    public interface IDadosBancariosService
    {
        Task<DadosBancarios> GetDadosBancariosByIdPessoa(long id);
        Task<DadosBancarios> CreateDadosBancariosAsync(CreateDadosBancariosViewModel dadosBancarios);
        Task<DadosBancarios> UpdateDadosBancariosAsync(UpdateDadosBancariosViewModel dadosBancarios);
    }
}
