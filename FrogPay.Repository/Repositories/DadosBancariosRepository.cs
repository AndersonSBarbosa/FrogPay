using FrogPay.Domain.Entities;
using FrogPay.Repository.Context;
using FrogPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Repository.Repositories
{
    public class DadosBancariosRepository : BaseRepository<DadosBancarios>, IDadosBancariosRepository
    {
        private readonly ManagerContext _context;
        public DadosBancariosRepository(ManagerContext context) : base(context)
        {
            _context=context;
        }

        public async Task<DadosBancarios> GetDadosBancariosByIdPessoaAsync(long idPessoa)
        {
            try
            {
                var dadosBancarios = await _context.DadosBancario
                    .FirstOrDefaultAsync(x => x.PessoaId == idPessoa);

                if (dadosBancarios == null)
                {
                    throw new InvalidOperationException("Dados bancários não encontrados.");
                }

                return dadosBancarios;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar os dados bancários pelo ID da pessoa.", ex);
            }
        }
    }
}
