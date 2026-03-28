using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Shared.Models;
using OtterCreekFarms.Api.Data;

namespace OtterCreekFarms.Api.Services;

public class RevenueConfigService(IDbContextFactory<AppDbContext> factory) : IRevenueConfigService
{
    public RevenueConfigEntity? GetLatest()
    {
        using var db = factory.CreateDbContext();
        return db.RevenueConfigs.OrderByDescending(c => c.SavedAt).FirstOrDefault();
    }

    public RevenueInputModel GetLatestOrDefault()
    {
        var entity = GetLatest();
        return entity is not null ? entity.ToInputModel() : new RevenueInputModel();
    }

    public void Save(RevenueInputModel input)
    {
        using var db = factory.CreateDbContext();
        db.RevenueConfigs.Add(RevenueConfigEntity.FromInputModel(input));
        db.SaveChanges();
    }
}
