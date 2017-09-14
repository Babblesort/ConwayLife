using ConwayLife.Domain;
using NUnit.Framework;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class LifeGameTests
    {
        private readonly LifeRules _rules = new LifeRules();
        private readonly PlayField _field = new PlayField(1, 1);

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullLifeRules()
        {
            var unused = new LifeGame(null, _field);
        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullPlayField()
        {
            var unused = new LifeGame(_rules, null);
        }
    }
}
