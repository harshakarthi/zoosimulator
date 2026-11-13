using System;
using System.Collections.Generic;
using System.Text;

namespace ZooSimulator.Core.Model
{
    public class Elephant : Animal
    {
        public Elephant(string name, double walkingThreshold) : base(name, walkingThreshold) { }

        public override string Species => "Elephant";

        public override string Status =>
            !IsDead && !CanWalk ? "Alive - cannot walk" : base.Status;

        protected override void CheckHealth()
        {
            bool couldNotWalkLastHour = !CanWalk;

            CanWalk = Health >= Threshold;

            if (!CanWalk && couldNotWalkLastHour)
                Die();
        }

        public override void Feed(double percentage)
        {
            base.Feed(percentage);

            if (Health >= Threshold)
                CanWalk = true;
        }
    }
}
