using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Game.Grid;
using Tetris.Game.Shape;
using Tetris.Helper;

namespace TetrisTests.Game.Grid
{
    [TestFixture]
    public class GameGridShapeDecoratorTests
    {
        private GameGridShapeDecorator gameGridDecorator;
        private Mock<GameGrid> gridMock;

        [SetUp]
        public virtual void Setup()
        {
            gridMock = new Mock<GameGrid>();
            //Setup good standard sizes.
            gridMock.Setup(m => m.Width).Returns(8);
            gridMock.Setup(m => m.Height).Returns(20);
            gameGridDecorator = new GameGridShapeDecorator(gridMock.Object);
        }

        //Since we are testing constructors we cannot use the setup in base.
        public class ConstructorTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WhenGridWidthIsLessThanFiveAnExceptionIsThrown()
            {
                gridMock.Setup(m => m.Width).Returns(3);
                gridMock.Setup(m => m.Height).Returns(10);
                Assert.Throws(typeof(ArgumentOutOfRangeException), () => new GameGridShapeDecorator(gridMock.Object));
            }

            [TestCase]
            public void WhenGridHeightIsLessThanFiveAnExceptionIsThrown()
            {
                gridMock.Setup(m => m.Width).Returns(10);
                gridMock.Setup(m => m.Height).Returns(3);
                Assert.Throws(typeof(ArgumentOutOfRangeException), () => new GameGridShapeDecorator(gridMock.Object));
            }

            [TestCase]
            public void WhenGridIsNullAnExceptionIsThrown()
            {
                Assert.Throws(typeof(ArgumentNullException), () => new GameGridShapeDecorator(null));
            }
        }

