using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Api.Data;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Services;

public interface IForecastBatchService
{
    List<ForecastBatchEntity> GetAll();
    ForecastBatchEntity Save(ForecastInputModel input);
    void Delete(int id);
}

public class ForecastBatchService(IDbContextFactory<AppDbContext> factory) : IForecastBatchService
{
    public List<ForecastBatchEntity> GetAll()
    {
        using var db = factory.CreateDbContext();
        return db.ForecastBatches.OrderByDescending(b => b.SavedAt).ToList();
    }

    public ForecastBatchEntity Save(ForecastInputModel input)
    {
        using var db = factory.CreateDbContext();
        var entity = ForecastBatchEntity.FromInputModel(input);
        db.ForecastBatches.Add(entity);
        db.SaveChanges();
        return entity;
    }

    public void Delete(int id)
    {
        using var db = factory.CreateDbContext();
        var e = db.ForecastBatches.Find(id);
        if (e is not null) { db.ForecastBatches.Remove(e); db.SaveChanges(); }
    }
}
