using FluentValidation.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FrogPay.Domain.Entities
{
    public abstract class Base
    {
        public int Id { get; set; }


        internal List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors;
        public bool IsValid => _errors.Count == 0;

        protected bool Validate<V, O>(V validator, O obj)
    where V : AbstractValidator<O>
        {
            var validation = validator.Validate(obj);

            if (validation.Errors.Count > 0)
            {
                AddErrorList(validation.Errors);
            }
            return IsValid;
        }

        private void AddErrorList(List<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                _errors.Add(error.ErrorMessage);
            }
        }

        public string ErrosToString()
        {
            var builder = new StringBuilder();

            foreach (var error in _errors)
            {
                builder.AppendLine(error);
            }
            return builder.ToString();
        }
    }
}