        public class WidthTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WidthReturnsGridWidth()
            {
                Assert.AreEqual(gridMock.Object.Width, gameGridDecorator.Width);
            }
        }

        public class HeightTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void HeightReturnsGridWidth()
            {
                Assert.AreEqual(gridMock.Object.Height, gameGridDecorator.Height);
            }
        }

        public class SetShapeTests : GameGridShapeDecoratorTests
        {
            public override void Setup()
            {
                base.Setup();
                List<ColouredPoint> points = new List<ColouredPoint>();
                gridMock.Setup(m => m.GetEnumerator()).Returns(points.GetEnumerator());
            }

            [TestCase]
            public void WhenAShapeIsSetItsPointsCanBeRetrieved()
            {
                var color = Color.Red;
                var pointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(pointList.ToArray());

                gameGridDecorator.SetShape(shapeMock.Object, color);

                var points = gameGridDecorator.ToList();
                Assert.AreEqual(pointList.Count, points.Count);
                Assert.AreEqual(pointList[0].Move(new Point((gridMock.Object.Width - 1) / 2, -1)), points[0].Point);
                Assert.AreEqual(color, points[0].Color);
            }
        }

        public class CommitShapeTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WhenShapeIsNullCanCommit()
            {
                var result = gameGridDecorator.CommitShape();
                Assert.True(result);
            }

            [TestCase]
            public void WhenCannotAddPointsToGridCannotCommit()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);

                gridMock.Setup(p => p.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(false);

                var canCommit = gameGridDecorator.CommitShape();
                Assert.False(canCommit);

            }

            [TestCase]
            public void WhenCommittedPointsAreAddedToGrid()
            {
                var color = Color.Red;
                var gridPointList = new List<ColouredPoint>() {
                    new ColouredPoint(color, new Point((gridMock.Object.Width - 1) / 2, -1))
                };

                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);

                gridMock.Setup(p => p.GetEnumerator()).Returns(gridPointList.GetEnumerator());
                gridMock.Setup(p => p.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(true);

                var canCommit = gameGridDecorator.CommitShape();
                Assert.True(canCommit);

                var outputPoints = gameGridDecorator.ToList();

                Assert.AreEqual(gridPointList.Count, outputPoints.Count);
                Assert.AreEqual(gridPointList[0], outputPoints[0]);
            }
        }

        public class ClearFullRowsTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WhenGridIsEmptyNoRowsAreCleared()
            {
                gridMock.Setup(p => p.GetEnumerator()).Returns(new List<ColouredPoint>().GetEnumerator());
                var rowsCleared = gameGridDecorator.ClearFullRows();
                Assert.AreEqual(0, rowsCleared);
            }

            [TestCase]
            public void WhenGridHasNoFullRowsNoneAreCleared()
            {
                var color = Color.Red;
                var points = new List<ColouredPoint>() {
                    new ColouredPoint(color, new Point(0,0)),
                    new ColouredPoint(color, new Point(1,1))
                };

                gridMock = new Mock<GameGrid>();
                //Setup good standard sizes.
                gridMock.Setup(m => m.Width).Returns(5);
                gridMock.Setup(m => m.Height).Returns(5);
                gridMock.Setup(m => m.GetEnumerator()).Returns(points.GetEnumerator());
                gameGridDecorator = new GameGridShapeDecorator(gridMock.Object);

                var result = gameGridDecorator.ClearFullRows();
                Assert.AreEqual(0, result);
            }

            [TestCase]
            public void WhenGridHasTwoFullRowsBothAreClearedAndOthersMoveDown()
            {
                var color = Color.Red;
                var points = new List<ColouredPoint>() {
                    new ColouredPoint(color, new Point(0,4)),
                    new ColouredPoint(color, new Point(1,4)),
                    new ColouredPoint(color, new Point(2,4)),
                    new ColouredPoint(color, new Point(3,4)),
                    new ColouredPoint(color, new Point(4,4)),
                    new ColouredPoint(color, new Point(0,3)),
                    new ColouredPoint(color, new Point(1,3)),
                    new ColouredPoint(color, new Point(2,3)),
                    new ColouredPoint(color, new Point(3,3)),
                    new ColouredPoint(color, new Point(4,3)),
                    new ColouredPoint(color, new Point(2,2)),
                    new ColouredPoint(color, new Point(3,1))
                };

                var pointsAfter = new List<ColouredPoint>() {
                    new ColouredPoint(color, new Point(2,4)),
                    new ColouredPoint(color, new Point(3,3))
                };

                //Due to complexity of this method use a real GameGrid.  Does increase chance of failure.
                var gameGrid = new GameGrid(5, 5);
                gameGrid.AddRange(points);

                gameGridDecorator = new GameGridShapeDecorator(gameGrid);
                var result = gameGridDecorator.ClearFullRows();
                Assert.AreEqual(2, result, string.Format("Expected two rows to be removed but was {0}", result));
                Assert.AreEqual(2, gameGrid.Count(), string.Format("Expected two points remaining but there were {0}", gameGrid.Count()));
                pointsAfter.ForEach(p => Assert.Contains(p, gameGrid.ToList()));
            }
        }

        public class MoveLeftTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WhenShapeIsNullReturnsTrue()
            {
                Assert.True(gameGridDecorator.MoveLeft());
            }

            [TestCase]
            public void WhenGridIsBlockedReturnsFalse()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);

                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(false);

                Assert.False(gameGridDecorator.MoveLeft());
            }

            [TestCase]
            public void WhenGridIsNotBlockedReturnsTrue()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);
                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(true);

                Assert.True(gameGridDecorator.MoveLeft());
            }
        }

        public class MoveRightTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WhenShapeIsNullReturnsTrue()
            {
                Assert.True(gameGridDecorator.MoveRight());
            }

            [TestCase]
            public void WhenGridIsBlockedReturnsFalse()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);

                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(false);

                Assert.False(gameGridDecorator.MoveRight());
            }

            [TestCase]
            public void WhenGridIsNotBlockedReturnsTrue()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);
                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(true);

                Assert.True(gameGridDecorator.MoveRight());
            }
        }

        public class MoveDownTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WhenShapeIsNullReturnsTrue()
            {
                Assert.True(gameGridDecorator.MoveDown());
            }

            [TestCase]
            public void WhenGridIsBlockedReturnsFalse()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);

                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(false);

                Assert.False(gameGridDecorator.MoveDown());
            }

            [TestCase]
            public void WhenGridIsNotBlockedReturnsTrue()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                gameGridDecorator.SetShape(shapeMock.Object, color);
                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(true);

                Assert.True(gameGridDecorator.MoveDown());
            }
        }

        public class RotateTests : GameGridShapeDecoratorTests
        {
            [TestCase]
            public void WhenShapeIsNullReturnsTrue()
            {
                Assert.True(gameGridDecorator.Rotate());
            }

            [TestCase]
            public void WhenGridIsBlockedReturnsFalse()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                shapeMock.Setup(p => p.Rotate()).Returns(shapeMock.Object);
                gameGridDecorator.SetShape(shapeMock.Object, color);

                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(false);

                Assert.False(gameGridDecorator.Rotate());
            }

            [TestCase]
            public void WhenGridIsNotBlockedReturnsTrue()
            {
                var color = Color.Red;
                var shapePointList = new List<Point>() { new Point(0, 0) };
                var shapeMock = new Mock<ITetrisShape>();
                shapeMock.Setup(p => p.Points).Returns(shapePointList.ToArray());
                shapeMock.Setup(p => p.Rotate()).Returns(shapeMock.Object);
                gameGridDecorator.SetShape(shapeMock.Object, color);
                gridMock.Setup(m => m.CanAddPoints(It.IsAny<IEnumerable<Point>>())).Returns(true);

                Assert.True(gameGridDecorator.Rotate());
            }
        }
    }
}
