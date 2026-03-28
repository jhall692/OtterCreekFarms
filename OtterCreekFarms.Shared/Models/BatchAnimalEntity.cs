using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtterCreekFarms.Shared.Models;

public class BatchAnimalEntity
{
    [Key] public int Id { get; set; }

    public int? BatchId { get; set; }
    [ForeignKey(nameof(BatchId))]
    public ForecastBatchEntity? Batch { get; set; }

    // ── Identity ───────────────────────────────────────────────
    [MaxLength(20)]  public string  EarTag  { get; set; } = string.Empty;
    [MaxLength(50)]  public string? Name    { get; set; }
    public AnimalSex  Sex  { get; set; } = AnimalSex.Barrow;
    public AnimalSource Source  { get; set; } = AnimalSource.Purchased;
    public AnimalRole   Role    { get; set; } = AnimalRole.Market;
    public AnimalStatus Status  { get; set; } = AnimalStatus.Active;

    // ── Acquisition ────────────────────────────────────────────
    public DateTime  FarmArrivalDate    { get; set; } = DateTime.Today;
    public decimal   AcquiredWeightLbs  { get; set; }
    public decimal   AcquiredCost       { get; set; }
    public DateTime? BirthDate          { get; set; }
    [MaxLength(200)] public string? BreederName { get; set; }

    // ── Per-animal costs ───────────────────────────────────────
    public decimal VetCost      { get; set; }
    public decimal VaccineCost  { get; set; }
    public decimal DewormerCost { get; set; }
    public decimal BeddingCost  { get; set; }
    public decimal MiscCost     { get; set; }

    // ── Revenue mode ───────────────────────────────────────────
    public RevenueMode DefaultRevenueMode { get; set; } = RevenueMode.RetailCuts;

    // ── Weight estimation ──────────────────────────────────────
    public WeightEstimateMethod WeightMethod          { get; set; } = WeightEstimateMethod.AdgProjection;
    public decimal              AverageDailyGainLbs   { get; set; } = 0.38m;
    public decimal?             LastRecordedWeightLbs { get; set; }
    public DateTime?            LastWeightDate        { get; set; }

    // Tape measurement
    public decimal?  TapeHeartGirthInches { get; set; }
    public decimal?  TapeBodyLengthInches { get; set; }
    public DateTime? TapeMeasureDate      { get; set; }
    public decimal   TapeDivisor          { get; set; } = 370m;

    // ── Processing actuals ─────────────────────────────────────
    public DateTime? ProcessedDate              { get; set; }
    public decimal?  ActualLiveWeightLbs        { get; set; }
    public decimal?  ActualHangingWeightLbs     { get; set; }
    public decimal?  ActualRevenueTotal         { get; set; }
    public decimal?  ActualProcessingCostTotal  { get; set; }
    [MaxLength(50)]  public string? ButcherProfileKey { get; set; }
    [MaxLength(500)] public string? ProcessingNotes   { get; set; }

    // ── Removal ────────────────────────────────────────────────
    public DateTime? RemovedDate              { get; set; }
    [MaxLength(200)] public string? RemovalReason { get; set; }

    [MaxLength(1000)] public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // ── Computed (not persisted) ───────────────────────────────

    [NotMapped]
    public int DaysOnFarm => Status == AnimalStatus.Active
        ? (DateTime.Today - FarmArrivalDate.Date).Days
        : ProcessedDate.HasValue
            ? (ProcessedDate.Value.Date - FarmArrivalDate.Date).Days
            : RemovedDate.HasValue
                ? (RemovedDate.Value.Date - FarmArrivalDate.Date).Days
                : 0;

    [NotMapped]
    public decimal AdgEstimatedWeightLbs
    {
        get
        {
            decimal baseWeight;
            int     baseDays;
            if (LastRecordedWeightLbs.HasValue && LastWeightDate.HasValue)
            {
                baseWeight = LastRecordedWeightLbs.Value;
                baseDays   = (DateTime.Today - LastWeightDate.Value.Date).Days;
            }
            else
            {
                baseWeight = AcquiredWeightLbs;
                baseDays   = DaysOnFarm;
            }
            return Math.Max(0m, Math.Round(baseWeight + (AverageDailyGainLbs * baseDays), 1));
        }
    }

    [NotMapped]
    public decimal? TapeEstimatedWeightLbs
    {
        get
        {
            if (TapeHeartGirthInches is null or <= 0 || TapeBodyLengthInches is null or <= 0)
                return null;
            var div = TapeDivisor > 0m ? TapeDivisor : 370m;
            return Math.Round(
                (TapeHeartGirthInches.Value * TapeHeartGirthInches.Value
                 * TapeBodyLengthInches.Value) / div, 1);
        }
    }

    public decimal? CalibrateDivisor(decimal knownLiveWeightLbs)
    {
        if (TapeHeartGirthInches is null or <= 0 ||
            TapeBodyLengthInches is null or <= 0 ||
            knownLiveWeightLbs   <= 0)
            return null;
        return Math.Round(
            (TapeHeartGirthInches.Value * TapeHeartGirthInches.Value
             * TapeBodyLengthInches.Value) / knownLiveWeightLbs, 1);
    }

    [NotMapped] public bool TapeIsComplete =>
        TapeHeartGirthInches is > 0 && TapeBodyLengthInches is > 0;

    [NotMapped]
    public decimal EstimatedCurrentWeightLbs =>
        WeightMethod == WeightEstimateMethod.TapeMeasure
            ? (TapeEstimatedWeightLbs ?? AdgEstimatedWeightLbs)
            : AdgEstimatedWeightLbs;

    [NotMapped]
    public string WeightMethodBadge =>
        WeightMethod == WeightEstimateMethod.TapeMeasure
            ? (TapeIsComplete ? "📏 Tape" : "📏 Tape†")
            : "📈 ADG";

    [NotMapped]
    public decimal TotalDirectCostPerAnimal =>
        AcquiredCost + VetCost + VaccineCost + DewormerCost + BeddingCost + MiscCost;

    [NotMapped]
    public string DisplayName => !string.IsNullOrWhiteSpace(Name)
        ? $"{EarTag} — {Name}" : EarTag;

    [NotMapped]
    public int? AgeMonths
    {
        get
        {
            if (!BirthDate.HasValue) return null;
            var today = DateTime.Today;
            var bd    = BirthDate.Value.Date;
            return ((today.Year - bd.Year) * 12) + (today.Month - bd.Month);
        }
    }
}
