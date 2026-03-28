using OtterCreekFarms.Shared.Models;
namespace OtterCreekFarms.Api.Services;
public interface IRevenueConfigService
{
    RevenueConfigEntity? GetLatest();
    RevenueInputModel GetLatestOrDefault();
    void Save(RevenueInputModel input);
}