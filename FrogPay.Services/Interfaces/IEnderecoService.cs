using FrogPay.Domain.Entities;
using FrogPay.Services.ViewModels.Endereco;

namespace FrogPay.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> GetEnderecoByIdPessoa(long id);
        Task<Endereco> GetEnderecoByName(string name);
        Task<Endereco> CreateEnderecoAsync(CreateEnderecoViewModel endereco);
        Task<Endereco> UpdateEnderecoAsync(UpdateEnderecoViewModel endereco);
    }
}
