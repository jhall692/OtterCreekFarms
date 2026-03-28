namespace OtterCreekFarms.Shared.Models;

public class ForecastInputModel
{
    public string BatchName { get; set; } = string.Empty;
    public string ButcherProfileKey { get; set; } = string.Empty;
    public int PigCount { get; set; } = 1;
    public decimal MortalityPercent { get; set; }
    public PigSource PigSource { get; set; } = PigSource.Purchased;
    public decimal PurchaseCostPerPig { get; set; }
    public decimal BreedingAllocationPerPig { get; set; }
    public decimal VetCostPerPig { get; set; }
    public decimal VaccineCostPerPig { get; set; }
    public decimal DewormerCostPerPig { get; set; }
    public decimal BeddingCostPerPig { get; set; }
    public decimal MiscPigCostPerPig { get; set; }
    public bool UseFeedProjection { get; set; } = true;
    public decimal FeedCostPerPigManual { get; set; }
    public decimal BulkFeedCostPerTon { get; set; } = 430m;
    public decimal FeedLbPerPigPerDay { get; set; } = 6.7m;
    public decimal SupplementFeedAdjustmentPercent { get; set; }
    public int GrowMonths { get; set; } = 15;
    public int PastureMonths { get; set; } = 7;
    public int WinterMonths => GrowMonths - PastureMonths;
    public decimal HangingWeightLow { get; set; } = 220m;
    public decimal HangingWeightExpected { get; set; } = 240m;
    public decimal HangingWeightHigh { get; set; } = 260m;
    public decimal ShrinkPercent { get; set; }
    public decimal YieldAdjustmentPercent { get; set; }
    public RevenueMode RevenueMode { get; set; } = RevenueMode.WholeHalf;
    public decimal WholeHalfPricePerLb { get; set; } = 5.50m;
    public decimal RetailRevenuePerPigLow { get; set; }
    public decimal RetailRevenuePerPigExpected { get; set; }
    public decimal RetailRevenuePerPigHigh { get; set; }
    public decimal ProcessingFlatPerPig { get; set; } = 300m;
    public decimal ProcessingPerLb { get; set; }
    public decimal EstimatedProcessingLbsPerPig { get; set; }
    public decimal SlaughterFeePerPig { get; set; }
    public decimal SmokingCostPerLb { get; set; }
    public decimal EstimatedSmokedLbsPerPig { get; set; }
    public decimal SausageCostPerLb { get; set; }
    public decimal EstimatedSausageLbsPerPig { get; set; }
    public DateTime ForecastStartDate { get; set; } = DateTime.Today;
    public DateTime PlannedProcessingDate { get; set; } = DateTime.Today.AddDays(90);
    public decimal CurrentLiveWeightPerPig { get; set; } = 240m;
    public decimal AverageDailyGainLbs { get; set; } = 0.35m;
    public decimal DressingPercentageExpected { get; set; } = 72m;
    public int PastureSeasonStartMonth { get; set; } = 6;
    public int PastureSeasonEndMonth { get; set; } = 9;
    public decimal WinterFeedMultiplierPercent { get; set; } = 25m;
    public decimal WinterExtraCostPerPigPerDay { get; set; } = 0.15m;

    public void CopyFrom(ForecastInputModel s)
    {
        BatchName = s.BatchName; ButcherProfileKey = s.ButcherProfileKey;
        PigCount = s.PigCount; MortalityPercent = s.MortalityPercent;
        PigSource = s.PigSource; PurchaseCostPerPig = s.PurchaseCostPerPig;
        BreedingAllocationPerPig = s.BreedingAllocationPerPig; VetCostPerPig = s.VetCostPerPig;
        VaccineCostPerPig = s.VaccineCostPerPig; DewormerCostPerPig = s.DewormerCostPerPig;
        BeddingCostPerPig = s.BeddingCostPerPig; MiscPigCostPerPig = s.MiscPigCostPerPig;
        UseFeedProjection = s.UseFeedProjection; FeedCostPerPigManual = s.FeedCostPerPigManual;
        BulkFeedCostPerTon = s.BulkFeedCostPerTon; FeedLbPerPigPerDay = s.FeedLbPerPigPerDay;
        GrowMonths = s.GrowMonths; PastureMonths = s.PastureMonths;
        SupplementFeedAdjustmentPercent = s.SupplementFeedAdjustmentPercent;
        HangingWeightLow = s.HangingWeightLow; HangingWeightExpected = s.HangingWeightExpected;
        HangingWeightHigh = s.HangingWeightHigh; ShrinkPercent = s.ShrinkPercent;
        YieldAdjustmentPercent = s.YieldAdjustmentPercent; RevenueMode = s.RevenueMode;
        WholeHalfPricePerLb = s.WholeHalfPricePerLb;
        RetailRevenuePerPigLow = s.RetailRevenuePerPigLow;
        RetailRevenuePerPigExpected = s.RetailRevenuePerPigExpected;
        RetailRevenuePerPigHigh = s.RetailRevenuePerPigHigh;
        ProcessingFlatPerPig = s.ProcessingFlatPerPig; ProcessingPerLb = s.ProcessingPerLb;
        EstimatedProcessingLbsPerPig = s.EstimatedProcessingLbsPerPig;
        SlaughterFeePerPig = s.SlaughterFeePerPig; SmokingCostPerLb = s.SmokingCostPerLb;
        EstimatedSmokedLbsPerPig = s.EstimatedSmokedLbsPerPig;
        SausageCostPerLb = s.SausageCostPerLb; EstimatedSausageLbsPerPig = s.EstimatedSausageLbsPerPig;
        ForecastStartDate = s.ForecastStartDate; PlannedProcessingDate = s.PlannedProcessingDate;
        CurrentLiveWeightPerPig = s.CurrentLiveWeightPerPig;
        AverageDailyGainLbs = s.AverageDailyGainLbs;
        DressingPercentageExpected = s.DressingPercentageExpected;
        PastureSeasonStartMonth = s.PastureSeasonStartMonth;
        PastureSeasonEndMonth = s.PastureSeasonEndMonth;
        WinterFeedMultiplierPercent = s.WinterFeedMultiplierPercent;
        WinterExtraCostPerPigPerDay = s.WinterExtraCostPerPigPerDay;
    }
}
