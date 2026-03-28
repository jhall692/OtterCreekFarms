namespace OtterCreekFarms.Api.Models;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string DisplayName);
public record AuthResponse(string Token, string DisplayName, string Email);
