using ZooSimulator.Core.Configuration;
using ZooSimulator.Core.Services.Implementation;

namespace ZooSimulator.API.Services
{
    public class ZooSimulationService : BackgroundService
    {
        private readonly IZooService _zoo;
        private readonly TimeSpan _hourLength;
        private DateTime _hourStartedAt = DateTime.UtcNow;

        public ZooSimulationService(IZooService zoo, ZooConfiguration config)
        {
            _zoo = zoo;
            _hourLength = TimeSpan.FromSeconds(config.HourLengthSeconds);
        }

        public double HourLengthSeconds => _hourLength.TotalSeconds;

        public double SecondsUntilNextHour => Math.Max(0, HourLengthSeconds - (DateTime.UtcNow - _hourStartedAt).TotalSeconds);

        public DateTime GetZooTime()
        {
            double secondsIntoHour = (DateTime.UtcNow - _hourStartedAt).TotalSeconds;
            int minutes = Math.Min(59, (int)(secondsIntoHour / HourLengthSeconds * 60));
            return _zoo.CurrentTime.AddMinutes(minutes);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_hourLength, stoppingToken);
                _zoo.AdvanceHour();
                _hourStartedAt = DateTime.UtcNow;
            }
        }
    }
}
