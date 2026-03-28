using System.ComponentModel.DataAnnotations;

namespace OtterCreekFarms.Shared.Models;

public class BreedingConfigEntity
{
    [Key] public int Id { get; set; }
    [MaxLength(100)] public string ConfigName { get; set; } = "My Herd";
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    public int SowCount { get; set; }
    public int BoarCount { get; set; }
    public decimal GiltPurchaseCost { get; set; }
    public decimal BoarPurchaseCost { get; set; }
    public decimal OutsideBoarServiceCostAnnual { get; set; }
    public decimal AnnualBreederFeedCostPerSow { get; set; }
    public decimal AnnualBreederFeedCostPerBoar { get; set; }
    public decimal AnnualBreederVetCost { get; set; }
    public decimal AnnualBreederVaccineCost { get; set; }
    public decimal AnnualBreederMiscCost { get; set; }
    public decimal BreederHousingAllocationAnnual { get; set; }
    public decimal BreederFenceAllocationAnnual { get; set; }
    public decimal LittersPerSowPerYear { get; set; }
    public decimal AvgPigletsBornPerLitter { get; set; }
    public decimal AvgPigletsWeanedPerLitter { get; set; }
    public decimal MortalityPercentPreWeaning { get; set; }
    public decimal ReplacementRatePercent { get; set; }

    public BreedingInputModel ToInputModel() => new()
    {
        SowCount = SowCount, BoarCount = BoarCount,
        GiltPurchaseCost = GiltPurchaseCost, BoarPurchaseCost = BoarPurchaseCost,
        OutsideBoarServiceCostAnnual = OutsideBoarServiceCostAnnual,
        AnnualBreederFeedCostPerSow = AnnualBreederFeedCostPerSow,
        AnnualBreederFeedCostPerBoar = AnnualBreederFeedCostPerBoar,
        AnnualBreederVetCost = AnnualBreederVetCost,
        AnnualBreederVaccineCost = AnnualBreederVaccineCost,
        AnnualBreederMiscCost = AnnualBreederMiscCost,
        BreederHousingAllocationAnnual = BreederHousingAllocationAnnual,
        BreederFenceAllocationAnnual = BreederFenceAllocationAnnual,
        LittersPerSowPerYear = LittersPerSowPerYear,
        AvgPigletsBornPerLitter = AvgPigletsBornPerLitter,
        AvgPigletsWeanedPerLitter = AvgPigletsWeanedPerLitter,
        MortalityPercentPreWeaning = MortalityPercentPreWeaning,
        ReplacementRatePercent = ReplacementRatePercent
    };

    public static BreedingConfigEntity FromInputModel(BreedingInputModel m, string configName = "My Herd") => new()
    {
        ConfigName = configName, SavedAt = DateTime.UtcNow,
        SowCount = m.SowCount, BoarCount = m.BoarCount,
        GiltPurchaseCost = m.GiltPurchaseCost, BoarPurchaseCost = m.BoarPurchaseCost,
        OutsideBoarServiceCostAnnual = m.OutsideBoarServiceCostAnnual,
        AnnualBreederFeedCostPerSow = m.AnnualBreederFeedCostPerSow,
        AnnualBreederFeedCostPerBoar = m.AnnualBreederFeedCostPerBoar,
        AnnualBreederVetCost = m.AnnualBreederVetCost,
        AnnualBreederVaccineCost = m.AnnualBreederVaccineCost,
        AnnualBreederMiscCost = m.AnnualBreederMiscCost,
        BreederHousingAllocationAnnual = m.BreederHousingAllocationAnnual,
        BreederFenceAllocationAnnual = m.BreederFenceAllocationAnnual,
        LittersPerSowPerYear = m.LittersPerSowPerYear,
        AvgPigletsBornPerLitter = m.AvgPigletsBornPerLitter,
        AvgPigletsWeanedPerLitter = m.AvgPigletsWeanedPerLitter,
        MortalityPercentPreWeaning = m.MortalityPercentPreWeaning,
        ReplacementRatePercent = m.ReplacementRatePercent
    };
}
