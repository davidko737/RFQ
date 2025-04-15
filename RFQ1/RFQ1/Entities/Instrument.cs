using RFQ1.Common;

namespace RFQ1.Entities
{
    public class Instrument: BaseEntity
    {
        public string Ticker { get; set; }

        public static implicit operator Instrument(Task<EquityInstrument> v)
        {
            throw new NotImplementedException();
        }
    }
}
