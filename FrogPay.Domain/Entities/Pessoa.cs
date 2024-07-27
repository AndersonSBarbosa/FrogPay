using FrogPay.Domain.Interfaces;

namespace FrogPay.Domain.Entities
{
    public class Pessoa : Base
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<DadosBancarios> DadosBancario { get; set; }
        public ICollection<Endereco> Endereco { get; set; }
        public ICollection<Loja> Loja { get; set; }
    }
}
