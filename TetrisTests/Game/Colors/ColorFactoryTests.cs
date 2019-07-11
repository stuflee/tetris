using NUnit.Framework;
using Tetris.Game.Colors;

namespace TetrisTests.Game.Colors
{
    public class ColorFactoryTests
    {
        [TestFixture]
        public class ConstructorTests : ColorFactoryTests
        {
            [TestCase]
            public void WhenFactoryIsConstructedItIsPopulated()
            {
                var factory = new ColorFactory(new System.Random());
                Assert.True(factory.GetItems.Count > 0);
            }
        }
    }
}
