namespace FrogPay.Services.ViewModels.Loja
{
    public class UpdateLojaViewModel
    {
        public long Id { get; set; }
        public long PessoaId { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
    }
}
