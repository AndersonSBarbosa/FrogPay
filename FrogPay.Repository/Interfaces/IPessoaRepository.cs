using FrogPay.Domain.Entities;

namespace FrogPay.Repository.Interfaces
{
    public interface IPessoaRepository : IBaseRepository<Pessoa>
    {
        Task<Pessoa> GetPessoaByNameAsync(string name);
    }
}
