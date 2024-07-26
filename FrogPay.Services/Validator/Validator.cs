using FluentValidation;
using FrogPay.Domain.Entities;

namespace FrogPay.Services.Validator
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.");

            RuleFor(p => p.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("CPF inválido. Deve conter 11 dígitos.");

            RuleFor(p => p.DataNascimento)
                .NotEmpty().WithMessage("Data de Nascimento é obrigatória.")
                .Must(data => data.TimeOfDay == TimeSpan.Zero).WithMessage("Data de Nascimento deve conter apenas dia/mês/ano.");
        }
    }
}
