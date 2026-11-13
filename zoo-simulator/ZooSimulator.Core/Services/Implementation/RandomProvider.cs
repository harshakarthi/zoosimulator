using System;
using System.Collections.Generic;
using System.Text;
using ZooSimulator.Core.Services.Interface;

namespace ZooSimulator.Core.Services.Implementation
{
    public class RandomProvider : IRandomProvider
    {
        public double NextDouble(double min, double max)
        {
            if (max < min)
            {
                throw new ArgumentException("max must be greater than or equal to min.", nameof(max));
            }

            return min + Random.Shared.NextDouble() * (max - min);
        }
    }
}
