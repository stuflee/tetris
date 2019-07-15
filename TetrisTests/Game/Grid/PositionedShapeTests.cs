using Moq;
using NUnit.Framework;
using System;
using System.Drawing;
using Tetris.Core.Game.Grid;
using Tetris.Core.Game.Shape;

namespace Tetris.Core.Tests.Game.Grid
{
    [TestFixture]
    public class PositionedShapeTests
    {
        public class ConstructorTests : PositionedShapeTests
        {
            [TestCase]
            public void WhenShapeIsNullAnExceptionIsThrown()
            {
                Assert.Throws(typeof(ArgumentNullException), () => new PositionedShape(null, Color.Red, new Point(1, 1)));
            }

            [TestCase("Red")] //Can't use Color directly as values are non const
            [TestCase("Green")]
            public void WhenConstructedColorIsSetCorrectly(string colorName)
            {
                var color = Color.FromName(colorName);
                var shape = new PositionedShape(new StaticShape(new[] { new Point(1, 1) }), color, new Point(1, 1));
                Assert.AreEqual(color, shape.Color);
            }

            [TestCase(1, 3)]
            [TestCase(2, 10)]
            public void WhenConstructedLocationIsSetCorrectly(int X, int Y)
            {
                var point = new Point(X, Y);
                var shape = new PositionedShape(new StaticShape(new[] { new Point(1, 1) }), Color.Red, point);
                Assert.AreEqual(point, shape.Location);
            }
        }

        public class MoveTests : PositionedShapeTests
        {
            [TestCase(1, 1, 1, 1)]
            [TestCase(1, 2, 3, 4)]
            public void WhenMoveIsCalledNewShapeIsMoved(int X, int Y, int offsetX, int offsetY)
            {
                var shapeMock = new Mock<ITetrisShape>();
                var shape = new PositionedShape(shapeMock.Object, Color.Red, new Point(X, Y));
                var movedShape = shape.Move(new Point(offsetX, offsetY));
                Assert.AreEqual(movedShape.Location, new Point(X + offsetX, Y + offsetY));
            }
        }

        public class RotateTests : PositionedShapeTests
        {
            [TestCase]
            public void WhenRotateIsCalledNRotatedShapeIsReturned()
            {
                var shapeMock = new Mock<ITetrisShape>();
                var rotatedShape = new Mock<ITetrisShape>();

                shapeMock.Setup(p => p.Rotate()).Returns(rotatedShape.Object);

                var shape = new PositionedShape(shapeMock.Object, Color.Red, new Point(0, 0));
                var movedShape = shape.Rotate();
                Assert.AreEqual(rotatedShape.Object, movedShape.Shape);
            }
        }
    }
}
