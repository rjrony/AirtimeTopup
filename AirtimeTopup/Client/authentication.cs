using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirtimeTopup.Client.Models;

namespace AirtimeTopup.Client
{
    public class Authentication
    {
        public Customer ClientLogin(string clientId, string clientKey)
        {
            return Repository.Instance.Customers.FirstOrDefault(x => x.ClientId == clientId && x.ClientKey == clientKey);
        }
    }
}
