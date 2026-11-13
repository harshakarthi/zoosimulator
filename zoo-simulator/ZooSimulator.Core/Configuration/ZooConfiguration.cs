using System;
using System.Collections.Generic;
using System.Text;

namespace ZooSimulator.Core.Configuration
{
    public class ZooConfiguration
    {
        public const string SectionName = "Zoo";
        public int HourLengthSeconds { get; set; } = 20;
        public int OpeningHour { get; set; } = 9;
        public int AnimalsPerSpecies { get; set; } = 5;
        public double HourlyDamageMin { get; set; } = 0;
        public double HourlyDamageMax { get; set; } = 20;
        public double FeedMin { get; set; } = 10;
        public double FeedMax { get; set; } = 25;
        public double MonkeyDeathThreshold { get; set; } = 30;
        public double GiraffeDeathThreshold { get; set; } = 50;
        public double ElephantWalkingThreshold { get; set; } = 70;

    }
}
