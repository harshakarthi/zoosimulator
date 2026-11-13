using System;
using System.Collections.Generic;
using System.Text;
using ZooSimulator.Core.Configuration;
using ZooSimulator.Core.Model;
using ZooSimulator.Core.Services.Interface;

namespace ZooSimulator.Core.Services.Implementation
{
    public class ZooService : IZooService
    {
        private readonly List<Animal> _animals = new();
        private readonly ZooConfiguration _config;
        private readonly IRandomProvider _random;
        private readonly Lock _lock = new();
        private DateTime _currentTime;

        public ZooService(ZooConfiguration config, IRandomProvider random)
        {
            _config = config;
            _random = random;
            _currentTime = DateTime.Today.AddHours(config.OpeningHour);

            for (int i = 1; i <= config.AnimalsPerSpecies; i++)
            {
                _animals.Add(new Monkey($"Monkey {i}", config.MonkeyDeathThreshold));
                _animals.Add(new Giraffe($"Giraffe {i}", config.GiraffeDeathThreshold));
                _animals.Add(new Elephant($"Elephant {i}", config.ElephantWalkingThreshold));
            }
        }

        public DateTime CurrentTime
        {
            get { lock (_lock) return _currentTime; }
        }

        public List<Animal> GetAnimals()
        {
            lock (_lock) return _animals.ToList();
        }


        public void AdvanceHour()
        {
            lock (_lock)
            {
                foreach (var animal in _animals)
                    animal.ApplyDamage(_random.NextDouble(_config.HourlyDamageMin, _config.HourlyDamageMax));

                _currentTime = _currentTime.AddHours(1);
            }
        }


        public Dictionary<string, double> Feed()
        {
            lock (_lock)
            {
                var feedBySpecies = new Dictionary<string, double>();

                foreach (var species in _animals.Select(a => a.Species).Distinct())
                    feedBySpecies[species] = _random.NextDouble(_config.FeedMin, _config.FeedMax);

                foreach (var animal in _animals)
                    animal.Feed(feedBySpecies[animal.Species]);

                return feedBySpecies;
            }
        }
    }
}
