using System;
using System.Collections.Generic;
using System.Text;

namespace ZooSimulator.Core.Model
{
    public class Monkey : Animal
    {
        public Monkey(string name, double deathThreshold) : base(name, deathThreshold) { }

        public override string Species => "Monkey";

        protected override void CheckHealth()
        {
            if (Health < Threshold) Die();
        }
    }
}
