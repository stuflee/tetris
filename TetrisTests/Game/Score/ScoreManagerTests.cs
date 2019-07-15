using NUnit.Framework;
using Tetris.Game.Score;

namespace TetrisTests.Game.Score
{
    [TestFixture]
    public class ScoreManagerTests
    {
        private ScoreManager scoreManager;

        [SetUp]
        public void Setup()
        {
            scoreManager = new ScoreManager();
        }

        public class UpdateScoreTests : ScoreManagerTests
        {
            [TestCase]
            public void WhenNoRowsAreUpdatedNoScoreChangeHappens()
            {
                scoreManager.ProcessRowsRemoved(0);
                Assert.AreEqual(scoreManager.Score, 0);
            }

            [TestCase]
            public void WhenRowsAreRemovedScoreIsUpdated()
            {
                scoreManager.ProcessRowsRemoved(1);
                Assert.GreaterOrEqual(scoreManager.Score, 1);
            }

            [TestCase]
            public void WhenShapeLandsScoreIsUpdated()
            {
                scoreManager.ProcessShapeLanded();
                Assert.GreaterOrEqual(scoreManager.Score, 1);
            }
        }
    }
}
