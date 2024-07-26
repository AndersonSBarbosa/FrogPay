using FrogPay.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogPay.Domain.Entities
{
    public class Endereco : Base, IAggregateRoot
    {
        public Guid PessoaId { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
