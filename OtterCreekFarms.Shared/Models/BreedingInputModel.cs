namespace OtterCreekFarms.Shared.Models;

public class BreedingInputModel
{
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
}
