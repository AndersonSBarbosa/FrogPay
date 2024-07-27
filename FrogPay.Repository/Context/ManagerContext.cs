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

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<DadosBancarios> DadosBancario { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Loja> Loja { get; set; }

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
