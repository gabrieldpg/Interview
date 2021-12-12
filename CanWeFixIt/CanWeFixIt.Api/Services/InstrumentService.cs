using CanWeFixIt.Api.Services.Interfaces;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Services
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IDatabaseService _datatase;
        private readonly ILogger<InstrumentService> _logger;

        public InstrumentService(
            IDatabaseService database,
            ILogger<InstrumentService> logger)
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
    }
}
