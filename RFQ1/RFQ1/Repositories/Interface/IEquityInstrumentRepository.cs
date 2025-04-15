using RFQ1.Entities;

namespace RFQ1.Repositories.Interface
{
    public interface IEquityInstrumentRepository
    {
        Task<EquityInstrument> GetEquityInstrumentAsync(string ticker);
    }
}
