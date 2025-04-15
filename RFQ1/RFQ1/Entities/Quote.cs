using RFQ1.Common;
using System;

namespace RFQ1.Entities
{
    public class Quote: BaseEntity
    {
        public string Ticker { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime Time { get; set; }
        public bool IsAccepted { get; set; } 
        public DateTime? AcceptedTime { get; set; }
        public DateTime? CanceledTime { get; set; }
        public Instrument Instrument { get; set; } 
    }
}
