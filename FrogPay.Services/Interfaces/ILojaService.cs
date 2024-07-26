using FrogPay.Domain.Entities;

namespace FrogPay.Services.Interfaces
{
    public interface ILojaService
    {
        Task<Loja> GetLojaByIdAsync(Guid id);
        Task CreateLojaAsync(Loja loja);
        Task UpdateLojaAsync(Loja loja);
    }
}
