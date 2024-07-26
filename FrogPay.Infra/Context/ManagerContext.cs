using FrogPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrogPay.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        { }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        { }

        public virtual DbSet<DadosBancarios> DadosBancarios { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Loja> Loja { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }


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
