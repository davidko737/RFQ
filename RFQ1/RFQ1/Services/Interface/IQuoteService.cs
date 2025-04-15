using RFQ1.Entities;

namespace RFQ1.Services.Interface
{
    public interface IQuoteService
    {
        Task<Quote> RequestQuoteAsync(string ticker, int quantity);
        Task<bool> AcceptQuoteAsync(int quoteId);
        Task<bool> CancelQuoteAsync(int quoteId);
        Task<List<Quote>> GetAllQuotesAsync(); 
    }
}
