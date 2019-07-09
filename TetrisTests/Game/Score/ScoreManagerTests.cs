using Moq;
using NUnit.Framework;
using System;
using Tetris.Game;
using Tetris.Game.Score;

namespace TetrisTests.Game.Score
{
    [TestFixture]
    public class ScoreManagerTests
    {
        public class ConstructorTests : ScoreManagerTests
        {
            [TestCase]
            public void WhenGameGridIsNullAnExceptionIsRaised()
            {
                Assert.Throws(typeof(ArgumentException), () => new ScoreManager(null));
            }
        }

        public class UpdateScoreTests : ScoreManagerTests
        {
            [TestCase]
            public void WhenNoRowsAreUpdatedNoScoreChangeHappens()
            {
                var gameGridMock = new Mock<IGameGrid>();
                var scoreManager = new ScoreManager(gameGridMock.Object);

                gameGridMock.Raise(grid => grid.OnRowsRemoved += null, 0);

                Assert.AreEqual(scoreManager.Score, 0);
            }

            [TestCase]
            public void WhenMoreThanOneRowIsUpdatedScoreChangeHappens()
            {
                var gameGridMock = new Mock<IGameGrid>();
                var scoreManager = new ScoreManager(gameGridMock.Object);

                gameGridMock.Raise(grid => grid.OnRowsRemoved += null, 1);

                Assert.GreaterOrEqual(scoreManager.Score, 1);
            }

            [TestCase]
            public void WhenRowsAreRemovedScoreIsUpdated()
            {
                var gameGridMock = new Mock<IGameGrid>();
                var scoreManager = new ScoreManager(gameGridMock.Object);

                int score = 0;
                scoreManager.OnScoreUpdated += (s) => { score = s; };

                gameGridMock.Raise(grid => grid.OnRowsRemoved += null, 1);

                Assert.GreaterOrEqual(score, 1);
            }

            [TestCase]
            public void WhenShapeLandsScoreIsUpdated()
            {
                var gameGridMock = new Mock<IGameGrid>();
                var scoreManager = new ScoreManager(gameGridMock.Object);

                int score = 0;
                scoreManager.OnScoreUpdated += (s) => { score = s; };

                gameGridMock.Raise(grid => grid.OnShapeLanded += null);

                Assert.GreaterOrEqual(score, 1);
            }
        }
    }
}
