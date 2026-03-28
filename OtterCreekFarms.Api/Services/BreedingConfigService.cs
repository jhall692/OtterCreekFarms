using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Api.Data;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Services;

public interface IBreedingConfigService
{
    BreedingConfigEntity? GetLatest();
    void Save(BreedingInputModel input, string configName = "My Herd");
}

public class BreedingConfigService(IDbContextFactory<AppDbContext> factory) : IBreedingConfigService
{
    public BreedingConfigEntity? GetLatest()
    {
        using var db = factory.CreateDbContext();
        return db.BreedingConfigs.OrderByDescending(c => c.SavedAt).FirstOrDefault();
    }

    public void Save(BreedingInputModel input, string configName = "My Herd")
    {
        using var db = factory.CreateDbContext();
        db.BreedingConfigs.Add(BreedingConfigEntity.FromInputModel(input, configName));
        db.SaveChanges();
    }
}
