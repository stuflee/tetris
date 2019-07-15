using System;
using Tetris.Game.Grid;

namespace Tetris.Game.Score
{
    public delegate void ScoreUpdated(int newScore);

    public class ScoreManager
    {
        public ScoreManager()
        {
        }

        public void ProcessShapeLanded()
        {
            UpdateScore(100);
        }

        public void ProcessRowsRemoved(int rowsRemoved)
        {
            if (rowsRemoved == 0)
                return;
            
            UpdateScore(500 * (int)Math.Round(Math.Pow(2, rowsRemoved)));
        }

        private void UpdateScore(int addition)
        {
            Score += addition;

            OnScoreUpdated?.Invoke(Score);
        }

        public int Score { get; private set; }

        public event ScoreUpdated OnScoreUpdated;
    }
}
