using Blazored.LocalStorage;
using OtterCreekFarms.Shared.Models;
using System.Net.Http.Json;
using System.IdentityModel.Tokens.Jwt;

namespace OtterCreekFarms.Client.Services;

public class AuthService(HttpClient http, ILocalStorageService storage)
{
    private const string TokenKey = "ocf_token";

    public event Action? OnAuthChanged;

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await storage.GetItemAsStringAsync(TokenKey);
        if (string.IsNullOrWhiteSpace(token)) return false;

        // Check expiry
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(token)) return false;
        var jwt = handler.ReadJwtToken(token);
        return jwt.ValidTo > DateTime.UtcNow;
    }

    public async Task<string?> GetTokenAsync() =>
        await storage.GetItemAsStringAsync(TokenKey);

    public async Task<string?> GetDisplayNameAsync()
    {
        var token = await storage.GetItemAsStringAsync(TokenKey);
        if (string.IsNullOrWhiteSpace(token)) return null;
        var handler = new JwtSecurityTokenHandler();
        var jwt     = handler.ReadJwtToken(token);
        return jwt.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value
            ?? jwt.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
    }

    public async Task<(bool Success, string? Error)> LoginAsync(string email, string password)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/auth/login", new { email, password });
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            var auth = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            await storage.SetItemAsStringAsync(TokenKey, auth!.Token);
            OnAuthChanged?.Invoke();
            return (true, null);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    public async Task<(bool Success, string? Error)> RegisterAsync(string email, string password, string displayName)
    {
        try
        {
            var response = await http.PostAsJsonAsync("api/auth/register", new { email, password, displayName });
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, error);
            }
            var auth = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            await storage.SetItemAsStringAsync(TokenKey, auth!.Token);
            OnAuthChanged?.Invoke();
            return (true, null);
        }
        catch (Exception ex) { return (false, ex.Message); }
    }

    public async Task LogoutAsync()
    {
        await storage.RemoveItemAsync(TokenKey);
        OnAuthChanged?.Invoke();
    }

    private record AuthResponseDto(string Token, string DisplayName, string Email);
}
