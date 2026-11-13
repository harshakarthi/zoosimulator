using System;
using System.Collections.Generic;
using System.Text;

namespace ZooSimulator.Core.Model
{
    public record AnimalInfo(string Name, string Species, double Health, bool IsDead, bool CanWalk, string Status, double Threshold);
}
