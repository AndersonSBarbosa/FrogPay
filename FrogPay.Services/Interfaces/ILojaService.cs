using FrogPay.Domain.Entities;
using FrogPay.Services.ViewModels.Loja;

namespace FrogPay.Services.Interfaces
{
    public interface ILojaService
    {
        Task<Loja> GetLojaByIdAsync(long id);
        Task<Loja> CreateLojaAsync(CreateLojaViewModel loja);
        Task<Loja> UpdateLojaAsync(UpdateLojaViewModel loja);
    }
}
