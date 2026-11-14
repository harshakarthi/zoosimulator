using System;
using System.Collections.Generic;
using System.Text;
using ZooSimulator.Core.Model;

namespace ZooSimulator.Tests
{
    public class ElephantTests
    {
        [Fact]
        public void Elephant_still_below_70_after_the_next_hour_dies()
        {
            var elephant = new Elephant("Elephant 1", 70);
            elephant.ApplyDamage(31);   

            elephant.ApplyDamage(0);  

            Assert.True(elephant.IsDead);
        }
    }
}
