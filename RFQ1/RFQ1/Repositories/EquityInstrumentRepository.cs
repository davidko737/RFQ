using RFQ1.Entities;
using RFQ1.Repositories.Interface;

namespace RFQ1.Repositories
{
    public class EquityInstrumentRepository : IEquityInstrumentRepository
    {
        private Dictionary<string, EquityInstrument> _equityDataStore = new();

        public EquityInstrumentRepository()
        {
            // Initializing with some in-memory data
            _equityDataStore["AAPL"] = new EquityInstrument { Ticker = "AAPL", CompanyName = "Apple Inc." };
            _equityDataStore["GOOGL"] = new EquityInstrument { Ticker = "GOOGL", CompanyName = "Alphabet Inc." };
        }

        public Task<EquityInstrument> GetEquityInstrumentAsync(string ticker)
        {
            _equityDataStore.TryGetValue(ticker, out var instrument);
            return Task.FromResult(instrument);
        }
    }
}
