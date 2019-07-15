using NUnit.Framework;
using Tetris.Core.Game.Colors;

namespace Tetris.Core.Tests.Game.Colors
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
