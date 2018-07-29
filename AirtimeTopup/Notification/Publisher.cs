using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirtimeTopup.Notification
{
    public class Publisher
    {
        EventAggregator EventAggregator;
        public Publisher(EventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public void PublishMessage()
        {
            //EventAggregator.Publish(new Mymessage());
            //EventAggregator.Publish(10);
        }
    }
}
