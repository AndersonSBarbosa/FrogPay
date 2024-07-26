using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogPay.Services.ViewModels
{
    public class ResultViewModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public dynamic Data { get; set; }
    }
}
