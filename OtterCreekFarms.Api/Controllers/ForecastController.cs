using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtterCreekFarms.Api.Services;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/forecast")]
public class ForecastController(
    IForecastService forecastService,
    IForecastBatchService batchService) : ControllerBase
{
    [HttpPost("calculate")]
    public IActionResult Calculate(ForecastInputModel input) =>
        Ok(forecastService.BuildForecast(input));

    [HttpGet("batches")]    public IActionResult GetBatches()              => Ok(batchService.GetAll());
    [HttpPost("batches")]   public IActionResult SaveBatch(ForecastInputModel i) => Ok(batchService.Save(i));
    [HttpDelete("batches/{id}")] public IActionResult DeleteBatch(int id) { batchService.Delete(id); return Ok(); }
}
