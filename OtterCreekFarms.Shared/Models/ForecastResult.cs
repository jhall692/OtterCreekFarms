namespace OtterCreekFarms.Shared.Models;

public class ForecastResultModel
{
    public int AdjustedPigCount { get; set; }
    public decimal DirectCostPerPig { get; set; }
    public decimal FeedCostPerPig { get; set; }
    public decimal ProcessingCostPerPig { get; set; }
    public decimal TotalCostPerPig { get; set; }
    public decimal TotalDirectCost { get; set; }
    public decimal TotalFeedCost { get; set; }
    public decimal TotalProcessingCost { get; set; }
    public decimal TotalBatchCost { get; set; }
    public decimal BreakEvenPerPig { get; set; }
    public decimal BreakEvenPerLbExpected { get; set; }
    public List<ForecastScenarioModel> Scenarios { get; set; } = new();
    public List<ProcessingTimingScenarioModel> TimingScenarios { get; set; } = new();
}
