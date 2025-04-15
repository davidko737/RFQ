using RFQ1.Entities;

namespace RFQ1.Repositories.Interface
{
    public interface IQuoteRepository
    {
        Task<Quote> GetQuoteByIdAsync(int quoteId);
        Task<bool> UpdateQuoteAsync(Quote quote);
        Task SaveQuoteAsync(Quote quote);
        Task<List<Quote>> GetAllQuotesAsync();
    }
}
