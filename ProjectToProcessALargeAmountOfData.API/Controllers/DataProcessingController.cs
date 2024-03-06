using Microsoft.AspNetCore.Mvc;
using ProjectToProcessALargeAmountOfData.Domain.DTOs;
using ProjectToProcessALargeAmountOfData.Domain.Interfaces.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectToProcessALargeAmountOfData.API.Controllers
{
    [Route("api/data-processing/")]
    [ApiController]
    public partial class DataProcessingController : ControllerBase
    {
        private readonly IDataProcessingService _dataProcessingService;

        public DataProcessingController(IDataProcessingService dataProcessingService)
        {
            _dataProcessingService = dataProcessingService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessDataAsync(IList<int> numbers)
        {
            try
            {
                IList<MultiplicationResultResponse> results = await _dataProcessingService
                    .ProcessDataAsync(numbers);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during processing: {ex.Message}");
            }
        }
    }
}
