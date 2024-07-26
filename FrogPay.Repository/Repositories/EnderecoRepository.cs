using FrogPay.Domain.Entities;
using FrogPay.Repository.Context;
using FrogPay.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogPay.Repository.Repositories
{
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        private readonly ManagerContext _context;
        public EnderecoRepository(ManagerContext context) : base(context)
        {
            _context=context;
        }

        public async Task<Endereco> GetEnderecoByIdPessoaAsync(Guid idPessoa)
        {
            try
            {
                var endereco = await _context.Enderecos.FirstOrDefaultAsync(x => x.PessoaId == idPessoa);

                if (endereco == null)
                {
                    throw new InvalidOperationException("Endereço não encontrado.");
                }

                return endereco;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar o endereço pelo ID da pessoa.", ex);
            }
        }
    }
}
