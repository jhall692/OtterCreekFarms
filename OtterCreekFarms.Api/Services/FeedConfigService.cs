using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Shared.Models;
using OtterCreekFarms.Api.Data;

namespace OtterCreekFarms.Api.Services;

public class FeedConfigService(IDbContextFactory<AppDbContext> factory) : IFeedConfigService
{
    public FeedConfigEntity? GetLatest()
    {
        using var db = factory.CreateDbContext();
        return db.FeedConfigs.OrderByDescending(c => c.SavedAt).FirstOrDefault();
    }

    public FeedInputModel GetLatestOrDefault()
    {
        var entity = GetLatest();
        return entity is not null ? entity.ToInputModel() : new FeedInputModel();
    }

    public void Save(FeedInputModel input)
    {
        using var db = factory.CreateDbContext();
        db.FeedConfigs.Add(FeedConfigEntity.FromInputModel(input));
        db.SaveChanges();
    }
}
