using RFQ1.Entities;
using RFQ1.Repositories.Interface;

namespace RFQ1.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private List<Quote> _quotes = new();

        public async Task<Quote> GetQuoteByIdAsync(int quoteId)
        {
            return _quotes.FirstOrDefault(q => q.Id == quoteId);
        }

        public async Task<bool> UpdateQuoteAsync(Quote quote)
        {
            var existingQuote = _quotes.FirstOrDefault(q => q.Id == quote.Id);
            if (existingQuote == null)
            {
                return false; // Quote not found
            }

            existingQuote.IsAccepted = quote.IsAccepted;
            existingQuote.AcceptedTime = quote.AcceptedTime;
            existingQuote.CanceledTime = quote.CanceledTime;

            return true; 
        }

        public async Task SaveQuoteAsync(Quote quote)
        {
            _quotes.Add(quote);
        }

        public async Task<List<Quote>> GetAllQuotesAsync()
        {
            return _quotes.ToList();
        }
    }
}
