using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtterCreekFarms.Api.Models;
using OtterCreekFarms.Shared.Models;

namespace OtterCreekFarms.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ExpenseItemModel>     Expenses        => Set<ExpenseItemModel>();
    public DbSet<ForecastBatchEntity>  ForecastBatches => Set<ForecastBatchEntity>();
    public DbSet<BreedingConfigEntity> BreedingConfigs => Set<BreedingConfigEntity>();
    public DbSet<ButcherProfileModel>  ButcherProfiles => Set<ButcherProfileModel>();
    public DbSet<BatchAnimalEntity>    BatchAnimals         => Set<BatchAnimalEntity>();
    public DbSet<FeedConfigEntity>     FeedConfigs     => Set<FeedConfigEntity>();
    public DbSet<RevenueConfigEntity>  RevenueConfigs  => Set<RevenueConfigEntity>();
    public DbSet<PigCostConfigEntity>  PigCostConfigs  => Set<PigCostConfigEntity>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<ButcherProfileModel>(e =>
        {
            e.ToTable("ButcherProfiles");
            e.HasKey(x => x.Key);
            e.HasData(
                new ButcherProfileModel { Key = "ortys",     Name = "Orty's Meat Market",   City = "Deer River, MN",    ProcessingFlatPerHog = 0m,   ProcessingPerLb = 0.75m, SlaughterFeePerHog = 50m, SmokingPerLb = 1.50m, SausagePerLb = 0.60m },
                new ButcherProfileModel { Key = "braham",    Name = "Braham Meats",           City = "Braham, MN",        ProcessingFlatPerHog = 300m, ProcessingPerLb = 0m,    SlaughterFeePerHog = 0m,  SmokingPerLb = 0m,    SausagePerLb = 0m    },
                new ButcherProfileModel { Key = "lakehaven", Name = "Lake Haven Meats",       City = "Sturgeon Lake, MN", ProcessingFlatPerHog = 0m,   ProcessingPerLb = 0.90m, SlaughterFeePerHog = 40m, SmokingPerLb = 1.75m, SausagePerLb = 0.75m },
                new ButcherProfileModel { Key = "custom",    Name = "Custom / Manual Entry",  City = "",                  ProcessingFlatPerHog = 0m,   ProcessingPerLb = 0m,    SlaughterFeePerHog = 0m,  SmokingPerLb = 0m,    SausagePerLb = 0m    }
            );
        });

        mb.Entity<ExpenseItemModel>(e =>
        {
            e.ToTable("Expenses");
            e.HasKey(x => x.Id);
            e.Property(x => x.Frequency).HasConversion<string>();
        });

        mb.Entity<BatchAnimalEntity>(e =>
        {
            e.ToTable("BatchAnimals");
            e.HasKey(x => x.Id);
            e.Property(x => x.WeightMethod).HasConversion<string>();
            e.Property(x => x.Sex).HasConversion<string>();
            e.Property(x => x.Role).HasConversion<string>();
            e.Property(x => x.Status).HasConversion<string>();
            e.Property(x => x.Source).HasConversion<string>();
            e.Property(x => x.DefaultRevenueMode).HasConversion<string>();
        });

        mb.Entity<ForecastBatchEntity>(e =>  { e.ToTable("ForecastBatches"); e.HasKey(x => x.Id); });
        mb.Entity<BreedingConfigEntity>(e => { e.ToTable("BreedingConfigs"); e.HasKey(x => x.Id); });
        mb.Entity<FeedConfigEntity>(e =>     { e.ToTable("FeedConfigs");     e.HasKey(x => x.Id); });
        mb.Entity<RevenueConfigEntity>(e =>  { e.ToTable("RevenueConfigs");  e.HasKey(x => x.Id); });
        mb.Entity<PigCostConfigEntity>(e =>  { e.ToTable("PigCostConfigs");  e.HasKey(x => x.Id); });
    }
}
