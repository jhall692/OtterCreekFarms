using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Shared.Models;
using OtterCreekFarms.Api.Data;

namespace OtterCreekFarms.Api.Services;

public class PigCostConfigService(IDbContextFactory<AppDbContext> factory) : IPigCostConfigService
{
    public PigCostConfigEntity? GetLatest()
    {
        using var db = factory.CreateDbContext();
        return db.PigCostConfigs.OrderByDescending(c => c.SavedAt).FirstOrDefault();
    }

    public PigCostInputModel GetLatestOrDefault()
    {
        var entity = GetLatest();
        return entity is not null ? entity.ToInputModel() : new PigCostInputModel();
    }

    public void Save(PigCostInputModel input)
    {
        using var db = factory.CreateDbContext();
        db.PigCostConfigs.Add(PigCostConfigEntity.FromInputModel(input));
        db.SaveChanges();
    }
}
