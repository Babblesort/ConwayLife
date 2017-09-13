using ConwayLife.Domain;
using NUnit.Framework;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class PlayFieldTests
    {
        private PlayField _field;

        [Test]
        public void ExpectedTotalSize()
        {
            const int rows = 10;
            const int cols = 10;
            _field = new PlayField(rows, cols);
            Assert.AreEqual(rows * cols, _field.TotalCellCount);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldColumnsTooLarge()
        {
             var i = PlayField.MaxSize + 1;
            _field = new PlayField(1, i);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldRowsTooLarge()
        {
            var i = PlayField.MaxSize + 1;
            _field = new PlayField(i, 1);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldDimensionTooLarge()
        {
            var i = PlayField.MaxSize + 1;
            _field = new PlayField(i, i);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldDimensionTooSmall()
        {
            var i = PlayField.MinSize - 1;
            _field = new PlayField(i, i);
        }

    }
}
