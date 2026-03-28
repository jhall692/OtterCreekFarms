using System.Net.Http.Headers;
using System.Net.Http.Json;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Client.Services;

/// <summary>
/// Wraps all API calls, automatically attaching the JWT token.
/// </summary>
public class ApiService(HttpClient http, AuthService auth)
{
    private async Task AuthorizeAsync()
    {
        var token = await auth.GetTokenAsync();
        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    // ── Expenses ───────────────────────────────────────────────
    public async Task<List<ExpenseItemModel>> GetExpensesAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<List<ExpenseItemModel>>("api/expenses") ?? [];
    }

    public async Task<ExpenseItemModel> AddExpenseAsync(ExpenseItemModel e)
    {
        await AuthorizeAsync();
        var r = await http.PostAsJsonAsync("api/expenses", e);
        return await r.Content.ReadFromJsonAsync<ExpenseItemModel>() ?? e;
    }

    public async Task UpdateExpenseAsync(ExpenseItemModel e)
    {
        await AuthorizeAsync();
        await http.PutAsJsonAsync($"api/expenses/{e.Id}", e);
    }

    public async Task DeleteExpenseAsync(int id)
    {
        await AuthorizeAsync();
        await http.DeleteAsync($"api/expenses/{id}");
    }

    // ── Animals ────────────────────────────────────────────────
    public async Task<List<BatchAnimalEntity>> GetAnimalsAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<List<BatchAnimalEntity>>("api/animals") ?? [];
    }

    public async Task<BatchAnimalEntity> AddAnimalAsync(BatchAnimalEntity a)
    {
        await AuthorizeAsync();
        var r = await http.PostAsJsonAsync("api/animals", a);
        return await r.Content.ReadFromJsonAsync<BatchAnimalEntity>() ?? a;
    }

    public async Task UpdateAnimalAsync(BatchAnimalEntity a)
    {
        await AuthorizeAsync();
        await http.PutAsJsonAsync($"api/animals/{a.Id}", a);
    }

    public async Task DeleteAnimalAsync(int id)
    {
        await AuthorizeAsync();
        await http.DeleteAsync($"api/animals/{id}");
    }

    public async Task RecordProcessingAsync(int id, ProcessingActualsModel actuals)
    {
        await AuthorizeAsync();
        await http.PostAsJsonAsync($"api/animals/{id}/process", actuals);
    }

    // ── Butcher Profiles ───────────────────────────────────────
    public async Task<List<ButcherProfileModel>> GetButcherProfilesAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<List<ButcherProfileModel>>("api/butcher-profiles") ?? [];
    }

    public async Task SaveButcherProfileAsync(ButcherProfileModel p)
    {
        await AuthorizeAsync();
        await http.PostAsJsonAsync("api/butcher-profiles", p);
    }

    public async Task DeleteButcherProfileAsync(string key)
    {
        await AuthorizeAsync();
        await http.DeleteAsync($"api/butcher-profiles/{key}");
    }

    // ── Forecast ───────────────────────────────────────────────
    public async Task<ForecastResultModel?> CalculateForecastAsync(ForecastInputModel input)
    {
        await AuthorizeAsync();
        var r = await http.PostAsJsonAsync("api/forecast/calculate", input);
        return await r.Content.ReadFromJsonAsync<ForecastResultModel>();
    }

    public async Task<List<ForecastBatchEntity>> GetForecastBatchesAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<List<ForecastBatchEntity>>("api/forecast/batches") ?? [];
    }

    public async Task<ForecastBatchEntity?> SaveForecastBatchAsync(ForecastInputModel input)
    {
        await AuthorizeAsync();
        var r = await http.PostAsJsonAsync("api/forecast/batches", input);
        return await r.Content.ReadFromJsonAsync<ForecastBatchEntity>();
    }

    public async Task DeleteForecastBatchAsync(int id)
    {
        await AuthorizeAsync();
        await http.DeleteAsync($"api/forecast/batches/{id}");
    }

    // ── Breeding ───────────────────────────────────────────────
    public async Task<BreedingResultModel?> CalculateBreedingAsync(BreedingInputModel input)
    {
        await AuthorizeAsync();
        var r = await http.PostAsJsonAsync("api/breeding/calculate", input);
        return await r.Content.ReadFromJsonAsync<BreedingResultModel>();
    }

    public async Task<BreedingConfigEntity?> GetBreedingConfigAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<BreedingConfigEntity>("api/breeding/config");
    }

    public async Task SaveBreedingConfigAsync(BreedingInputModel input)
    {
        await AuthorizeAsync();
        await http.PostAsJsonAsync("api/breeding/config", input);
    }

    // ── Config ─────────────────────────────────────────────────
    public async Task<FeedConfigEntity?> GetFeedConfigAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<FeedConfigEntity>("api/config/feed");
    }

    public async Task SaveFeedConfigAsync(FeedInputModel c)
    {
        await AuthorizeAsync();
        await http.PostAsJsonAsync("api/config/feed", c);
    }

    public async Task<RevenueConfigEntity?> GetRevenueConfigAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<RevenueConfigEntity>("api/config/revenue");
    }

    public async Task SaveRevenueConfigAsync(RevenueInputModel c)
    {
        await AuthorizeAsync();
        await http.PostAsJsonAsync("api/config/revenue", c);
    }

    public async Task<PigCostConfigEntity?> GetPigCostConfigAsync()
    {
        await AuthorizeAsync();
        return await http.GetFromJsonAsync<PigCostConfigEntity>("api/config/pigcost");
    }

    public async Task SavePigCostConfigAsync(PigCostInputModel c)
    {
        await AuthorizeAsync();
        await http.PostAsJsonAsync("api/config/pigcost", c);
    }
}
