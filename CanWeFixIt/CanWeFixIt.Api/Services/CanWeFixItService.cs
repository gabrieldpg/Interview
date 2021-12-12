using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Services
{
    public class CanWeFixItService : ICanWeFixItService
    {
        private readonly IDatabaseService _datatase;
        private readonly ILogger<CanWeFixItService> _logger;

        public CanWeFixItService(
            IDatabaseService database,
            ILogger<CanWeFixItService> logger)
        {
            _datatase = database;
            _logger = logger;
        }

        public async Task<IEnumerable<Instrument>> GetInstrumentsAsync(bool? active = null)
        {
            _logger.LogInformation($"Executing service: '{GetType().Name}.{nameof(GetInstrumentsAsync)}'. Param active = '{active}'");

            var instruments = await _datatase.GetInstrumentsAsync(active);

            _logger.LogInformation($"Exiting service: '{GetType().Name}.{nameof(GetInstrumentsAsync)}'");

            return instruments;
        }

        public async Task<IEnumerable<MarketDataDto>> GetMarketDataAsync(bool? active = null)
        {
            _logger.LogInformation($"Executing service: '{GetType().Name}.{nameof(GetMarketDataAsync)}'. Param active = '{active}'");

            var marketDataDtoList = new List<MarketDataDto>();

            var marketDataList = await _datatase.GetMarketDataAsync(active);
            foreach (var marketData in marketDataList)
            {
                _logger.LogInformation($"Searching for Instrument with Sedol = '{marketData.Sedol}'");

                var relatedInstrument = await _datatase.GetInstrumentsBySedolAsync(marketData.Sedol);

                if (relatedInstrument != null && relatedInstrument.Any())
                {
                    var instrumentId = relatedInstrument.First().Id;

                    _logger.LogInformation($"Found Instrument with Id = '{instrumentId}'");

                    marketDataDtoList.Add(new MarketDataDto()
                    {
                        Id = marketData.Id,
                        DataValue = marketData.DataValue,
                        InstrumentId = instrumentId,
                        Active = marketData.Active
                    });
                }
            }

            _logger.LogInformation($"Exiting service: '{GetType().Name}.{nameof(GetMarketDataAsync)}'");

            return marketDataDtoList;
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
