using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Shared.Models;
using OtterCreekFarms.Api.Data;

namespace OtterCreekFarms.Api.Services;

public class HerdService(IDbContextFactory<AppDbContext> factory) : IHerdService
{
    // ── Queries ────────────────────────────────────────────────

    public List<BatchAnimalEntity> GetAll()
    {
        using var db = factory.CreateDbContext();
        return db.BatchAnimals
                 .OrderBy(a => a.FarmArrivalDate)
                 .ThenBy(a => a.EarTag)
                 .ToList();
    }

    public List<BatchAnimalEntity> GetActive()
    {
        using var db = factory.CreateDbContext();
        return db.BatchAnimals
                 .Where(a => a.Status == AnimalStatus.Active)
                 .OrderBy(a => a.FarmArrivalDate)
                 .ThenBy(a => a.EarTag)
                 .ToList();
    }

    public BatchAnimalEntity? GetById(int id)
    {
        using var db = factory.CreateDbContext();
        return db.BatchAnimals.Find(id);
    }

    public BatchAnimalEntity? GetByEarTag(string earTag)
    {
        using var db = factory.CreateDbContext();
        return db.BatchAnimals.FirstOrDefault(a => a.EarTag == earTag);
    }

    public HerdSummaryModel GetSummary(decimal feedLbPerPigPerDay, decimal feedCostPerTon)
    {
        using var db = factory.CreateDbContext();

        var active    = db.BatchAnimals.Where(a => a.Status == AnimalStatus.Active).ToList();
        var processed = db.BatchAnimals
                          .Where(a => a.Status == AnimalStatus.Processed
                                   && a.ProcessedDate.HasValue
                                   && a.ProcessedDate.Value.Year == DateTime.UtcNow.Year)
                          .OrderByDescending(a => a.ProcessedDate)
                          .ToList();

        var totalLiveWeight = active.Sum(a => a.EstimatedCurrentWeightLbs);
        var feedCostPerLb   = feedCostPerTon / 2000m;
        var dailyFeedLbs    = feedLbPerPigPerDay * active.Count;
        var dailyFeedCost   = dailyFeedLbs * feedCostPerLb;

        return new HerdSummaryModel
        {
            ActiveCount                 = active.Count,
            MarketCount                 = active.Count(a => a.Role == AnimalRole.Market),
            BreedingCount               = active.Count(a => a.Role == AnimalRole.Breeding),
            EstimatedTotalLiveWeightLbs = Math.Round(totalLiveWeight, 0),
            EstimatedDailyFeedLbsTotal  = Math.Round(dailyFeedLbs, 1),
            EstimatedDailyFeedCostTotal = Math.Round(dailyFeedCost, 2),
            ProcessedThisYear           = processed.Count,
            ActiveAnimals               = active,
            ProcessedAnimals            = processed
        };
    }

    // ── Mutations ──────────────────────────────────────────────

    public BatchAnimalEntity Add(BatchAnimalEntity animal)
    {
        using var db = factory.CreateDbContext();
        animal.CreatedAt = DateTime.UtcNow;
        animal.UpdatedAt = DateTime.UtcNow;
        db.BatchAnimals.Add(animal);
        db.SaveChanges();
        return animal;
    }

    public void Update(BatchAnimalEntity animal)
    {
        using var db = factory.CreateDbContext();
        animal.UpdatedAt = DateTime.UtcNow;
        db.BatchAnimals.Update(animal);
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        using var db = factory.CreateDbContext();
        var animal = db.BatchAnimals.Find(id)
            ?? throw new InvalidOperationException($"Animal {id} not found.");
        db.BatchAnimals.Remove(animal);
        db.SaveChanges();
    }

