using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtterCreekFarms.Api.Services;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/config")]
public class ConfigController(
    IFeedConfigService feedConfig,
    IRevenueConfigService revenueConfig,
    IPigCostConfigService pigCostConfig) : ControllerBase
{
    [HttpGet("feed")]     public IActionResult GetFeed()                    => Ok(feedConfig.GetLatest());
    [HttpPost("feed")]    public IActionResult SaveFeed(FeedInputModel c)   { feedConfig.Save(c);       return Ok(); }

    [HttpGet("revenue")]  public IActionResult GetRevenue()                 => Ok(revenueConfig.GetLatest());
    [HttpPost("revenue")] public IActionResult SaveRevenue(RevenueInputModel c) { revenueConfig.Save(c); return Ok(); }

    [HttpGet("pigcost")]  public IActionResult GetPigCost()                 => Ok(pigCostConfig.GetLatest());
    [HttpPost("pigcost")] public IActionResult SavePigCost(PigCostInputModel c) { pigCostConfig.Save(c); return Ok(); }
}
