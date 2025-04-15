using RFQ1.Entities;

namespace RFQ1.Repositories.Interface
{
    public interface ITradeRepository
    {
        Task<bool> BookTradeAsync(Trade trade);
    }
}
