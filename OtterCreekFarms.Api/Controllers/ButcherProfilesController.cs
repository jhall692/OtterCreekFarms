using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtterCreekFarms.Api.Services;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/butcher-profiles")]
public class ButcherProfilesController(IButcherProfileService service) : ControllerBase
{
    [HttpGet]    public IActionResult GetAll()                    => Ok(service.GetProfiles());
    [HttpPost]   public IActionResult Save(ButcherProfileModel p) { service.Save(p); return Ok(); }
    [HttpDelete("{key}")] public IActionResult Delete(string key) { service.Delete(key); return Ok(); }
}
