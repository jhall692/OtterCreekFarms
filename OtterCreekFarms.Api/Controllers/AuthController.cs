using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtterCreekFarms.Api.Models;
using OtterCreekFarms.Api.Services;

namespace OtterCreekFarms.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    JwtService jwtService,
    IConfiguration config) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest req)
    {
        var user = await userManager.FindByEmailAsync(req.Email);
        if (user is null) return Unauthorized("Invalid credentials.");

        var result = await signInManager.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: true);
        if (!result.Succeeded)
            return result.IsLockedOut ? Unauthorized("Account locked.") : Unauthorized("Invalid credentials.");

        var token = jwtService.GenerateToken(user);
        return Ok(new AuthResponse(token, user.DisplayName, user.Email!));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest req)
    {
        if (!config.GetValue<bool>("Auth:RegistrationEnabled"))
            return BadRequest("Registration is disabled.");

        var user = new ApplicationUser
        {
            UserName       = req.Email,
            Email          = req.Email,
            DisplayName    = req.DisplayName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded)
            return BadRequest(string.Join(" ", result.Errors.Select(e => e.Description)));

        var token = jwtService.GenerateToken(user);
        return Ok(new AuthResponse(token, user.DisplayName, user.Email!));
    }
}
