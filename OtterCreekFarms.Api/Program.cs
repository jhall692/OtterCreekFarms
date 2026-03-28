using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OtterCreekFarms.Api.Data;
using OtterCreekFarms.Api.Models;
using OtterCreekFarms.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// ── Database ───────────────────────────────────────────────────
var dbFileName = builder.Configuration["Database:FileName"] ?? "ottercreek.db";
var dbPath     = Path.Combine(builder.Environment.ContentRootPath, "App_Data", dbFileName);
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

builder.Services.AddDbContextFactory<AppDbContext>(o => o.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddScoped<AppDbContext>(p =>
    p.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());

// ── Identity ───────────────────────────────────────────────────
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
{
    o.Password.RequireDigit           = true;
    o.Password.RequiredLength         = 8;
    o.Password.RequireNonAlphanumeric = false;
    o.Password.RequireUppercase       = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// ── JWT Auth ───────────────────────────────────────────────────
var jwtSecret = builder.Configuration["Jwt:Secret"]!;
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer              = builder.Configuration["Jwt:Issuer"],
        ValidAudience            = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

// ── CORS — allow WASM client ───────────────────────────────────
var clientUrl = builder.Configuration["Client:Url"] ?? "https://ottercreekfarms.com";
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.WithOrigins(clientUrl, "https://localhost:7000", "http://localhost:5000")
     .AllowAnyMethod()
     .AllowAnyHeader()));

// ── Services ───────────────────────────────────────────────────
builder.Services.AddSingleton<JwtService>();
builder.Services.AddScoped<IExpenseService,       ExpenseService>();
builder.Services.AddScoped<IHerdService,           HerdService>();
builder.Services.AddScoped<IButcherProfileService, ButcherProfileService>();
builder.Services.AddScoped<IForecastBatchService,  ForecastBatchService>();
builder.Services.AddScoped<IBreedingConfigService, BreedingConfigService>();
builder.Services.AddScoped<IFeedConfigService,     FeedConfigService>();
builder.Services.AddScoped<IRevenueConfigService,  RevenueConfigService>();
builder.Services.AddScoped<IPigCostConfigService,  PigCostConfigService>();
builder.Services.AddSingleton<IForecastService,    ForecastService>();
builder.Services.AddSingleton<IBreedingService,    BreedingService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ── Migrate on startup ─────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
