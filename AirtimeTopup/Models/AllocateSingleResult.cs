using System;

namespace AirtimeTopup.Models
{
    public class AllocateSingleResult
    {
        public String serial { get; set; }
        public String pin { get; set; }        
        public int unit { get; set; }
        public int fee { get; set; }
        public String type { get; set; }
        public String xref { get; set; }
        public String message { get; set; }
    }
}