﻿using FrogPay.Domain.Interfaces;

namespace FrogPay.Domain.Entities
{
    public class DadosBancarios : Base
    {
        public long PessoaId { get; set; }
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string DigitoConta { get; set; }
    }
}
