using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Services;

public interface IForecastService
{
    ForecastResultModel BuildForecast(ForecastInputModel input);
}

public class ForecastService : IForecastService
{
    public ForecastResultModel BuildForecast(ForecastInputModel input)
    {
        ArgumentNullException.ThrowIfNull(input);
        if (input.PigCount <= 0) throw new ArgumentException("Pig count must be > 0.");

        var adjustedCount        = CalcAdjustedCount(input.PigCount, input.MortalityPercent);
        var directCostPerPig     = CalcDirectCost(input);
        var feedCostPerPig       = CalcFeedCost(input);
        var processingCostPerPig = CalcProcessingCost(input);
        var totalCostPerPig      = directCostPerPig + feedCostPerPig + processingCostPerPig;
        var totalBatchCost       = totalCostPerPig * adjustedCount;

        var expectedWeight = ApplyWeightAdj(input.HangingWeightExpected, input.ShrinkPercent, input.YieldAdjustmentPercent);
        var breakEvenPerLb = expectedWeight > 0 ? totalCostPerPig / expectedWeight : 0m;

        return new ForecastResultModel
        {
            AdjustedPigCount       = adjustedCount,
            DirectCostPerPig       = R(directCostPerPig),
            FeedCostPerPig         = R(feedCostPerPig),
            ProcessingCostPerPig   = R(processingCostPerPig),
            TotalCostPerPig        = R(totalCostPerPig),
            TotalDirectCost        = R(directCostPerPig     * adjustedCount),
            TotalFeedCost          = R(feedCostPerPig       * adjustedCount),
            TotalProcessingCost    = R(processingCostPerPig * adjustedCount),
            TotalBatchCost         = R(totalBatchCost),
            BreakEvenPerPig        = R(totalCostPerPig),
            BreakEvenPerLbExpected = R(breakEvenPerLb),
            Scenarios              = BuildScenarios(input, adjustedCount, totalBatchCost),
            TimingScenarios        = BuildTimingScenarios(input, adjustedCount, directCostPerPig)
        };
    }

    private static int CalcAdjustedCount(int count, decimal mortality) =>
        Math.Max(0, (int)Math.Floor(count * Math.Max(0m, (100m - mortality) / 100m)));

    private static decimal CalcDirectCost(ForecastInputModel i)
    {
        var base_ = i.PigSource == PigSource.FarmBred ? i.BreedingAllocationPerPig : i.PurchaseCostPerPig;
        return R(base_ + i.VetCostPerPig + i.VaccineCostPerPig + i.DewormerCostPerPig + i.BeddingCostPerPig + i.MiscPigCostPerPig);
    }

    private static decimal CalcFeedCost(ForecastInputModel i)
    {
        if (!i.UseFeedProjection) return R(i.FeedCostPerPigManual);
        if (i.FeedLbPerPigPerDay <= 0) return 0m;
        var costPerLb   = i.BulkFeedCostPerTon / 2000m;
        var pastureAdj  = Math.Max(0m, 1m - i.SupplementFeedAdjustmentPercent / 100m);
        var pastureLbs  = i.FeedLbPerPigPerDay * Math.Max(0, i.PastureMonths) * 30m * pastureAdj;
        var winterLbs   = i.FeedLbPerPigPerDay * Math.Max(0, i.WinterMonths)  * 30m;
        return R((pastureLbs + winterLbs) * costPerLb);
    }

    private static decimal CalcProcessingCost(ForecastInputModel i) =>
        R(i.ProcessingFlatPerPig
        + i.ProcessingPerLb * i.EstimatedProcessingLbsPerPig
        + i.SlaughterFeePerPig
        + i.SmokingCostPerLb * i.EstimatedSmokedLbsPerPig
        + i.SausageCostPerLb * i.EstimatedSausageLbsPerPig);

    private static decimal ApplyWeightAdj(decimal w, decimal shrink, decimal yield) =>
        Math.Max(0m, w * (1m - shrink / 100m) * (1m + yield / 100m));

    private List<ForecastScenarioModel> BuildScenarios(ForecastInputModel input, int count, decimal totalCost)
    {
        var items = input.RevenueMode == RevenueMode.WholeHalf
            ? new[] {
                ("Low",      R(ApplyWeightAdj(input.HangingWeightLow,      input.ShrinkPercent, input.YieldAdjustmentPercent) * input.WholeHalfPricePerLb)),
                ("Expected", R(ApplyWeightAdj(input.HangingWeightExpected, input.ShrinkPercent, input.YieldAdjustmentPercent) * input.WholeHalfPricePerLb)),
                ("High",     R(ApplyWeightAdj(input.HangingWeightHigh,     input.ShrinkPercent, input.YieldAdjustmentPercent) * input.WholeHalfPricePerLb)) }
            : new[] {
                ("Low",      input.RetailRevenuePerPigLow),
                ("Expected", input.RetailRevenuePerPigExpected),
                ("High",     input.RetailRevenuePerPigHigh) };

        return items.Select(x => {
            var rev    = x.Item2 * count;
            var profit = rev - totalCost;
            var margin = rev == 0m ? 0m : profit / rev * 100m;
            return new ForecastScenarioModel {
                Name = x.Item1, Revenue = R(rev), Expense = R(totalCost),
                Profit = R(profit), ProfitPerPig = count == 0 ? 0m : R(profit / count),
                MarginPercent = R(margin) };
        }).ToList();
    }

