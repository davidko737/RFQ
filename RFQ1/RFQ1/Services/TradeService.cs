using RFQ1.Entities;
using RFQ1.Repositories.Interface;
using RFQ1.Services.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RFQ1.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository; // Repository for trade operations

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public async Task<bool> BookTradeAsync(Quote quote)
        {
            // Validate the quote to ensure it is not null and contains valid data
            if (quote == null || string.IsNullOrWhiteSpace(quote.Ticker))
            {
                throw new ArgumentException("Invalid quote provided for booking the trade.");
            }

            // TODO: Should have validation logics, like duplicate trade 

            var trade = new Trade
            {
                Ticker = quote.Ticker,
                Quantity = quote.Quantity,
                Price = quote.AskPrice, // Assuming trade booked at ask price
                Time = DateTime.UtcNow
            };

            // Save the trade to the repository
            await _tradeRepository.BookTradeAsync(trade);

            return true; // Trade booked successfully
        }
    }
}
