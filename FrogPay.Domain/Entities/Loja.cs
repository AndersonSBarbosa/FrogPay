﻿using FrogPay.Domain.Interfaces;

namespace FrogPay.Domain.Entities
{
    public class Loja : Base, IAggregateRoot
    {
        public Guid PessoaId { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public DateTime DataAbertura { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
