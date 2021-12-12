using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CanWeFixItController : ControllerBase
    {
        private readonly IInstrumentService _instrumentService;
        private readonly IMarketDataService _marketDataService;
        private readonly IValuationService _valuationService;

        private readonly ILogger<CanWeFixItController> _logger;

        public CanWeFixItController(
            IInstrumentService instrumentService,
            IMarketDataService marketDataService,
            IValuationService valuationService,
            ILogger<CanWeFixItController> logger)
        {
            _instrumentService = instrumentService;
            _marketDataService = marketDataService;
            _valuationService = valuationService;
            _logger = logger;
        }

        // GET Instruments
        [Route("instruments")]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstruments()
        {
            try
            {
                var instruments = await _instrumentService.GetInstrumentsAsync(true);
                return Ok(instruments);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error retrieving Instruments. {e.Message}", e);

                return BadRequest(); // Implement some sort of error handling
            }
        }

        // GET MarketData
        [Route("marketdata")]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> GetMarketData()
        {
            try
            {
                var marketData = await _marketDataService.GetMarketDataAsync(true);
                return Ok(marketData);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error retrieving Market Data. {e.Message}", e);

                return BadRequest(); // Implement some sort of error handling
            }
        }

        // GET Valuations
        [Route("valuations")]
        public async Task<ActionResult<IEnumerable<MarketValuation>>> GetValuations()
        {
            try
            {
                var marketValuation = await _valuationService.GetMarketValuationsAsync(true);
                return Ok(marketValuation);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error retrieving Valuations. {e.Message}", e);

                return BadRequest(); // Implement some sort of error handling
            }
        }
    }
}
