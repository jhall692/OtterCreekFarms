using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Api.Data;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Services;

public interface IButcherProfileService
{
    List<ButcherProfileModel> GetProfiles();
    ButcherProfileModel? GetByKey(string key);
    void Save(ButcherProfileModel profile);
    void Delete(string key);
}

public class ButcherProfileService(IDbContextFactory<AppDbContext> factory) : IButcherProfileService
{
    public List<ButcherProfileModel> GetProfiles()
    {
        using var db = factory.CreateDbContext();
        return db.ButcherProfiles.OrderBy(p => p.Name).ToList();
    }

    public ButcherProfileModel? GetByKey(string key)
    {
        using var db = factory.CreateDbContext();
        return db.ButcherProfiles.Find(key);
    }

    public void Save(ButcherProfileModel profile)
    {
        using var db = factory.CreateDbContext();
        if (db.ButcherProfiles.Find(profile.Key) is not null)
            db.ButcherProfiles.Update(profile);
        else
            db.ButcherProfiles.Add(profile);
        db.SaveChanges();
    }

    public void Delete(string key)
    {
        using var db = factory.CreateDbContext();
        var p = db.ButcherProfiles.Find(key);
        if (p is not null) { db.ButcherProfiles.Remove(p); db.SaveChanges(); }
    }
}
