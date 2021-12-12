using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace CanWeFixIt.Api.Services
{
    public class MarketDataService : IMarketDataService
    {
        private readonly IDatabaseService _datatase;
        private readonly ILogger<MarketDataService> _logger;

        public MarketDataService(
            IDatabaseService database,
            ILogger<MarketDataService> logger)
        {
            _datatase = database;
            _logger = logger;
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
    }
}
