using System.ComponentModel.DataAnnotations;

namespace OtterCreekFarms.Shared.Models;

public class RevenueConfigEntity
{
    [Key] public int Id { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    public RevenueMode RevenueMode                { get; set; } = RevenueMode.RetailCuts;
    public decimal     WholeHalfPricePerLb        { get; set; } = 5.50m;
    public decimal     RetailRevenuePerPigLow      { get; set; } = 1800m;
    public decimal     RetailRevenuePerPigExpected { get; set; } = 2200m;
    public decimal     RetailRevenuePerPigHigh     { get; set; } = 2600m;
    public decimal     HangingWeightLow            { get; set; } = 220m;
    public decimal     HangingWeightExpected       { get; set; } = 240m;
    public decimal     HangingWeightHigh           { get; set; } = 260m;
    public decimal     ShrinkPercent               { get; set; }
    public decimal     YieldAdjustmentPercent      { get; set; }
    public decimal     DressingPercentageExpected  { get; set; } = 72m;

    public RevenueInputModel ToInputModel() => new()
    {
        RevenueMode                 = RevenueMode,
        WholeHalfPricePerLb         = WholeHalfPricePerLb,
        RetailRevenuePerPigLow      = RetailRevenuePerPigLow,
        RetailRevenuePerPigExpected = RetailRevenuePerPigExpected,
        RetailRevenuePerPigHigh     = RetailRevenuePerPigHigh,
        HangingWeightLow            = HangingWeightLow,
        HangingWeightExpected       = HangingWeightExpected,
        HangingWeightHigh           = HangingWeightHigh,
        ShrinkPercent               = ShrinkPercent,
        YieldAdjustmentPercent      = YieldAdjustmentPercent,
        DressingPercentageExpected  = DressingPercentageExpected
    };

    public static RevenueConfigEntity FromInputModel(RevenueInputModel m) => new()
    {
        SavedAt                     = DateTime.UtcNow,
        RevenueMode                 = m.RevenueMode,
        WholeHalfPricePerLb         = m.WholeHalfPricePerLb,
        RetailRevenuePerPigLow      = m.RetailRevenuePerPigLow,
        RetailRevenuePerPigExpected = m.RetailRevenuePerPigExpected,
        RetailRevenuePerPigHigh     = m.RetailRevenuePerPigHigh,
        HangingWeightLow            = m.HangingWeightLow,
        HangingWeightExpected       = m.HangingWeightExpected,
        HangingWeightHigh           = m.HangingWeightHigh,
        ShrinkPercent               = m.ShrinkPercent,
        YieldAdjustmentPercent      = m.YieldAdjustmentPercent,
        DressingPercentageExpected  = m.DressingPercentageExpected
    };
}

public class RevenueInputModel
{
    public RevenueMode RevenueMode                { get; set; } = RevenueMode.RetailCuts;
    public decimal     WholeHalfPricePerLb        { get; set; } = 5.50m;
    public decimal     RetailRevenuePerPigLow      { get; set; } = 1800m;
    public decimal     RetailRevenuePerPigExpected { get; set; } = 2200m;
    public decimal     RetailRevenuePerPigHigh     { get; set; } = 2600m;
    public decimal     HangingWeightLow            { get; set; } = 220m;
    public decimal     HangingWeightExpected       { get; set; } = 240m;
    public decimal     HangingWeightHigh           { get; set; } = 260m;
    public decimal     ShrinkPercent               { get; set; }
    public decimal     YieldAdjustmentPercent      { get; set; }
    public decimal     DressingPercentageExpected  { get; set; } = 72m;
}
