using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogPay.Domain.Interfaces
{
    public interface IAggregateRoot
    {
        bool Validate();
    }
}
