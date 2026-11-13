using ZooSimulator.API.Models;
using ZooSimulator.Core.Services.Implementation;

namespace ZooSimulator.API.Services
{
    public class ZooApiService : IZooApiService
    {
        private readonly IZooService _zoo;
        private readonly ZooSimulationService _simulation;

        public ZooApiService(IZooService zoo, ZooSimulationService simulation)
        {
            _zoo = zoo;
            _simulation = simulation;
        }

        public ZooStatusResponse GetStatus()
        {
            var animals = _zoo.GetAnimals();

            return new ZooStatusResponse(
                ZooTime: _simulation.GetZooTime(),
                SecondsUntilNextHour: _simulation.SecondsUntilNextHour,
                HourLengthSeconds: _simulation.HourLengthSeconds,
                AliveCount: animals.Count(a => !a.IsDead),
                TotalCount: animals.Count,
                Animals: animals);
        }

        public FeedResponse Feed()
        {
            var feeds = _zoo.Feed()
                .Select(f => new SpeciesFeed(f.Key, f.Value))
                .ToList();

            var status = GetStatus();
            return new FeedResponse(status.ZooTime, feeds, status);
        }
    }
}
