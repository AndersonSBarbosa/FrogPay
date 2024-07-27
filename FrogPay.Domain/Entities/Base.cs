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
        public long Id { get; set; }
    }
}
