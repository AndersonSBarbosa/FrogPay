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
    public class LojaRepository : BaseRepository<Loja>, ILojaRepository
    {
        private readonly ManagerContext _context;
        public LojaRepository(ManagerContext context) : base(context)
        {
            _context=context;
        }
    }
}
