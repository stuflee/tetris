namespace Tetris.Game.Score
{
    public delegate void ScoreUpdated(int newScore);

    public class ScoreManager
    {
        private IGameGrid _gameGrid;

        public ScoreManager(IGameGrid gameGrid)
        {
            _gameGrid = gameGrid;
            _gameGrid.OnRowsRemoved += UpdateScore;
        }

        public void UpdateScore(int rowsRemoved)
        {
            Score += 500 * (2 ^ (rowsRemoved - 1));
            OnScoreUpdated(Score);
        }

        public int Score { get; private set; }

        public event ScoreUpdated OnScoreUpdated;
    }
}
