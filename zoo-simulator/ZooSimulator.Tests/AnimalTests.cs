using System;
using System.Collections.Generic;
using System.Text;
using ZooSimulator.Core.Model;

namespace ZooSimulator.Tests
{
    public class AnimalTests
    {
        [Fact]
        public void Damage_is_a_percentage_of_current_health()
        {
            var monkey = new Monkey("Monkey 1", 30);

            monkey.ApplyDamage(10);   
            monkey.ApplyDamage(10);   

            Assert.Equal(81, monkey.Health, 9);   
        }

        [Fact]
        public void Monkey_below_its_threshold_dies()
        {
            var monkey = new Monkey("Monkey 1", 30);

            monkey.ApplyDamage(71);   

            Assert.True(monkey.IsDead);
        }
    }
}
