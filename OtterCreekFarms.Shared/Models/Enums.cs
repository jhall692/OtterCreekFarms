namespace OtterCreekFarms.Shared.Models;

public enum PigSource        { Purchased, FarmBred }
public enum RevenueMode      { WholeHalf, RetailCuts }
public enum ExpenseFrequency { Monthly, Annual, OneTime, PerPig }

public enum AnimalStatus
{
    Active,
    Processed,
    Died,
    Sold,
    Removed
}

public enum AnimalSource
{
    Purchased,
    FarmBred
}

public enum AnimalRole
{
    Market,
    Breeding,
    Retained
}

public enum AnimalSex
{
    Boar,
    Barrow,
    Sow,
    Gilt
}

public enum WeightEstimateMethod { AdgProjection, TapeMeasure }
