using NUnit.Framework;
using System;
using System.Drawing;
using Tetris.Core.Game.Shape;

namespace Tetris.Core.Tests.Game.Shape
{
    [TestFixture]
    public class FourRotationShapeTests
    {
        public class ConstructorTests : FourRotationShapeTests
        {
            [TestCase]
            public void WhenPointsIsNullAnExceptionIsThrown()
            {
                Assert.Throws(typeof(ArgumentNullException), () => new FourRotationShape(null));
            }

            [TestCase]
            public void WhenPointsIsNotNullPropertyIsCorrectlySet()
            {
                var shape = new FourRotationShape(new[] { new Point(1, 1) });
                Assert.AreEqual(1, shape.Points.Length);
                Assert.AreEqual(new Point(1, 1), shape.Points[0]);
            }
        }

        public class RotateTests : FourRotationShapeTests
        {

        }
    }
}
