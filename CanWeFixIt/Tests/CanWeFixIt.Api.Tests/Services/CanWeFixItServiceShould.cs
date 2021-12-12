using CanWeFixIt.Api.Services;
using CanWeFixIt.Data.Models;
using CanWeFixIt.Data.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CanWeFixIt.Api.Tests.Services
{
    public class CanWeFixItServiceShould
    {
        private readonly CanWeFixItService _service;
        private readonly Mock<IDatabaseService> _databaseMock;
        private readonly Mock<ILogger<CanWeFixItService>> _loggerMock;

        private IEnumerable<Instrument> _instruments;
        private IEnumerable<MarketData> _marketDataList;
        private IEnumerable<MarketDataDto> _marketDataDtoList;
        private IEnumerable<MarketValuation> _marketValuations;

        public CanWeFixItServiceShould()
        {
            _databaseMock = new Mock<IDatabaseService>();
            _loggerMock = new Mock<ILogger<CanWeFixItService>>();

            _service = new CanWeFixItService(
                _databaseMock.Object,
                _loggerMock.Object);

            SetupData();
        }

        private void SetupData()
        {
            _instruments = new List<Instrument>()
            {
                new Instrument
                {
                    Id = 1,
                    Sedol = "Sedol1",
                    Name = "Name1",
                    Active = true
                }
            };

            _marketDataList = new List<MarketData>()
            {
                new MarketData
                {
                    Id = 1,
                    DataValue = 1111,
                    Sedol = "Sedol1",
                    Active = true
                }
            };

            _marketDataDtoList = new List<MarketDataDto>()
            {
                new MarketDataDto
                {
                    Id = 1,
                    DataValue = 1111,
                    InstrumentId = 1,
                    Active = true
                }
            };

            _marketValuations = new List<MarketValuation>()
            {
                new MarketValuation
                {
                    Total = 1111
                }
            };
        }

        [Fact]
        public async Task ShouldSucceedWhenGettingInstrumentsAsync()
        {
            // Arrange
            _databaseMock
                .Setup(x => x.GetInstrumentsAsync(It.IsAny<bool>()))
                .ReturnsAsync(_instruments);

            // Act
            var result = await _service.GetInstrumentsAsync(true);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeEquivalentTo(_instruments);
        }

        [Fact]
        public async Task ShouldSucceedWhenGettingMarketDataAsync()
        {
            // Arrange
            _databaseMock
                .Setup(x => x.GetMarketDataAsync(It.IsAny<bool>()))
                .ReturnsAsync(_marketDataList);

            _databaseMock
                .Setup(x => x.GetInstrumentsBySedolAsync(It.IsAny<string>()))
                .ReturnsAsync(_instruments);

            // Act
            var result = await _service.GetMarketDataAsync(true);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeEquivalentTo(_marketDataDtoList);
        }

        [Fact]
        public async Task ShouldSucceedWhenGettingMarketValuationsAsync()
        {
            // Arrange
            _databaseMock
                .Setup(x => x.GetMarketDataAsync(It.IsAny<bool>()))
                .ReturnsAsync(_marketDataList);

            // Act
            var result = await _service.GetMarketValuationsAsync(true);

            // Assert
            Assert.NotNull(result);
            result.ShouldBeEquivalentTo(_marketValuations);
        }
    }
}
