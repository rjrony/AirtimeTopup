using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirtimeTopup.Client.Models
{
    public class Customer
    {
        public long CustomerId { get; set; }
        public string ClientId { get; set; }
        public string ClientKey { get; set; }
        public double Balance { get; set; }
        public double Discount { get; set; }

    }
}
