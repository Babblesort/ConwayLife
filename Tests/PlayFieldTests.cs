using ConwayLife.Domain;
using NUnit.Framework;
using System.Linq;

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

        [TestCase(0, 0, -1, -1, -1, -1, 1, -1, 3, 4)]
        [TestCase(0, 1, -1, -1, -1, 0, 2, 3, 4, 5)]
        [TestCase(0, 2, -1, -1, -1, 1, -1, 4, 5, -1)]
        [TestCase(1, 0, -1, 0, 1, -1, 4, -1, 6, 7)]
        [TestCase(1, 1, 0, 1, 2, 3, 5, 6, 7, 8)]
        [TestCase(1, 2, 1, 2, -1, 4, -1, 7, 8, -1)]
        [TestCase(2, 0, -1, 3, 4, -1, 7, -1, -1, -1)]
        [TestCase(2, 1, 3, 4, 5, 6, 8, -1, -1, -1)]
        [TestCase(2, 2, 4, 5, -1, 7, -1, -1, -1, -1)]
        public void NeighborIndices(int row, int col, params int[] expectedIndices)
        {
            var field = new PlayField(3, 3);
            Assert.AreEqual(expectedIndices[0], field.TopLeftNeighborIndex(row, col));
            Assert.AreEqual(expectedIndices[1], field.TopNeighborIndex(row, col));
            Assert.AreEqual(expectedIndices[2], field.TopRightNeighborIndex(row, col));
            Assert.AreEqual(expectedIndices[3], field.LeftNeighborIndex(row, col));
            Assert.AreEqual(expectedIndices[4], field.RightNeighborIndex(row, col));
            Assert.AreEqual(expectedIndices[5], field.BottomLeftNeighborIndex(row, col));
            Assert.AreEqual(expectedIndices[6], field.BottomNeighborIndex(row, col));
            Assert.AreEqual(expectedIndices[7], field.BottomRightNeighborIndex(row, col));
        }

        [Test]
        public void AllowsCreationOfFreshCells()
        {
            var cells = new PlayField(2, 2).FreshCells;
            Assert.AreEqual(4, cells.Count);
            Assert.IsTrue(cells.All(c => c == false));
        }

        [Test]
        public void AllowsCreationOfRandomCells()
        {
            var cells = new PlayField(2, 2).RandomCells;
            Assert.AreEqual(4, cells.Count);
        }

        [Test]
        public void RaisesEvent_PlayFieldSizeChanged_OnColsSet()
        {
            var field = new PlayField(1, 1);
            var wasCalled = false;
            field.PlayFieldSizeChanged += (sender, e) =>
            {
                wasCalled = true;
                Assert.IsInstanceOf<PlayField>(sender);
                Assert.AreEqual(1, e.Rows);
                Assert.AreEqual(2, e.Cols);
            };

            field.Cols = 2;
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void RaisesEvent_PlayFieldSizeChanged_OnRowsSet()
        {
            var field = new PlayField(1, 1);
            var wasCalled = false;
            field.PlayFieldSizeChanged += (sender, e) =>
            {
                wasCalled = true;
                Assert.IsInstanceOf<PlayField>(sender);
                Assert.AreEqual(2, e.Rows);
                Assert.AreEqual(1, e.Cols);
            };

            field.Rows = 2;
            Assert.IsTrue(wasCalled);
        }
    }
}
