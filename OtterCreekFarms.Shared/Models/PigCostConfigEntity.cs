using System.ComponentModel.DataAnnotations;

namespace OtterCreekFarms.Shared.Models;

public class PigCostConfigEntity
{
    [Key] public int Id { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    public PigSource   PigSource                    { get; set; } = PigSource.Purchased;
    public RevenueMode DefaultRevenueMode            { get; set; } = RevenueMode.RetailCuts;
    public decimal     MortalityPercent              { get; set; }
    public decimal     DefaultPurchaseCostPerPig     { get; set; } = 200m;
    public decimal     DefaultBreedingAllocationPerPig { get; set; }
    public decimal     DefaultVetCostPerPig          { get; set; }
    public decimal     DefaultVaccineCostPerPig      { get; set; }
    public decimal     DefaultDewormerCostPerPig     { get; set; }
    public decimal     DefaultBeddingCostPerPig      { get; set; }
    public decimal     DefaultMiscCostPerPig         { get; set; }

    public PigCostInputModel ToInputModel() => new()
    {
        PigSource                    = PigSource,
        DefaultRevenueMode           = DefaultRevenueMode,
        MortalityPercent             = MortalityPercent,
        DefaultPurchaseCostPerPig    = DefaultPurchaseCostPerPig,
        DefaultBreedingAllocationPerPig = DefaultBreedingAllocationPerPig,
        DefaultVetCostPerPig         = DefaultVetCostPerPig,
        DefaultVaccineCostPerPig     = DefaultVaccineCostPerPig,
        DefaultDewormerCostPerPig    = DefaultDewormerCostPerPig,
        DefaultBeddingCostPerPig     = DefaultBeddingCostPerPig,
        DefaultMiscCostPerPig        = DefaultMiscCostPerPig
    };

    public static PigCostConfigEntity FromInputModel(PigCostInputModel m) => new()
    {
        SavedAt                      = DateTime.UtcNow,
        PigSource                    = m.PigSource,
        DefaultRevenueMode           = m.DefaultRevenueMode,
        MortalityPercent             = m.MortalityPercent,
        DefaultPurchaseCostPerPig    = m.DefaultPurchaseCostPerPig,
        DefaultBreedingAllocationPerPig = m.DefaultBreedingAllocationPerPig,
        DefaultVetCostPerPig         = m.DefaultVetCostPerPig,
        DefaultVaccineCostPerPig     = m.DefaultVaccineCostPerPig,
        DefaultDewormerCostPerPig    = m.DefaultDewormerCostPerPig,
        DefaultBeddingCostPerPig     = m.DefaultBeddingCostPerPig,
        DefaultMiscCostPerPig        = m.DefaultMiscCostPerPig
    };
}

public class PigCostInputModel
{
    public PigSource   PigSource                    { get; set; } = PigSource.Purchased;
    public RevenueMode DefaultRevenueMode            { get; set; } = RevenueMode.RetailCuts;
    public decimal     MortalityPercent              { get; set; }
    public decimal     DefaultPurchaseCostPerPig     { get; set; } = 200m;
    public decimal     DefaultBreedingAllocationPerPig { get; set; }
    public decimal     DefaultVetCostPerPig          { get; set; }
    public decimal     DefaultVaccineCostPerPig      { get; set; }
    public decimal     DefaultDewormerCostPerPig     { get; set; }
    public decimal     DefaultBeddingCostPerPig      { get; set; }
    public decimal     DefaultMiscCostPerPig         { get; set; }
}
