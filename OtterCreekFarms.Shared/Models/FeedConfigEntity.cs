using System.ComponentModel.DataAnnotations;

namespace OtterCreekFarms.Shared.Models;

public class FeedConfigEntity
{
    [Key] public int Id { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    public bool    UseFeedProjection               { get; set; } = true;
    public decimal BulkFeedCostPerTon              { get; set; } = 430m;
    public decimal FeedLbPerPigPerDay              { get; set; } = 6.7m;
    public decimal SupplementFeedAdjustmentPercent { get; set; } = 25m;
    public int     GrowMonths                      { get; set; } = 18;
    public int     PastureMonths                   { get; set; } = 10;
    public int     PastureSeasonStartMonth         { get; set; } = 5;
    public int     PastureSeasonEndMonth           { get; set; } = 10;
    public decimal WinterFeedMultiplierPercent     { get; set; } = 25m;
    public decimal WinterExtraCostPerPigPerDay     { get; set; } = 0.15m;
    public decimal FeedCostPerPigManual            { get; set; }

    public FeedInputModel ToInputModel() => new()
    {
        UseFeedProjection               = UseFeedProjection,
        BulkFeedCostPerTon              = BulkFeedCostPerTon,
        FeedLbPerPigPerDay              = FeedLbPerPigPerDay,
        SupplementFeedAdjustmentPercent = SupplementFeedAdjustmentPercent,
        GrowMonths                      = GrowMonths,
        PastureMonths                   = PastureMonths,
        PastureSeasonStartMonth         = PastureSeasonStartMonth,
        PastureSeasonEndMonth           = PastureSeasonEndMonth,
        WinterFeedMultiplierPercent     = WinterFeedMultiplierPercent,
        WinterExtraCostPerPigPerDay     = WinterExtraCostPerPigPerDay,
        FeedCostPerPigManual            = FeedCostPerPigManual
    };

    public static FeedConfigEntity FromInputModel(FeedInputModel m) => new()
    {
        SavedAt                         = DateTime.UtcNow,
        UseFeedProjection               = m.UseFeedProjection,
        BulkFeedCostPerTon              = m.BulkFeedCostPerTon,
        FeedLbPerPigPerDay              = m.FeedLbPerPigPerDay,
        SupplementFeedAdjustmentPercent = m.SupplementFeedAdjustmentPercent,
        GrowMonths                      = m.GrowMonths,
        PastureMonths                   = m.PastureMonths,
        PastureSeasonStartMonth         = m.PastureSeasonStartMonth,
        PastureSeasonEndMonth           = m.PastureSeasonEndMonth,
        WinterFeedMultiplierPercent     = m.WinterFeedMultiplierPercent,
        WinterExtraCostPerPigPerDay     = m.WinterExtraCostPerPigPerDay,
        FeedCostPerPigManual            = m.FeedCostPerPigManual
    };
}

public class FeedInputModel
{
    public bool    UseFeedProjection               { get; set; } = true;
    public decimal BulkFeedCostPerTon              { get; set; } = 430m;
    public decimal FeedLbPerPigPerDay              { get; set; } = 6.7m;
    public decimal SupplementFeedAdjustmentPercent { get; set; } = 25m;
    public int     GrowMonths                      { get; set; } = 18;
    public int     PastureMonths                   { get; set; } = 10;
    public int     PastureSeasonStartMonth         { get; set; } = 5;
    public int     PastureSeasonEndMonth           { get; set; } = 10;
    public decimal WinterFeedMultiplierPercent     { get; set; } = 25m;
    public decimal WinterExtraCostPerPigPerDay     { get; set; } = 0.15m;
    public decimal FeedCostPerPigManual            { get; set; }
}
