namespace OtterCreekFarms.Shared.Models;

public class ForecastScenarioModel
{
    public string Name { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal Expense { get; set; }
    public decimal Profit { get; set; }
    public decimal ProfitPerPig { get; set; }
    public decimal MarginPercent { get; set; }
}
