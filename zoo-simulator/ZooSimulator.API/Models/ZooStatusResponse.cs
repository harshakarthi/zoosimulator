using ZooSimulator.Core.Model;

namespace ZooSimulator.API.Models
{
    public record ZooStatusResponse(DateTime ZooTime, double SecondsUntilNextHour, double HourLengthSeconds, int AliveCount, int TotalCount, List<Animal> Animals);

}
