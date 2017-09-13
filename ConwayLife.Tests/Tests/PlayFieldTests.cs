﻿using ConwayLife.Domain;
using NUnit.Framework;

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
        public void PlayFieldColumnsTooLarge()
        {
            int i = PlayField.MaxSize + 1;
            testPlayField = new PlayField(1, i);
        }

        [Test]
        [ExpectedException("System.ArgumentOutOfRangeException")]
        public void PlayFieldRowsTooLarge()
        {
            int i = PlayField.MaxSize + 1;
            testPlayField = new PlayField(i, 1);
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
