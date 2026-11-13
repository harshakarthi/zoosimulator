using System;
using System.Collections.Generic;
using System.Text;

namespace ZooSimulator.Core.Model
{
    public abstract class Animal
    {
        public string Name { get; }
        public abstract string Species { get; }
        public double Threshold { get; }
        public double Health { get; protected set; } = 100;
        public bool IsDead { get; protected set; }
        public bool CanWalk { get; protected set; } = true;
        public virtual string Status => IsDead ? "Dead" : "Alive";

        protected Animal(string name, double threshold)
        {
            Name = name;
            Threshold = threshold;
        }

        public void ApplyDamage(double percentage)
        {
            if (IsDead) return;

            Health -= Health * percentage / 100;
            CheckHealth();
        }

        public virtual void Feed(double percentage)
        {
            if (IsDead) return;

            Health = Math.Min(100, Health + Health * percentage / 100);
        }

        protected abstract void CheckHealth();

        protected void Die()
        {
            IsDead = true;
            CanWalk = false;
        }


    }
}
