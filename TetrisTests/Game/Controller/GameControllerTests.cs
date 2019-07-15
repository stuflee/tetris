using Moq;
using NUnit.Framework;
using System;
using Tetris.Core.Game.Controller;
using Tetris.Core.Game.Grid;

namespace Tetris.Core.Tests.Game.Controller
{
    [TestFixture]
    public class GameControllerTests
    {
        public class ConstructorTests : GameControllerTests
        {
            [TestCase]
            public void WhenGridIsNullAnExceptionIsThrown()
            {
                Assert.Throws(typeof(ArgumentException), () => new GameController(null));
            }

            [TestCase]
            public void EventsArePopulatedForAllDirecionValues()
            {
                var gridMock = new Mock<IGameGridShapeDecorator>();
                var gameController = new GameController(gridMock.Object);
                var events = gameController.Events;
                foreach (Direction e in typeof(Direction).GetEnumValues())
                {
                    GameEventHandler handler;
                    Assert.True(events.TryGetValue(e, out handler));
                    Assert.NotNull(handler);
                }
            }
        }

        public class KeyPressedTests : GameControllerTests
        {
            [TestCase]
            public void WhenDownIsPressedGridIsMovedDown()
            {
                var gridMock = new Mock<IGameGridShapeDecorator>();
                var controller = new GameController(gridMock.Object);
                controller.KeyPressed(Direction.Down);
                gridMock.Verify(grid => grid.MoveDown(), Times.Once);
                gridMock.VerifyNoOtherCalls();
            }

            [TestCase]
            public void WhenLeftIsPressedGridMoveLeftIsCalled()
            {
                var gridMock = new Mock<IGameGridShapeDecorator>();
                var controller = new GameController(gridMock.Object);
                controller.KeyPressed(Direction.Left);
                gridMock.Verify(grid => grid.MoveLeft(), Times.Once);
                gridMock.VerifyNoOtherCalls();
            }

            [TestCase]
            public void WhenRightIsPressedGridMoveRightIsCalled()
            {
                var gridMock = new Mock<IGameGridShapeDecorator>();
                var controller = new GameController(gridMock.Object);
                controller.KeyPressed(Direction.Right);
                gridMock.Verify(grid => grid.MoveRight(), Times.Once);
                gridMock.VerifyNoOtherCalls();
            }

            [TestCase]
            public void WhenUpIsPressedGridRotatedLeftIsCalled()
            {
                var gridMock = new Mock<IGameGridShapeDecorator>();
                var controller = new GameController(gridMock.Object);
                controller.KeyPressed(Direction.Up);
                gridMock.Verify(grid => grid.Rotate(), Times.Once);
                gridMock.VerifyNoOtherCalls();
            }

            [TestCase]
            public void WhenAnInvalidEnumIsPassedNoEventIsCalled()
            {
                var gridMock = new Mock<IGameGridShapeDecorator>();
                var controller = new GameController(gridMock.Object);
                controller.KeyPressed((Direction)100);
                gridMock.VerifyNoOtherCalls();
            }
        }
    }
}
