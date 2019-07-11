using NUnit.Framework;
using Tetris.Game.Shape;

namespace TetrisTests.Game.Shape
{
    [TestFixture]
    public class TetrisShapeFactoryTests
    {
        public class ConstructorTests : TetrisShapeFactoryTests
        {
            [TestCase]
            public void WhenFactoryIsConstructedItIsPopulated()
            {
                var factory = new TetrisShapeFactory(new System.Random());
                Assert.True(factory.GetItems.Count > 0);
            }
        }
    }
}
