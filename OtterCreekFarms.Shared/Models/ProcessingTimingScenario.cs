namespace OtterCreekFarms.Shared.Models;

public class ProcessingTimingScenarioModel
{
    public string Name { get; set; } = string.Empty;
    public DateTime ProcessingDate { get; set; }
    public int DelayDays { get; set; }
    public decimal ProjectedLiveWeight { get; set; }
    public decimal ProjectedHangingWeight { get; set; }
    public decimal AddedFeedCostPerPig { get; set; }
    public decimal AddedWinterCostPerPig { get; set; }
    public decimal TotalCostPerPig { get; set; }
    public decimal RevenuePerPig { get; set; }
    public decimal ProfitPerPig { get; set; }
    public decimal BatchProfit { get; set; }
    public decimal ProfitChangeVsNow { get; set; }
}
