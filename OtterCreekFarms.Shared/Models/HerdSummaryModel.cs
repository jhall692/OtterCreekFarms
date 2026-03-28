namespace OtterCreekFarms.Shared.Models;

public class HerdSummaryModel
{
    public int     ActiveCount                  { get; set; }
    public int     MarketCount                  { get; set; }
    public int     BreedingCount                { get; set; }
    public decimal EstimatedTotalLiveWeightLbs  { get; set; }
    public decimal EstimatedDailyFeedCostTotal  { get; set; }
    public decimal EstimatedDailyFeedLbsTotal   { get; set; }
    public int     ProcessedThisYear            { get; set; }
    public int     TargetMarketCount            { get; set; }
    public int     MarketHeadcountGap           => Math.Max(0, TargetMarketCount - MarketCount);
    public List<BatchAnimalEntity> ActiveAnimals    { get; set; } = new();
    public List<BatchAnimalEntity> ProcessedAnimals { get; set; } = new();
}
