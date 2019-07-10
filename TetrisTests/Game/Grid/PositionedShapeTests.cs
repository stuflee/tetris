using NUnit.Framework;
using System;
using System.Drawing;
using Tetris.Game.Grid;
using Tetris.Game.Shape;

namespace TetrisTests.Game.Grid
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
                var shape = new PositionedShape(new Square(), color, new Point(1, 1));
                Assert.AreEqual(color, shape.Color);
            }

            [TestCase(1, 3)]
            [TestCase(2, 10)]
            public void WhenConstructedLocationIsSetCorrectly(int X, int Y)
            {
                var point = new Point(X, Y);
                var shape = new PositionedShape(new Square(), Color.Red, point);
                Assert.AreEqual(point, shape.Location);
            }
        }
    }
}