    private List<ProcessingTimingScenarioModel> BuildTimingScenarios(ForecastInputModel input, int count, decimal directCost)
    {
        var start = input.ForecastStartDate.Date;
        var requested = input.PlannedProcessingDate.Date < start ? start : input.PlannedProcessingDate.Date;
        var reqDays = (requested - start).Days;

        var options = new List<(string, int)> { ("Process now",0),("+ 30 days",30),("+ 60 days",60),("+ 90 days",90) };
        if (reqDays > 0 && !options.Any(o => o.Item2 == reqDays)) options.Add(("Planned date", reqDays));

        ProcessingTimingScenarioModel? baseline = null;
        var result = new List<ProcessingTimingScenarioModel>();
        foreach (var (name, days) in options.OrderBy(o => o.Item2))
        {
            var s = BuildTimingScenario(input, count, directCost, start, name, days);
            s.ProfitChangeVsNow = baseline is null ? 0m : R(s.BatchProfit - baseline.BatchProfit);
            baseline ??= s;
            result.Add(s);
        }
        return result;
    }

    private ProcessingTimingScenarioModel BuildTimingScenario(
        ForecastInputModel input, int count, decimal directCost,
        DateTime start, string name, int days)
    {
        var date            = start.AddDays(days);
        var liveWt          = input.CurrentLiveWeightPerPig + input.AverageDailyGainLbs * days;
        var hangWt          = ApplyWeightAdj(liveWt * (input.DressingPercentageExpected / 100m), input.ShrinkPercent, input.YieldAdjustmentPercent);
        var scale           = input.HangingWeightExpected > 0m ? Math.Max(0m, hangWt / input.HangingWeightExpected) : 1m;
        var procCost        = R(input.ProcessingFlatPerPig + input.ProcessingPerLb * input.EstimatedProcessingLbsPerPig * scale + input.SlaughterFeePerPig + input.SmokingCostPerLb * input.EstimatedSmokedLbsPerPig * scale + input.SausageCostPerLb * input.EstimatedSausageLbsPerPig * scale);
        var (addFeed, addWinter) = CalcSeasonalCosts(input, start, date);
        var rev             = input.RevenueMode == RevenueMode.WholeHalf
            ? R(hangWt * input.WholeHalfPricePerLb)
            : input.HangingWeightExpected <= 0m ? 0m : R(input.RetailRevenuePerPigExpected * hangWt / input.HangingWeightExpected);
        var totalCost       = directCost + addFeed + addWinter + procCost;
        var profitPerPig    = rev - totalCost;
        return new ProcessingTimingScenarioModel {
            Name = name, ProcessingDate = date, DelayDays = days,
            ProjectedLiveWeight = R(liveWt), ProjectedHangingWeight = R(hangWt),
            AddedFeedCostPerPig = addFeed, AddedWinterCostPerPig = addWinter,
            TotalCostPerPig = R(totalCost), RevenuePerPig = rev,
            ProfitPerPig = R(profitPerPig), BatchProfit = R(profitPerPig * count) };
    }

    private (decimal feed, decimal winter) CalcSeasonalCosts(ForecastInputModel input, DateTime start, DateTime end)
    {
        if (end <= start) return (0m, 0m);
        var costPerLb = input.BulkFeedCostPerTon / 2000m;
        decimal feed = 0m, winter = 0m;
        var seg = start.Date;
        while (seg < end.Date)
        {
            var next = new DateTime(seg.Year, seg.Month, 1).AddMonths(1);
            var segEnd = next < end.Date ? next : end.Date;
            var days = (decimal)(segEnd - seg).Days;
            bool pasture = input.PastureSeasonStartMonth <= input.PastureSeasonEndMonth
                ? seg.Month >= input.PastureSeasonStartMonth && seg.Month <= input.PastureSeasonEndMonth
                : seg.Month >= input.PastureSeasonStartMonth || seg.Month <= input.PastureSeasonEndMonth;
            if (pasture)
                feed += input.FeedLbPerPigPerDay * Math.Max(0m, 1m - input.SupplementFeedAdjustmentPercent / 100m) * days * costPerLb;
            else {
                feed   += input.FeedLbPerPigPerDay * (1m + input.WinterFeedMultiplierPercent / 100m) * days * costPerLb;
                winter += input.WinterExtraCostPerPigPerDay * days;
            }
            seg = segEnd;
        }
        return (R(feed), R(winter));
    }

    private static decimal R(decimal v) => Math.Round(v, 2, MidpointRounding.AwayFromZero);
}
