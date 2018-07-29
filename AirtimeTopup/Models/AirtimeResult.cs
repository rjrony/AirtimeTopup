using System;

namespace AirtimeTopup.Models
{
    public class AirtimeResult
    {
        public string Id { get; set; }
        public double Balance { get; set; }
        public int Amount { get; set; }
        public double Charge { get; set; }
        public string Message { get; set; }
        public int ResultCode { get; set; }
    }
}