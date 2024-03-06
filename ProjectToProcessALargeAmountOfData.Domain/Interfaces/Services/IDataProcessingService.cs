using ProjectToProcessALargeAmountOfData.Domain.DTOs;

namespace ProjectToProcessALargeAmountOfData.Domain.Interfaces.Services
{
    public interface IDataProcessingService
    {
        Task<IList<MultiplicationResultResponse>> ProcessDataAsync(IList<int> numbers);
    }
}
