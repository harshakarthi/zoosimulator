using ZooSimulator.API.Models;

namespace ZooSimulator.API.Services
{
    public interface IZooApiService
    {
        FeedResponse Feed();
        ZooStatusResponse GetStatus();
    }
}