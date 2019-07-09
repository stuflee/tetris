using NUnit.Framework;
using Tetris.Renderer;
using System.Drawing;

namespace TetrisTests.Winforms
{
    [TestFixture]
    public class RectTests
    {
        public class ToPointTests : RectTests
        {
            [Test]
            public void FourPointsAreReturned()
            {
                var rectangle = new Rect(1,1);
                var points = rectangle.ToPoints(0, 0);

                Assert.That(points.Length == 4, string.Format("Expected an array of length 4 but was {0}", points.Length));
            }

            [Test]
            public void WhenXYAreZeroPointsAreZeroBased()
            {
                var rectangle = new Rect(1, 1);
                var points = rectangle.ToPoints(0, 0);

                Assert.AreEqual(points[0], new Point(0, 0));
                Assert.AreEqual(points[1], new Point(1, 0));
                Assert.AreEqual(points[2], new Point(1, 1));
                Assert.AreEqual(points[3], new Point(0, 1));
            }

            [Test]
            public void WhenXIsNonZeroAnXOffsetIsApplied()
            {
                var rectangle = new Rect(1, 1);
                var points = rectangle.ToPoints(1, 0);

                Assert.AreEqual(points[0], new Point(1, 0));
                Assert.AreEqual(points[1], new Point(2, 0));
                Assert.AreEqual(points[2], new Point(2, 1));
                Assert.AreEqual(points[3], new Point(1, 1));
            }


            [Test]
            public void WhenYIsNonZeroAYOffsetIsApplied()
            {
                var rectangle = new Rect(1, 1);
                var points = rectangle.ToPoints(0, 1);

                Assert.AreEqual(points[0], new Point(0, 1));
                Assert.AreEqual(points[1], new Point(1, 1));
                Assert.AreEqual(points[2], new Point(1, 2));
                Assert.AreEqual(points[3], new Point(0, 2));
            }

            [Test]
            public void WhenXandYAreNonZeroAnOffsetIsApplied()
            {
                var rectangle = new Rect(1, 1);
                var points = rectangle.ToPoints(2, 1);

                Assert.AreEqual(points[0], new Point(2, 1));
                Assert.AreEqual(points[1], new Point(3, 1));
                Assert.AreEqual(points[2], new Point(3, 2));
                Assert.AreEqual(points[3], new Point(2, 2));
            }
        }

    }
}
