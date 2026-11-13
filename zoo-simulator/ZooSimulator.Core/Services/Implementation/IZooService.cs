using ZooSimulator.Core.Model;

namespace ZooSimulator.Core.Services.Implementation
{
    public interface IZooService
    {
        DateTime CurrentTime { get; }

        void AdvanceHour();
        Dictionary<string, double> Feed();
        List<Animal> GetAnimals();
    }
}