using System;
using System.Collections.Generic;
using System.Text;
using ZooSimulator.Core.Configuration;
using ZooSimulator.Core.Services.Implementation;

namespace ZooSimulator.Tests
{
    public class ZooServiceTests
    {
        [Fact]
        public void Each_animal_gets_its_own_damage_value()
        {
            var config = new ZooConfiguration { AnimalsPerSpecies = 1 };
            var zoo = new ZooService(config, new FakeRandomProvider(5, 10, 15));

            zoo.AdvanceHour();
            var animals = zoo.GetAnimals();

            Assert.Equal(95, animals.Single(a => a.Name == "Monkey 1").Health, 9);
            Assert.Equal(90, animals.Single(a => a.Name == "Giraffe 1").Health, 9);
            Assert.Equal(85, animals.Single(a => a.Name == "Elephant 1").Health, 9);
        }
    }
}
