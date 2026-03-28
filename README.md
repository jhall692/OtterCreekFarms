# Otter Creek Farms — A Hall Family Farm

Blazor WASM + ASP.NET Core Web API farm management application.

## Projects

| Project | Description |
|---|---|
| `OtterCreekFarms.Api` | ASP.NET Core Web API — JWT auth, EF Core, SQLite |
| `OtterCreekFarms.Client` | Blazor WASM — mobile-first UI |
| `OtterCreekFarms.Shared` | Shared models used by both projects |

## Local Development

```bash
# Run the API (port 5000)
cd OtterCreekFarms.Api
dotnet run

# Run the client (port 7000)
cd OtterCreekFarms.Client
dotnet run
```

Set API URL in `OtterCreekFarms.Client/wwwroot/appsettings.json`:
```json
{ "ApiBaseUrl": "http://localhost:5000" }
```

## Secrets (API)

```powershell
cd OtterCreekFarms.Api
dotnet user-secrets set "Jwt:Secret" "your-32-char-minimum-secret-key-here"
dotnet user-secrets set "Auth:RegistrationEnabled" "true"
```

## Deployment

- **API** → GoDaddy/Plesk (needs .NET 9 hosting bundle)
- **Client** → GoDaddy static files (just copy `wwwroot` publish output)
