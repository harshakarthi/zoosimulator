using System;
using System.Collections.Generic;
using System.Text;
using ZooSimulator.Core.Services.Interface;

namespace ZooSimulator.Tests
{
    public class FakeRandomProvider : IRandomProvider
    {
        private readonly Queue<double> _values;
        private double _last;

        public FakeRandomProvider(params double[] values)
        {
            _values = new Queue<double>(values);
            _last = values[^1];
        }

        public double NextDouble(double min, double max)
        {
            if (_values.Count > 0) _last = _values.Dequeue();
            return _last;
        }
    }
}
