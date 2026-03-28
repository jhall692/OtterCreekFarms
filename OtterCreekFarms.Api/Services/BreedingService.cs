using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Services;

public interface IBreedingService
{
    BreedingResultModel BuildBreedingForecast(BreedingInputModel input);
}

public class BreedingService : IBreedingService
{
    public BreedingResultModel BuildBreedingForecast(BreedingInputModel input)
    {
        var annualCost = CalcAnnualCost(input);
        var born   = input.SowCount * input.LittersPerSowPerYear * input.AvgPigletsBornPerLitter;
        var weaned = input.SowCount * input.LittersPerSowPerYear * input.AvgPigletsWeanedPerLitter
                     * (1m - input.MortalityPercentPreWeaning / 100m);
        weaned = Math.Max(0m, weaned);
        return new BreedingResultModel {
            AnnualBreederCost    = R(annualCost),
            PigletsBornPerYear   = R(born),
            PigletsWeanedPerYear = R(weaned),
            CostPerWeanedPiglet  = weaned > 0 ? R(annualCost / weaned) : 0m };
    }

    private static decimal CalcAnnualCost(BreedingInputModel i)
    {
        var purchase    = i.GiltPurchaseCost + i.BoarPurchaseCost + i.OutsideBoarServiceCostAnnual;
        var feed        = i.SowCount * i.AnnualBreederFeedCostPerSow + i.BoarCount * i.AnnualBreederFeedCostPerBoar;
        var health      = i.AnnualBreederVetCost + i.AnnualBreederVaccineCost + i.AnnualBreederMiscCost;
        var facility    = i.BreederHousingAllocationAnnual + i.BreederFenceAllocationAnnual;
        var replacement = purchase * (i.ReplacementRatePercent / 100m);
        return R(purchase + feed + health + facility + replacement);
    }

    private static decimal R(decimal v) => Math.Round(v, 2, MidpointRounding.AwayFromZero);
}
