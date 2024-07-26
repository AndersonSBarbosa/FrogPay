using FrogPay.Domain.Entities;
using FrogPay.Repository.Context;
using FrogPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Repository.Repositories
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
    {
        private readonly ManagerContext _context;
        public PessoaRepository(ManagerContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pessoa> GetPessoaByNameAsync(string name)
        {
            try
            {
                var pessoa = await _context.Pessoas
                    .FirstOrDefaultAsync(p => EF.Functions.Like(p.Nome, $"%{name}%"));

                if (pessoa == null)
                {
                    throw new InvalidOperationException("Nome não encontrado.");
                }

                return pessoa;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar a pessoa pelo nome.", ex);
            }
        }
    }
}
