using System.ComponentModel.DataAnnotations;

namespace OtterCreekFarms.Shared.Models;

public class ButcherProfileModel
{
    [Key][MaxLength(50)]  public string Key  { get; set; } = string.Empty;
    [MaxLength(100)] public string Name { get; set; } = string.Empty;
    [MaxLength(100)] public string City { get; set; } = string.Empty;
    public decimal ProcessingFlatPerHog { get; set; }
    public decimal ProcessingPerLb      { get; set; }
    public decimal SlaughterFeePerHog   { get; set; }
    public decimal SmokingPerLb         { get; set; }
    public decimal SausagePerLb         { get; set; }
    public decimal VacuumPackagingPerLb { get; set; }
    public decimal PattiesPerLb         { get; set; }
    public decimal GrindingPerLb        { get; set; }
    [MaxLength(500)] public string Notes { get; set; } = string.Empty;
}
