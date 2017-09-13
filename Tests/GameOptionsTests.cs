using ConwayLife.Domain;
using NUnit.Framework;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class GameOptionsTests
    {
        GameRunOptions testOptions;

        [SetUp]
        public void Init()
        {
            testOptions = new GameRunOptions();
        }

        [TearDown]
        public void Dispose()
        {
            testOptions = null;
        }

        [Test]
        public void AllowedGenerationsAssignment()
        {
            testOptions.AllowedGenerations = 10;
            Assert.AreEqual(10, testOptions.AllowedGenerations);
        }

        [Test]
        public void AllowedDelayStepAssignment()
        {
            testOptions.DelayStepMilliseconds = 200;
            Assert.AreEqual(200, testOptions.DelayStepMilliseconds);
        }

        [Test]
        public void HaltOnExtinctionAssignment()
        {
            testOptions.HaltOnExtinction = false;
            Assert.AreEqual(false, testOptions.HaltOnExtinction);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MinGenerationsExceeded()
        {
            int i = GameRunOptions.MinGenerations - 1;
            testOptions.AllowedGenerations = i;
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MaxGenerationsExceeded()
        {
            int i = GameRunOptions.MaxGenerations + 1;
            testOptions.AllowedGenerations = i;
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MinDelayExceeded()
        {
            int i = GameRunOptions.MinDelayMilliseconds - 1;
            testOptions.DelayStepMilliseconds = i;
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MaxDelayExceeded()
        {
            int i = GameRunOptions.MaxDelayMilliseconds + 1;
            testOptions.DelayStepMilliseconds = i;
        }

    }
}
