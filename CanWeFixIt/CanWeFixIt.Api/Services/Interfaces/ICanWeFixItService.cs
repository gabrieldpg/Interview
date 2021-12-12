using CanWeFixIt.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Services.Interfaces
{
    public interface ICanWeFixItService
    {
        Task<IEnumerable<Instrument>> GetInstrumentsAsync(bool? active = null);
        Task<IEnumerable<MarketDataDto>> GetMarketDataAsync(bool? active = null);
        Task<IEnumerable<MarketValuation>> GetMarketValuationsAsync(bool? active = null);
    }
}
