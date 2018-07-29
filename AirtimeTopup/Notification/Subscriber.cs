using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirtimeTopup.Models;

namespace AirtimeTopup.Notification
{
    public class Subscriber
    {
        Subscription<AirtimeResult> airtimeResultToken;
        EventAggregator eventAggregator;

        public Subscriber(EventAggregator eve)
        {
            this.eventAggregator = eve;
            this.eventAggregator.Subscribe<AirtimeResult>(this.NotifyAdmins);
        }

        private void NotifyAdmins(AirtimeResult result)
        {
            //Console.WriteLine("Notify Admins");

            if (result.Balance < 2000)
            {
                Console.WriteLine("Notify Admins: balance is low");
                //notify admins
            }
            else if (result.Balance < 10)
            {
                Console.WriteLine("Fatal error: Notify Admins");
                //temprary stop gp recharge
                //notify admins
            }
        }

    }
}
