using System.ComponentModel.DataAnnotations;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Shared.Models;

public class ExpenseItemModel
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public ExpenseFrequency Frequency { get; set; } = ExpenseFrequency.Monthly;
    public bool IsActive { get; set; } = true;
    public string Notes { get; set; } = string.Empty;
}
