using Microsoft.AspNetCore.Identity;

namespace OtterCreekFarms.Api.Models;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = string.Empty;
}
