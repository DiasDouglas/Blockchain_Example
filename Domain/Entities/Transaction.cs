using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public decimal Value { get; set; }

        public Transaction(string origin, string destiny, decimal value)
        {
            Origin = origin;
            Destiny = destiny;
            Value = value;
        }
    }
}
