using Microsoft.Extensions.Logging;
using Moq;
using ProjectToProcessALargeAmountOfData.Domain.DTOs;
using ProjectToProcessALargeAmountOfData.Domain.Interfaces.Services;
using ProjectToProcessALargeAmountOfData.Domain.Services;
using Xunit;

namespace ProjectToProcessALargeAmountOfData.Tests
{
    public class DataProcessingServiceTests
    {
        private readonly Mock<SemaphoreSlim> _semaphore;
        private readonly Mock<ILogger<DataProcessingService>> _logger;
        private readonly IDataProcessingService _service;

        public DataProcessingServiceTests()
        {
            _semaphore = new Mock<SemaphoreSlim>(1, 1);
            _logger = new Mock<ILogger<DataProcessingService>>();
            _service = new DataProcessingService(_semaphore.Object, _logger.Object);
        }
        [Fact]
        public async Task ProcessDataAsync_Returns_Results()
        {
            var numbers = new List<int> { 2, 3, 5, 7, 11 };

            IList<MultiplicationResultResponse> results = await _service.ProcessDataAsync(numbers);

            var numberOfMultiplications = 10;

            Assert.NotNull(results);
            Assert.Equal(numbers.Count * numberOfMultiplications, results.Count);
        }
    }
}