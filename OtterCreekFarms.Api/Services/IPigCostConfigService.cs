using OtterCreekFarms.Shared.Models;
namespace OtterCreekFarms.Api.Services;
public interface IPigCostConfigService
{
    PigCostConfigEntity? GetLatest();
    PigCostInputModel GetLatestOrDefault();
    void Save(PigCostInputModel input);
}