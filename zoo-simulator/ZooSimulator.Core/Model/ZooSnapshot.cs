using System;
using System.Collections.Generic;
using System.Text;

namespace ZooSimulator.Core.Model
{
    public record ZooSnapshot(DateTime Time, List<AnimalInfo> Animals);
}
