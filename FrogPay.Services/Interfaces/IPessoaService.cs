using FrogPay.Domain.Entities;
using FrogPay.Services.ViewModels.Pessoa;

namespace FrogPay.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<Pessoa> GetPessoaByIdAsync(long id);
        Task<Pessoa> CreatePessoaAsync(CreatePessoaViewModel pessoa);
        Task<Pessoa> UpdatePessoaAsync(UpdatePessoaViewModel pessoa);
        Task<Pessoa> GetPessoaByNameAsync(string name);
    }
}
