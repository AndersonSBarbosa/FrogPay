using FrogPay.Domain.Interfaces;

namespace FrogPay.Domain.Entities
{
    public class DadosBancarios : Base, IAggregateRoot
    {
        public Guid PessoaId { get; set; }
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string DigitoConta { get; set; }


        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
