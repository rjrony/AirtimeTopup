using System;

namespace AirtimeTopup.Models
{
    public class CheckResult
    {
        public String id { get; set; }
        public String msisdn { get; set; }
        public String net { get; set; }
        public int amount { get; set; }
        public String xref { get; set; }
        public DateTime stamp { get; set; }
    }
}