using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Game.Grid;

namespace TetrisTests.Game.Grid
{
    [TestFixture]
    public class GameGridTests
    {
        public class ConstructorTests : GameGridTests
        {
            [TestCase]
            public void WhenGridIsConstructedWidthAndHeightMatch()
            {
                int width = 100;
                int height = 120;

                var gameGrid = new StaticGameGrid(width, height);
                Assert.AreEqual(width, gameGrid.Width);
                Assert.AreEqual(height, gameGrid.Height);
            }

            [TestCase]
            public void WhenGridIsConstructedItsEmpty()
            {
                var gameGrid = new StaticGameGrid(100, 120);
                Assert.IsEmpty(gameGrid);
            }
        }

        public class GetEnumeratorTests : GameGridTests
        {
            [TestCase]
            public void WhenAPointIsAddedItIsEnumerated()
            {
                var gameGrid = new StaticGameGrid(100, 120);
                var point = new ColouredPoint(Color.Red, new Point(1, 1));
                gameGrid.AddRange( new[] { point });

                var enumerator = gameGrid.GetEnumerator();
                Assert.True(enumerator.MoveNext());
                Assert.AreEqual(point, enumerator.Current);
                Assert.False(enumerator.MoveNext());
            }
        }

        public class AddRangeTests : GameGridTests
        {
            [TestCase]
            public void PointsWithinBoundsCanBeAdded()
            {
                var points = new List<ColouredPoint>() {
                    new ColouredPoint(Color.Red, new Point(1, 1)),
                    new ColouredPoint(Color.Green, new Point(1, 2))
                };
                var gameGrid = new StaticGameGrid(10, 10);
                gameGrid.AddRange(points);

                points.ForEach(p => Assert.True(gameGrid.ToList().Contains(p)));
                Assert.True(gameGrid.ToList().Count == points.Count);
            }

            [TestCase]
            public void WhenAddingTheSamePointTwiceAnExceptionIsThrown()
            {
                var point = new ColouredPoint(Color.Red, new Point(1, 1));
                var gameGrid = new StaticGameGrid(10, 10);
                gameGrid.AddRange(new[] { point });

                Assert.Throws(typeof(ArgumentOutOfRangeException), () => gameGrid.AddRange(new[] { point }));
            }

            [TestCase(-1,1)]
            [TestCase(1, -1)]
            [TestCase(11, 1)]
            [TestCase(1, 11)]
            public void WhenAddingAnOutOfRangePointAnExceptionIsThrown(int X, int Y)
            {
                var point = new ColouredPoint(Color.Red, new Point(X, Y));
                var gameGrid = new StaticGameGrid(10, 10);
                Assert.Throws(typeof(ArgumentOutOfRangeException), () => gameGrid.AddRange(new[] { point }));
            }
        }

        public class RemoveRangeTests : GameGridTests
        {
            [TestCase]
            public void WhenRemoveRangeIsCalledWithNoItemsNoneAreRemoved()
            {
                var points = new List<ColouredPoint>() {
                    new ColouredPoint(Color.Red, new Point(0, 2)),
                    new ColouredPoint(Color.Green, new Point(1, 2)),
                    new ColouredPoint(Color.Blue, new Point(2, 2))
                };
                var gameGrid = new StaticGameGrid(3, 3);
                gameGrid.AddRange(points);
                gameGrid.RemoveRange(new ColouredPoint[] { });
                Assert.AreEqual(3, gameGrid.Count());
            }

            [TestCase]
            public void OnePointCanBeRemoved()
            {
                var points = new List<ColouredPoint>() {
                    new ColouredPoint(Color.Red, new Point(0, 2)),
                    new ColouredPoint(Color.Green, new Point(1, 2)),
                    new ColouredPoint(Color.Blue, new Point(2, 2))
                };
                var gameGrid = new StaticGameGrid(3, 3);
                gameGrid.AddRange(points);
                gameGrid.RemoveRange(new ColouredPoint[] { points [0]});
                Assert.AreEqual(2, gameGrid.Count());
                Assert.True(gameGrid.ToList().Contains(points[1]));
                Assert.True(gameGrid.ToList().Contains(points[2]));
            }

            [TestCase]
            public void ManyPointsCanBeremoved()
            {
                var points = new List<ColouredPoint>() {
                    new ColouredPoint(Color.Red, new Point(0, 1)),
                    new ColouredPoint(Color.Green, new Point(1, 1)),
                    new ColouredPoint(Color.Blue, new Point(2, 1)),
                    new ColouredPoint(Color.Red, new Point(0, 2)),
                    new ColouredPoint(Color.Green, new Point(1, 2)),
                    new ColouredPoint(Color.Blue, new Point(2, 2))
                };
                var gameGrid = new StaticGameGrid(3, 3);
                gameGrid.AddRange(points);
                var countBefore = gameGrid.Count();
                gameGrid.RemoveRange(points);
                var countAfter = gameGrid.Count();
                Assert.AreEqual(6, countBefore);
                Assert.AreEqual(0, countAfter);
            }
        }

        public class CanAddPointsTests : GameGridTests
        {
            [TestCase]
            public void WhenGridIsEmptyPointsCanBeAdded()
            {
                var point = new ColouredPoint(Color.Red, new Point(1, 1));
                var gameGrid = new StaticGameGrid(10, 10);

                Assert.True(gameGrid.CanAddPoints(new[] { point.Point }));
            }

            [TestCase]
            public void WhenADuplicatePointIsUsedItCannotBeAdded()
            {
                var point = new ColouredPoint(Color.Red, new Point(1, 1));
                var gameGrid = new StaticGameGrid(10, 10);
                gameGrid.AddRange(new[] { point });

                Assert.False(gameGrid.CanAddPoints(new[] { point.Point }));
            }

            [TestCase]
            public void WhenAPointIsOutOfRangeItCannotBeAdded()
            {
                var point = new ColouredPoint(Color.Red, new Point(30, 1));
                var gameGrid = new StaticGameGrid(10, 10);

                Assert.False(gameGrid.CanAddPoints(new[] { point.Point }));
            }
        }

    }
}
