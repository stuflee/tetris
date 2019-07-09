using System;

namespace Tetris.Game.Score
{
    public delegate void ScoreUpdated(int newScore);

    public class ScoreManager
    {
        private IGameGrid _gameGrid;

        public ScoreManager(IGameGrid gameGrid)
        {
            _gameGrid = gameGrid ?? throw new ArgumentException("GameGrid cannot be null");
            _gameGrid.OnRowsRemoved += ProcessRowsRemoved;
            _gameGrid.OnShapeLanded += ProcessShapeLanded;
        }

        public void ProcessShapeLanded()
        {
            UpdateScore(100);
        }

        public void ProcessRowsRemoved(int rowsRemoved)
        {
            if (rowsRemoved == 0)
                return;

            UpdateScore(500 * (2 ^ (rowsRemoved)));
        }

        public void UpdateScore(int addition)
        {
            Score += addition;

            OnScoreUpdated?.Invoke(Score);
        }

        public int Score { get; private set; }

        public event ScoreUpdated OnScoreUpdated;
    }
}
