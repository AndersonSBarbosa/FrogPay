﻿namespace FrogPay.Services.ViewModels.ContaBancaria
{
    public class UpdateDadosBancariosViewModel
    {
        public long Id { get; set; }
        public long PessoaId { get; set; }
        public string CodigoBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string DigitoConta { get; set; }
    }
}