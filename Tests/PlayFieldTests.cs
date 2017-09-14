using ConwayLife.Domain;
using NUnit.Framework;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class PlayFieldTests
    {
        [Test]
        public void ExpectedTotalSize()
        {
            const int rows = 10;
            const int cols = 10;
            var field = new PlayField(rows, cols);
            Assert.AreEqual(rows * cols, field.TotalCellCount);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldColumnsTooLarge()
        {
             var i = PlayField.MaxSize + 1;
             var field = new PlayField(1, i);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldRowsTooLarge()
        {
            var i = PlayField.MaxSize + 1;
            var field = new PlayField(i, 1);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldDimensionTooLarge()
        {
            var i = PlayField.MaxSize + 1;
            var field = new PlayField(i, i);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldDimensionTooSmall()
        {
            var i = PlayField.MinSize - 1;
            var field = new PlayField(i, i);
        }

        [Test]
        public void CellIndex()
        {
            var field = new PlayField(3, 3);
            Assert.AreEqual(0, field.CellIndex(row: 0, col: 0));
            Assert.AreEqual(1, field.CellIndex(row: 0, col: 1));
            Assert.AreEqual(4, field.CellIndex(row: 1, col: 1));
            Assert.AreEqual(8, field.CellIndex(row: 2, col: 2));
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void CellIndex_ThrowsOnNegativeRow()
        {
            var field = new PlayField(3, 3);
            field.CellIndex(row: -1, col: 0);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void CellIndex_ThrowsOnNegativeCol()
        {
            var field = new PlayField(3, 3);
            field.CellIndex(row: 0, col: -1);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void CellIndex_ThrowsOn_CellIndexBeyondTotalCells()
        {
            var field = new PlayField(2, 2);
            field.CellIndex(row: 1, col: 2);
        }
    }
}
