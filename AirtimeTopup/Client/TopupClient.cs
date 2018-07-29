using System;
using System.Linq;
using AirtimeTopup.Client.Models;
using AirtimeTopup.Models;

namespace AirtimeTopup.Client
{
    public class TopupClient
    {
        public Customer customer = null;
        private RechargeService rechargeService;
        private readonly TopupService topupService;

        public TopupClient(string clientId, string clientKey)
        {
            var authentication = new Authentication();
            this.customer = authentication.ClientLogin(clientId, clientKey);
            if (this.customer==null)
            {
                //throw exception
            }
            this.topupService = new TopupService(this.customer);
        }

        public TopupService TopupService
        {
            get { return this.topupService; }
        }

    }
}
