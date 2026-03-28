namespace OtterCreekFarms.Shared.Models;

public class ProcessingActualsModel
{
    public DateTime ProcessingDate { get; set; } = DateTime.Today;
    public decimal ActualLiveWeight { get; set; }
    public decimal ActualHangingWeight { get; set; }
    public decimal ActualProcessingCost { get; set; }
    public decimal ActualRevenueTotal { get; set; }
    public string Notes { get; set; } = string.Empty;
}
