using FrogPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Repository.Context
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        { }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<DadosBancarios> DadosBancarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Loja> Lojas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DadosBancarios>()
                .HasOne<Pessoa>()
                .WithMany(p => p.DadosBancario)
                .HasForeignKey(d => d.PessoaId);

            modelBuilder.Entity<Endereco>()
                .HasOne<Pessoa>()
                .WithMany(p => p.Endereco)
                .HasForeignKey(e => e.PessoaId);

            modelBuilder.Entity<Loja>()
                .HasOne<Pessoa>()
                .WithMany(p => p.Loja)
                .HasForeignKey(l => l.PessoaId);
        }



    }
}
