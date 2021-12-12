using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace CanWeFixIt.Api.Services
{
    public class ValuationService : IValuationService
    {
        private readonly IDatabaseService _datatase;
        private readonly ILogger<ValuationService> _logger;

        public ValuationService(
            IDatabaseService database,
            ILogger<ValuationService> logger)
        {
            _datatase = database;
            _logger = logger;
        }

        public async Task<IEnumerable<MarketValuation>> GetMarketValuationsAsync(bool? active = null)
        {
            _logger.LogInformation($"Executing service: '{GetType().Name}.{nameof(GetMarketValuationsAsync)}'. Param active = '{active}'");

            var marketDataList = await _datatase.GetMarketDataAsync(active);

            var marketValuationList = new List<MarketValuation>()
            {
                new MarketValuation()
                {
                    Total = marketDataList.Any() ? marketDataList.Sum(x => x.DataValue) : 0
                }
            };

            _logger.LogInformation($"Exiting service: '{GetType().Name}.{nameof(GetMarketValuationsAsync)}'");

            return marketValuationList;
        }
    }
}