    public void RecordProcessing(
        int      id,
        DateTime processedDate,
        decimal? actualLiveWeightLbs,
        decimal  actualHangingWeightLbs,
        decimal  actualRevenueTotal,
        decimal  actualProcessingCostTotal,
        string?  butcherProfileKey,
        string?  notes)
    {
        using var db = factory.CreateDbContext();
        var animal = db.BatchAnimals.Find(id)
            ?? throw new InvalidOperationException($"Animal {id} not found.");

        animal.Status                    = AnimalStatus.Processed;
        animal.ProcessedDate             = processedDate;
        animal.ActualLiveWeightLbs       = actualLiveWeightLbs;
        animal.ActualHangingWeightLbs    = actualHangingWeightLbs;
        animal.ActualRevenueTotal        = actualRevenueTotal;
        animal.ActualProcessingCostTotal = actualProcessingCostTotal;
        animal.ButcherProfileKey         = butcherProfileKey;
        animal.ProcessingNotes           = notes;
        animal.UpdatedAt                 = DateTime.UtcNow;

        db.SaveChanges();
    }

    public void RecordRemoval(int id, AnimalStatus status, DateTime removedDate, string? reason)
    {
        using var db = factory.CreateDbContext();
        var animal = db.BatchAnimals.Find(id)
            ?? throw new InvalidOperationException($"Animal {id} not found.");

        animal.Status        = status;
        animal.RemovedDate   = removedDate;
        animal.RemovalReason = reason;
        animal.UpdatedAt     = DateTime.UtcNow;

        db.SaveChanges();
    }

    public void RecordWeight(int id, decimal weightLbs, DateTime date)
    {
        using var db = factory.CreateDbContext();
        var animal = db.BatchAnimals.Find(id)
            ?? throw new InvalidOperationException($"Animal {id} not found.");

        // Recalibrate ADG from the last anchor
        if (animal.LastRecordedWeightLbs.HasValue && animal.LastWeightDate.HasValue)
        {
            var days = (date.Date - animal.LastWeightDate.Value.Date).Days;
            if (days > 0)
            {
                var newAdg = (weightLbs - animal.LastRecordedWeightLbs.Value) / days;
                if (newAdg > 0m)
                    animal.AverageDailyGainLbs = Math.Round(newAdg, 3);
            }
        }

        animal.LastRecordedWeightLbs = weightLbs;
        animal.LastWeightDate        = date;
        animal.UpdatedAt             = DateTime.UtcNow;

        db.SaveChanges();
    }

    public void RecordTapeMeasurement(
        int      id,
        decimal  heartGirthInches,
        decimal  bodyLengthInches,
        DateTime measureDate,
        decimal  divisor)
    {
        using var db = factory.CreateDbContext();
        var animal = db.BatchAnimals.Find(id)
            ?? throw new InvalidOperationException($"Animal {id} not found.");

        animal.TapeHeartGirthInches = heartGirthInches;
        animal.TapeBodyLengthInches = bodyLengthInches;
        animal.TapeMeasureDate      = measureDate;
        animal.TapeDivisor          = divisor > 0m ? divisor : 370m;
        animal.WeightMethod         = WeightEstimateMethod.TapeMeasure;
        animal.UpdatedAt            = DateTime.UtcNow;

        db.SaveChanges();
    }

    public decimal? CalibrateHerdDivisor(int id, decimal actualLiveWeightLbs)
    {
        using var db = factory.CreateDbContext();
        var animal = db.BatchAnimals.Find(id);
        return animal?.CalibrateDivisor(actualLiveWeightLbs);
    }

    // ── Helpers ────────────────────────────────────────────────

    public string NextEarTagSuggestion()
    {
        using var db  = factory.CreateDbContext();
        var year      = DateTime.Today.Year % 100;
        var prefix    = $"OCF-{year:D2}-";

        var maxSeq = db.BatchAnimals
                       .Where(a => a.EarTag.StartsWith(prefix))
                       .Select(a => a.EarTag)
                       .ToList()
                       .Select(tag =>
                       {
                           var suffix = tag[prefix.Length..];
                           return int.TryParse(suffix, out var n) ? n : 0;
                       })
                       .DefaultIfEmpty(0)
                       .Max();

        return $"{prefix}{(maxSeq + 1):D3}";
    }
}
