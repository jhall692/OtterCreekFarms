using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Services;

public interface IHerdService
{
    // ── Queries ────────────────────────────────────────────────
    List<BatchAnimalEntity> GetAll();
    List<BatchAnimalEntity> GetActive();
    BatchAnimalEntity?      GetById(int id);
    BatchAnimalEntity?      GetByEarTag(string earTag);
    HerdSummaryModel        GetSummary(decimal feedLbPerPigPerDay, decimal feedCostPerTon);

    // ── Mutations ──────────────────────────────────────────────
    BatchAnimalEntity Add(BatchAnimalEntity animal);
    void              Update(BatchAnimalEntity animal);
    void              Delete(int id);

    void RecordProcessing(
        int      id,
        DateTime processedDate,
        decimal? actualLiveWeightLbs,
        decimal  actualHangingWeightLbs,
        decimal  actualRevenueTotal,
        decimal  actualProcessingCostTotal,
        string?  butcherProfileKey,
        string?  notes);

    void RecordRemoval(
        int          id,
        AnimalStatus status,
        DateTime     removedDate,
        string?      reason);

    void RecordWeight(int id, decimal weightLbs, DateTime date);

    void RecordTapeMeasurement(
        int      id,
        decimal  heartGirthInches,
        decimal  bodyLengthInches,
        DateTime measureDate,
        decimal  divisor);

    /// <summary>
    /// After butcher, call this to find the divisor that would have
    /// matched your tape measurements to actual live weight.
    /// Returns the calibrated divisor, or null if data is insufficient.
    /// </summary>
    decimal? CalibrateHerdDivisor(int id, decimal actualLiveWeightLbs);

    // ── Helpers ────────────────────────────────────────────────
    string NextEarTagSuggestion();
}
