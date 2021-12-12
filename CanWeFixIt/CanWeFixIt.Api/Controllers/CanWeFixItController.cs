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
        private readonly ICanWeFixItService _service;
        private readonly ILogger<CanWeFixItController> _logger;

        public CanWeFixItController(
            ICanWeFixItService service,
            ILogger<CanWeFixItController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET Instruments
        [Route("instruments")]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstruments()
        {
            try
            {
                var instruments = await _service.GetInstrumentsAsync(true);
                return Ok(instruments);
            }
            catch (Exception e)
            {
                var errorMessage = $"Error retrieving Instruments. {e.Message}";
                _logger.LogError(errorMessage);

                return BadRequest(errorMessage);
            }
        }

        // GET MarketData
        [Route("marketdata")]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> GetMarketData()
        {
            try
            {
                var marketData = await _service.GetMarketDataAsync(true);
                return Ok(marketData);
            }
            catch (Exception e)
            {
                var errorMessage = $"Error retrieving Market Data. {e.Message}";
                _logger.LogError(errorMessage);

                return BadRequest(errorMessage);
            }
        }

        // GET Valuations
        [Route("valuations")]
        public async Task<ActionResult<IEnumerable<MarketValuation>>> GetValuations()
        {
            try
            {
                var marketValuation = await _service.GetMarketValuationsAsync(true);
                return Ok(marketValuation);
            }
            catch (Exception e)
            {
                var errorMessage = $"Error retrieving Valuations. {e.Message}";
                _logger.LogError(errorMessage);

                return BadRequest(errorMessage);
            }
        }
    }
}
