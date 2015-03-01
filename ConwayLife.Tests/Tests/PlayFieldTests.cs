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
    public class PlayFieldTests
    {
        PlayField testPlayField;

        [Test]
        public void ExpectedTotalSize()
        {
            int rows = 10;
            int cols = 10;
            testPlayField = new PlayField(rows, cols);
            Assert.AreEqual(rows * cols, testPlayField.TotalCellCount);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldDimensionTooLarge()
        {
            int i = PlayField.MaxSize + 1;
            testPlayField = new PlayField(i, i);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldDimensionTooSmall()
        {
            int i = PlayField.MinSize - 1;
            testPlayField = new PlayField(i, i);
        }

    }
}
