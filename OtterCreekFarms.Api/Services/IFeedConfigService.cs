using OtterCreekFarms.Shared.Models;
namespace OtterCreekFarms.Api.Services;
public interface IFeedConfigService
{
    FeedConfigEntity? GetLatest();
    FeedInputModel GetLatestOrDefault();
    void Save(FeedInputModel input);
}