using RFQ1.Common;
using System;

namespace RFQ1.Entities
{
    public class Trade: BaseEntity
    {
        public string Ticker { get; set; } 
        public int Quantity { get; set; }
        public decimal Price { get; set; } 
        public DateTime Time { get; set; }

        public Trade()
        {
            Time = DateTime.UtcNow;
        }
    }
}
