using FrogPay.Domain.Entities;

namespace FrogPay.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<Pessoa> GetPessoaByIdAsync(Guid id);
        Task CreatePessoaAsync(Pessoa pessoa);
        Task UpdatePessoaAsync(Pessoa pessoa);
        Task<Pessoa> GetPessoaByNameAsync(string name);
    }
}
