using NUnit.Framework;
using System;
using System.Drawing;
using Tetris.Core.Game.Grid;

namespace Tetris.Core.Tests.Game.Grid
{
    [TestFixture]
    public class ColouredPointTests
    {
        public class ConstructorTests : ColouredPointTests
        {
            [TestCase("Red")] //Can't use Color directly as values are non const
            [TestCase("Green")]
            public void ColorIsSetCorrectly(string colorName)
            {
                var color = Color.FromName(colorName);
                var colouredPt = new ColouredPoint(color, new Point(1, 1));
                Assert.AreEqual(color, colouredPt.Color);
            }

            [TestCase(1, 3)]
            [TestCase(2, 10)]
            public void WhenConstructedPointIsSetCorrectly(int X, int Y)
            {
                var point = new Point(X, Y);
                var shape = new ColouredPoint(Color.Red, point);
                Assert.AreEqual(point, shape.Point);
            }
        }

        public class EqualsTests : ColouredPointTests
        {
            [TestCase(1, 3, "Red")]
            [TestCase(2, 7, "Green")]
            public void TwoPointsWithTheSameParametersAreEqual(int X, int Y, string colourName)
            {
                var point1 = new ColouredPoint(Color.FromName(colourName), new Point(X, Y));
                var point2 = new ColouredPoint(Color.FromName(colourName), new Point(X, Y));

                Assert.True(point1.Equals(point2));
            }

            [TestCase(1, 3, "Red")]
            [TestCase(2, 7, "Green")]
            public void TwoPointsWithDifferentParametersAreNotEqual(int X, int Y, string colourName)
            {
                var point1 = new ColouredPoint(Color.FromName(colourName), new Point(X, Y));
                var point2 = new ColouredPoint(Color.Turquoise, new Point(X + 1, Y + 1));

                Assert.False(point1.Equals(point2));
            }

            [TestCase]
            public void IsNotEqualToADifferentType()
            {
                var point1 = new ColouredPoint(Color.Turquoise, new Point(1, 1));

                Assert.False(point1.Equals(new object()));
            }

            [TestCase(1, 3, "Red")]
            [TestCase(2, 7, "Green")]
            public void TwoPointsWithTheSameParametersAreObjectEqual(int X, int Y, string colourName)
            {
                var point1 = new ColouredPoint(Color.FromName(colourName), new Point(X, Y));
                var point2 = new ColouredPoint(Color.FromName(colourName), new Point(X, Y));

                Assert.True(point1.Equals((Object)point2));
            }

            [TestCase]
            public void IsNotEqualToNull()
            {
                var point1 = new ColouredPoint(Color.Turquoise, new Point(1, 1));

                Assert.False(point1.Equals(null));
            }
        }

        public class GetHashCodeTests : ColouredPointTests
        {
            [TestCase(1, 3, "Red")]
            [TestCase(2, 7, "Green")]
            public void TwoPointsWithTheSameParametersHaveSameHashCode(int X, int Y, string colourName)
            {
                var point1 = new ColouredPoint(Color.FromName(colourName), new Point(X, Y));
                var point2 = new ColouredPoint(Color.FromName(colourName), new Point(X, Y));

                Assert.AreEqual(point1.GetHashCode(), point2.GetHashCode());
            }
        }
    }
}
