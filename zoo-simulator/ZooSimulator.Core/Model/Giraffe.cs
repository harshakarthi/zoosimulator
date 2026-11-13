using System;
using System.Collections.Generic;
using System.Text;

namespace ZooSimulator.Core.Model
{
    public class Giraffe : Animal
    {
        public Giraffe(string name, double deathThreshold) : base(name, deathThreshold) { }
        public override string Species => "Giraffe";

        protected override void CheckHealth()
        {
            if (Health < Threshold) Die();
        }
    }
}
