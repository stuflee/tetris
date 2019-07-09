namespace Tetris.Game.Score
{
    public delegate void ScoreUpdated(int newScore);

    public class ScoreManager
    {
        private IGameGrid _gameGrid;

        public ScoreManager(IGameGrid gameGrid)
        {
            _gameGrid = gameGrid;
            _gameGrid.OnRowsRemoved += ProcessRowsRemoved;
            _gameGrid.OnShapeLanded += ProcessShapeLanded;
        }

        public void ProcessShapeLanded()
        {
            Score += 100;
            OnScoreUpdated(Score);
        }

        public void ProcessRowsRemoved(int rowsRemoved)
        {
            Score += 500 * (2 ^ (rowsRemoved));
            OnScoreUpdated(Score);
        }

        public int Score { get; private set; }

        public event ScoreUpdated OnScoreUpdated;
    }
}
