using RFQ1.Entities;
using RFQ1.Repositories.Interface;

namespace RFQ1.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private List<Trade> _tradedInstruments = new();

        public async Task<bool> BookTradeAsync(Trade trade)
        {
            _tradedInstruments.Add(trade);
            return true; // Indicate success
        }
    }
}
