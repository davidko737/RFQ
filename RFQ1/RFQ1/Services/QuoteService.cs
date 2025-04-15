using RFQ1.Entities;
using RFQ1.Repositories.Interface;
using RFQ1.Services.Interface;

namespace RFQ1.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IEquityInstrumentRepository _equityRepository;
        private readonly IQuoteRepository _quoteRepository;

        public QuoteService(IEquityInstrumentRepository equityRepository, IQuoteRepository quoteRepository)
        {
            _equityRepository = equityRepository;
            _quoteRepository = quoteRepository;
        }

        public async Task<Quote> RequestQuoteAsync(string ticker, int quantity)
        {
            if (string.IsNullOrWhiteSpace(ticker))
            {
                throw new ArgumentException("Ticker cannot be null or empty.", nameof(ticker));
            }

            var equity = await _equityRepository.GetEquityInstrumentAsync(ticker);
            if (equity == null)
            {
                throw new ArgumentException("Equity instrument not found.", nameof(ticker));
            }

            var quote = new Quote
            {
                Id = GenerateNewQuoteId(), 
                Ticker = ticker,
                BidPrice = 100.0M, 
                AskPrice = 101.0M,
                Quantity = quantity,
                Time = DateTime.UtcNow,
                Instrument = equity
            };

            await _quoteRepository.SaveQuoteAsync(quote);

            return quote;
        }

        public async Task<bool> AcceptQuoteAsync(int quoteId) // Updated to use int
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(quoteId);
            if (quote == null)
            {
                throw new ArgumentException("Quote not found.", nameof(quoteId));
            }

            quote.IsAccepted = true;
            quote.AcceptedTime = DateTime.UtcNow;

            bool updateSuccessful = await _quoteRepository.UpdateQuoteAsync(quote);
            return updateSuccessful; // Return if the update was successful
        }

        public async Task<bool> CancelQuoteAsync(int quoteId) // Updated to use int
        {
            var quote = await _quoteRepository.GetQuoteByIdAsync(quoteId);
            if (quote == null)
            {
                throw new ArgumentException("Quote not found.", nameof(quoteId));
            }

            quote.IsAccepted = false; 
            quote.CanceledTime = DateTime.UtcNow;

            bool updateSuccessful = await _quoteRepository.UpdateQuoteAsync(quote);
            return updateSuccessful; // Return if the update was successful
        }

        public async Task<List<Quote>> GetAllQuotesAsync() 
        {
            return await _quoteRepository.GetAllQuotesAsync(); 
        }

        private int GenerateNewQuoteId()
        {
            return new Random().Next(1, 1000000); 
        }

        
    }
}
