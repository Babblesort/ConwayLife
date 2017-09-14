using ConwayLife.Domain;
using NUnit.Framework;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class RunOptionsTests
    {
        private RunOptions _options;

        [SetUp]
        public void Init()
        {
            _options = new RunOptions();
        }

        [TearDown]
        public void Dispose()
        {
            _options = null;
        }

        [Test]
        public void AllowedGenerationsAssignment()
        {
            _options.AllowedGenerations = 10;
            Assert.AreEqual(10, _options.AllowedGenerations);
        }

        [Test]
        public void AllowedDelayStepAssignment()
        {
            _options.DelayStepMilliseconds = 200;
            Assert.AreEqual(200, _options.DelayStepMilliseconds);
        }

        [Test]
        public void HaltOnExtinctionAssignment()
        {
            _options.HaltOnExtinction = false;
            Assert.AreEqual(false, _options.HaltOnExtinction);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MinGenerationsExceeded()
        {
            var i = RunOptions.MinGenerations - 1;
            _options.AllowedGenerations = i;
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MaxGenerationsExceeded()
        {
            var i = RunOptions.MaxGenerations + 1;
            _options.AllowedGenerations = i;
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MinDelayExceeded()
        {
            var i = RunOptions.MinDelayMilliseconds - 1;
            _options.DelayStepMilliseconds = i;
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void MaxDelayExceeded()
        {
            var i = RunOptions.MaxDelayMilliseconds + 1;
            _options.DelayStepMilliseconds = i;
        }
    }
}
