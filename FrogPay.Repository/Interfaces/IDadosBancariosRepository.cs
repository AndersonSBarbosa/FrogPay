using FrogPay.Domain.Entities;

namespace FrogPay.Repository.Interfaces
{
    public interface IDadosBancariosRepository : IBaseRepository<DadosBancarios>
    {
        Task<DadosBancarios> GetDadosBancariosByIdPessoaAsync(Guid idPessoa);
    }
}
