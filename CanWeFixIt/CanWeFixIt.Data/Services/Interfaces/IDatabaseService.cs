﻿using CanWeFixIt.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CanWeFixIt.Data.Services.Interfaces
{
    public interface IDatabaseService
    {
        Task<IEnumerable<Instrument>> GetInstrumentsAsync(bool? active = null);
        Task<IEnumerable<Instrument>> GetInstrumentsBySedolAsync(string sedol);
        Task<IEnumerable<MarketData>> GetMarketDataAsync(bool? active = null);
        void SetupDatabase();
    }
}