using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtterCreekFarms.Api.Services;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/breeding")]
public class BreedingController(
    IBreedingService breedingService,
    IBreedingConfigService configService) : ControllerBase
{
    [HttpPost("calculate")]
    public IActionResult Calculate(BreedingInputModel input) =>
        Ok(breedingService.BuildBreedingForecast(input));

    [HttpGet("config")]  public IActionResult GetConfig()           => Ok(configService.GetLatest());
    [HttpPost("config")] public IActionResult SaveConfig(BreedingInputModel i) { configService.Save(i); return Ok(); }
}
