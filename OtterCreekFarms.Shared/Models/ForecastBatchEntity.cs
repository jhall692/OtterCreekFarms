using System.ComponentModel.DataAnnotations;

namespace OtterCreekFarms.Shared.Models;

public class ForecastBatchEntity
{
    [Key] public int Id { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    [MaxLength(100)] public string BatchName { get; set; } = string.Empty;
    [MaxLength(50)]  public string ButcherProfileKey { get; set; } = string.Empty;
    public int PigCount { get; set; }
    public decimal MortalityPercent { get; set; }
    public string PigSource { get; set; } = nameof(Models.PigSource.Purchased);
    public string RevenueMode { get; set; } = nameof(Models.RevenueMode.RetailCuts);
    public decimal PurchaseCostPerPig { get; set; }
    public decimal BreedingAllocationPerPig { get; set; }
    public decimal VetCostPerPig { get; set; }
    public decimal VaccineCostPerPig { get; set; }
    public decimal DewormerCostPerPig { get; set; }
    public decimal BeddingCostPerPig { get; set; }
    public decimal MiscPigCostPerPig { get; set; }
    public bool UseFeedProjection { get; set; }
    public decimal FeedCostPerPigManual { get; set; }
    public decimal BulkFeedCostPerTon { get; set; }
    public decimal FeedLbPerPigPerDay { get; set; }
    public decimal SupplementFeedAdjustmentPercent { get; set; }
    public int GrowMonths { get; set; }
    public int PastureMonths { get; set; }
    public decimal HangingWeightLow { get; set; }
    public decimal HangingWeightExpected { get; set; }
    public decimal HangingWeightHigh { get; set; }
    public decimal ShrinkPercent { get; set; }
    public decimal YieldAdjustmentPercent { get; set; }
    public decimal WholeHalfPricePerLb { get; set; }
    public decimal RetailRevenuePerPigLow { get; set; }
    public decimal RetailRevenuePerPigExpected { get; set; }
    public decimal RetailRevenuePerPigHigh { get; set; }
    public decimal ProcessingFlatPerPig { get; set; }
    public decimal ProcessingPerLb { get; set; }
    public decimal EstimatedProcessingLbsPerPig { get; set; }
    public decimal SlaughterFeePerPig { get; set; }
    public decimal SmokingCostPerLb { get; set; }
    public decimal EstimatedSmokedLbsPerPig { get; set; }
    public decimal SausageCostPerLb { get; set; }
    public decimal EstimatedSausageLbsPerPig { get; set; }
    public DateTime ForecastStartDate { get; set; }
    public DateTime PlannedProcessingDate { get; set; }
    public decimal CurrentLiveWeightPerPig { get; set; }
    public decimal AverageDailyGainLbs { get; set; }
    public decimal DressingPercentageExpected { get; set; }
    public int PastureSeasonStartMonth { get; set; }
    public int PastureSeasonEndMonth { get; set; }
    public decimal WinterFeedMultiplierPercent { get; set; }
    public decimal WinterExtraCostPerPigPerDay { get; set; }

    public ForecastInputModel ToInputModel() => new()
    {
        BatchName = BatchName, ButcherProfileKey = ButcherProfileKey,
        PigCount = PigCount, MortalityPercent = MortalityPercent,
        PigSource = Enum.Parse<PigSource>(PigSource),
        RevenueMode = Enum.Parse<RevenueMode>(RevenueMode),
        PurchaseCostPerPig = PurchaseCostPerPig, BreedingAllocationPerPig = BreedingAllocationPerPig,
        VetCostPerPig = VetCostPerPig, VaccineCostPerPig = VaccineCostPerPig,
        DewormerCostPerPig = DewormerCostPerPig, BeddingCostPerPig = BeddingCostPerPig,
        MiscPigCostPerPig = MiscPigCostPerPig, UseFeedProjection = UseFeedProjection,
        FeedCostPerPigManual = FeedCostPerPigManual, BulkFeedCostPerTon = BulkFeedCostPerTon,
        FeedLbPerPigPerDay = FeedLbPerPigPerDay, SupplementFeedAdjustmentPercent = SupplementFeedAdjustmentPercent,
        GrowMonths = GrowMonths, PastureMonths = PastureMonths,
        HangingWeightLow = HangingWeightLow, HangingWeightExpected = HangingWeightExpected,
        HangingWeightHigh = HangingWeightHigh, ShrinkPercent = ShrinkPercent,
        YieldAdjustmentPercent = YieldAdjustmentPercent, WholeHalfPricePerLb = WholeHalfPricePerLb,
        RetailRevenuePerPigLow = RetailRevenuePerPigLow, RetailRevenuePerPigExpected = RetailRevenuePerPigExpected,
        RetailRevenuePerPigHigh = RetailRevenuePerPigHigh, ProcessingFlatPerPig = ProcessingFlatPerPig,
        ProcessingPerLb = ProcessingPerLb, EstimatedProcessingLbsPerPig = EstimatedProcessingLbsPerPig,
        SlaughterFeePerPig = SlaughterFeePerPig, SmokingCostPerLb = SmokingCostPerLb,
        EstimatedSmokedLbsPerPig = EstimatedSmokedLbsPerPig, SausageCostPerLb = SausageCostPerLb,
        EstimatedSausageLbsPerPig = EstimatedSausageLbsPerPig, ForecastStartDate = ForecastStartDate,
        PlannedProcessingDate = PlannedProcessingDate, CurrentLiveWeightPerPig = CurrentLiveWeightPerPig,
        AverageDailyGainLbs = AverageDailyGainLbs, DressingPercentageExpected = DressingPercentageExpected,
        PastureSeasonStartMonth = PastureSeasonStartMonth, PastureSeasonEndMonth = PastureSeasonEndMonth,
        WinterFeedMultiplierPercent = WinterFeedMultiplierPercent, WinterExtraCostPerPigPerDay = WinterExtraCostPerPigPerDay
    };

    public static ForecastBatchEntity FromInputModel(ForecastInputModel m) => new()
    {
        SavedAt = DateTime.UtcNow, BatchName = m.BatchName, ButcherProfileKey = m.ButcherProfileKey,
        PigCount = m.PigCount, MortalityPercent = m.MortalityPercent,
        PigSource = m.PigSource.ToString(), RevenueMode = m.RevenueMode.ToString(),
        PurchaseCostPerPig = m.PurchaseCostPerPig, BreedingAllocationPerPig = m.BreedingAllocationPerPig,
        VetCostPerPig = m.VetCostPerPig, VaccineCostPerPig = m.VaccineCostPerPig,
        DewormerCostPerPig = m.DewormerCostPerPig, BeddingCostPerPig = m.BeddingCostPerPig,
        MiscPigCostPerPig = m.MiscPigCostPerPig, UseFeedProjection = m.UseFeedProjection,
        FeedCostPerPigManual = m.FeedCostPerPigManual, BulkFeedCostPerTon = m.BulkFeedCostPerTon,
        FeedLbPerPigPerDay = m.FeedLbPerPigPerDay, SupplementFeedAdjustmentPercent = m.SupplementFeedAdjustmentPercent,
        GrowMonths = m.GrowMonths, PastureMonths = m.PastureMonths,
        HangingWeightLow = m.HangingWeightLow, HangingWeightExpected = m.HangingWeightExpected,
        HangingWeightHigh = m.HangingWeightHigh, ShrinkPercent = m.ShrinkPercent,
        YieldAdjustmentPercent = m.YieldAdjustmentPercent, WholeHalfPricePerLb = m.WholeHalfPricePerLb,
        RetailRevenuePerPigLow = m.RetailRevenuePerPigLow, RetailRevenuePerPigExpected = m.RetailRevenuePerPigExpected,
        RetailRevenuePerPigHigh = m.RetailRevenuePerPigHigh, ProcessingFlatPerPig = m.ProcessingFlatPerPig,
        ProcessingPerLb = m.ProcessingPerLb, EstimatedProcessingLbsPerPig = m.EstimatedProcessingLbsPerPig,
        SlaughterFeePerPig = m.SlaughterFeePerPig, SmokingCostPerLb = m.SmokingCostPerLb,
        EstimatedSmokedLbsPerPig = m.EstimatedSmokedLbsPerPig, SausageCostPerLb = m.SausageCostPerLb,
        EstimatedSausageLbsPerPig = m.EstimatedSausageLbsPerPig, ForecastStartDate = m.ForecastStartDate,
        PlannedProcessingDate = m.PlannedProcessingDate, CurrentLiveWeightPerPig = m.CurrentLiveWeightPerPig,
        AverageDailyGainLbs = m.AverageDailyGainLbs, DressingPercentageExpected = m.DressingPercentageExpected,
        PastureSeasonStartMonth = m.PastureSeasonStartMonth, PastureSeasonEndMonth = m.PastureSeasonEndMonth,
        WinterFeedMultiplierPercent = m.WinterFeedMultiplierPercent, WinterExtraCostPerPigPerDay = m.WinterExtraCostPerPigPerDay
    };
}
