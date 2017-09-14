using ConwayLife.Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class LifeRulesTests
    {
        private LifeRules _rules;

        [Test]
        public void EmptyConstructorDefaultsRuleLists()
        {
            var surviveList = new List<int> { 2, 3 };
            var birthList = new List<int> { 3 };
            _rules = new LifeRules();

            Assert.AreEqual(true, surviveList.SequenceEqual(_rules.SurvivalNeighborCounts));
            Assert.AreEqual(true, birthList.SequenceEqual(_rules.BirthNeighborCounts));
        }

        [Test]
        public void ParametizedConstructorSetsRuleLists()
        {
            var surviveList = new List<int> { 2, 3 };
            var birthList = new List<int> { 3 };
            _rules = new LifeRules(surviveList, birthList);

            Assert.AreEqual(true, surviveList.SequenceEqual(_rules.SurvivalNeighborCounts));
            Assert.AreEqual(true, birthList.SequenceEqual(_rules.BirthNeighborCounts));
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullSurviveList()
        {
            var unused = new LifeRules(null, new List<int> { 3 });
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullBirthList()
        {
            var unused = new LifeRules(new List<int> { 2, 3 }, null);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MinNeighborsExceeded()
        {
            var i = LifeRules.MinNeighborCount - 1;
            var unused = new LifeRules(new List<int> { i, 2 }, new List<int> { 3 });
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MaxNeighborsExceeded()
        {
            var i = LifeRules.MaxNeighborCount + 1;
            var unused = new LifeRules(new List<int> { 2, 3 }, new List<int> { i });
        }

    }
}
