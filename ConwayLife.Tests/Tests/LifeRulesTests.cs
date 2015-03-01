using ConwayLife.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class LifeRulesTests
    {
        LifeRules testLifeRules;

        [Test]
        public void ListsMatchConstructors()
        {
            List<int> surviveList = new List<int>() { 2, 3 };
            List<int> birthList = new List<int>() { 3 };
            testLifeRules = new LifeRules(surviveList, birthList);

            Assert.AreEqual(true, surviveList.SequenceEqual(testLifeRules.SurvivalNeighborCounts));
            Assert.AreEqual(true, birthList.SequenceEqual(testLifeRules.BirthNeighborCounts));
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullSurviveList()
        {
            var constructorFailLifeRules = new LifeRules(null, new List<int>() { 3 });
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullBirthList()
        {
            var constructorFailLifeRules = new LifeRules(new List<int>() { 2, 3 }, null);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MinNeighborsExceeded()
        {
            int i = LifeRules.MinNeighborCount - 1;
            var constructorFailLifeRules = new LifeRules(new List<int>() { i, 2 }, new List<int>() { 3 });
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MaxNeighborsExceeded()
        {
            int i = LifeRules.MaxNeighborCount + 1;
            var constructorFailLifeRules = new LifeRules(new List<int>() { 2, 3 }, new List<int>() { i });
        }

    }
}
