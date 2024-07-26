using FrogPay.Domain.Entities;

namespace FrogPay.Repository.Interfaces
{
    public interface IEnderecoRepository : IBaseRepository<Endereco>
    {
        Task<Endereco> GetEnderecoByIdPessoaAsync(Guid idPessoa);
    }
}
