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

            }
        }
    }
}
