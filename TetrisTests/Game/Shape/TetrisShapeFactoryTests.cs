using NUnit.Framework;
using Tetris.Core.Game.Shape;

namespace Tetris.Core.Tests.Game.Shape
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
