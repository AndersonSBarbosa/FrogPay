﻿namespace FrogPay.Services.ViewModels.Endereco
{
    public class CreateEnderecoViewModel
    {
        public long PessoaId { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}