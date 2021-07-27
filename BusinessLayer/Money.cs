using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Money : IMoney
    {
        public Money(string currency, decimal amount)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }

        public string Currency { get; }
    }

}
