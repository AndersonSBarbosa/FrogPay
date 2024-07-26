using FrogPay.Domain.Entities;

namespace FrogPay.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> GetEnderecoByIdPessoa(Guid id);
        Task<Endereco> GetEnderecoByName(string name);
        Task CreateEnderecoAsync(Endereco endereco);
        Task UpdateEnderecoAsync(Endereco endereco);
    }
}
