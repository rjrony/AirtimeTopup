using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirtimeTopup.Client.Models
{
    public class Repository
    {
        private static readonly Repository instance = new Repository();
        public List<Customer> Customers=new List<Customer>(); 
        private Repository()
        {
            var customer = new Customer()
            {
                CustomerId = 1,
                ClientId = "rony",
                ClientKey = "pass",
                Balance = 1000
            };
            this.Customers.Add(customer);

            customer = new Customer()
            {
                CustomerId = 1,
                ClientId = "ahad",
                ClientKey = "pass2",
                Balance = 1500
            };
            this.Customers.Add(customer);
        }

        static Repository()
        {

        }
        public static Repository Instance
        {
            get
            {
                return instance;
            }
        }

        public Customer GetById<T>(long id)
        {
            return this.Customers.FirstOrDefault(x => x.CustomerId == id);
        }


    }
}
