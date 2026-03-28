using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtterCreekFarms.Api.Services;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/animals")]
public class AnimalsController(IHerdService herdService) : ControllerBase
{
    [HttpGet] public IActionResult GetAll() => Ok(herdService.GetAll());
    [HttpPost] public IActionResult Add(BatchAnimalEntity a) => Ok(herdService.Add(a));
    [HttpPut("{id}")] public IActionResult Update(int id, BatchAnimalEntity a) { a.Id = id; herdService.Update(a); return Ok(); }
    [HttpDelete("{id}")] public IActionResult Delete(int id) { herdService.Delete(id); return Ok(); }

    [HttpPost("{id}/process")]
    public IActionResult RecordProcessing(int id, ProcessingActualsModel actuals)
    {
        herdService.RecordProcessing(
            id,
            actuals.ProcessingDate,
            actuals.ActualLiveWeight,
            actuals.ActualHangingWeight,
            actuals.ActualRevenueTotal,
            actuals.ActualProcessingCost,
            butcherProfileKey: null,
            actuals.Notes);
        return Ok();
    }
}
