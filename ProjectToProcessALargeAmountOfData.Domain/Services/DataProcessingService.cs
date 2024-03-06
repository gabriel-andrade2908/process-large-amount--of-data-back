using Microsoft.Extensions.Logging;
using ProjectToProcessALargeAmountOfData.Domain.DTOs;
using ProjectToProcessALargeAmountOfData.Domain.Interfaces.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectToProcessALargeAmountOfData.Domain.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly ILogger<DataProcessingService> _logger;
        private readonly IList<MultiplicationResultResponse> _results;

        public DataProcessingService(SemaphoreSlim semaphore, ILogger<DataProcessingService> logger)
        {
            _results = [];
            _semaphore = semaphore;
            _logger = logger;
        }

        public async Task<IList<MultiplicationResultResponse>> ProcessDataAsync(IList<int> numbers)
        {
            var tasks = new List<Task>();

            foreach (int number in numbers)
            {
                tasks.Add(ProcessNumberAsync(number));
            }

            await Task.WhenAll(tasks);

            return _results;
        }

        private async Task ProcessNumberAsync(int number)
        {
            try
            {
                await _semaphore.WaitAsync();

                var firstMultiplicationNumber = 1;
                var lastMultiplicationNumber = 10;
                var fileName = $"table_of_{number}.txt";

                using StreamWriter writer = new(fileName, append: false);
                for (int i = firstMultiplicationNumber; i <= lastMultiplicationNumber; i++)
                {
                    int multiplicationResult = number * i;
                    await writer.WriteLineAsync($"{number} x {i} = {multiplicationResult}");

                    AddResultToResultsList(number, i, multiplicationResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing number {number}: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private void AddResultToResultsList(int number, int multiplier, int result)
        {
            _results.Add(new MultiplicationResultResponse
            {
                Number = number,
                Multiplier = multiplier,
                Result = result
            });
        }
    }
}
