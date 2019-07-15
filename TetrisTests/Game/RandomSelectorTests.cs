using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tetris.Game;

namespace TetrisTests.Game
{
    [TestFixture]
    public class RandomSelectorTests
    {
        public class ConstructorTests : RandomSelectorTests
        {
            [TestCase]
            public void IfItemsIsNullAnExpectionIsRaised()
            {
                Assert.Throws(typeof(ArgumentNullException), () => new RandomSelector<int>(new Random(), null));
            }

            [TestCase]
            public void IfItemsIsEmptyAnExpectionIsRaised()
            {
                Assert.Throws(typeof(ArgumentException), () => new RandomSelector<int>(new Random(), new List<int>()));
            }
        }

        public class GetNextTests : RandomSelectorTests
        {
            [TestCase]
            public void AnItemIsReturned()
            {
                var itemList = new List<int>() { 1, 2, 3 };
                var selector = new RandomSelector<int>(new Random(), itemList);

                var next = selector.GetNext();

                Assert.True(itemList.Contains(next));
            }

            [TestCase]
            public void WhenCalledMultipleTimesItemsAreRandom()
            {
                var itemList = new List<int>() { 1, 2, 3 };
                var random = new Mock<Random>();
                random.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(0).Returns(2).Returns(1);
                var selector = new RandomSelector<int>(random.Object, itemList);

                var first = selector.GetNext();
                var second = selector.GetNext();
                var third = selector.GetNext();

                Assert.AreEqual(first, itemList[0]);
                Assert.AreEqual(second, itemList[2]);
                Assert.AreEqual(third, itemList[1]);
            }
        }

        public class PeekNextTests : RandomSelectorTests
        {
            [TestCase]
            public void AnItemIsReturned()
            {
                var itemList = new List<int>() { 1, 2, 3 };
                var selector = new RandomSelector<int>(new Random(), itemList);

                var next = selector.PeekNext();

                Assert.True(itemList.Contains(next));
            }

            [TestCase]
            public void WhenCalledMultipleTimesTheSameItemIsReturned()
            {
                var itemList = new List<int>() { 1, 2, 3 };
                var random = new Mock<Random>();
                random.SetupSequence(r => r.Next(It.IsAny<int>())).Returns(0).Returns(2);
                var selector = new RandomSelector<int>(random.Object, itemList);

                var first = selector.PeekNext();
                var second = selector.PeekNext();

                Assert.AreEqual(first, itemList[0]);
                Assert.AreEqual(second, first);
            }
        }
    }
}
