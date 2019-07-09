using System.Windows.Forms;
using Tetris.Game.Score;

namespace Tetris.Winforms
{
    public class ScoreLabel : Label
    {
        private ScoreManager _scoreManager;

        public ScoreManager ScoreManager {
            get {return _scoreManager;}
            set
            {
                if (value == null)
                    return;

                if (_scoreManager != null)
                    _scoreManager.OnScoreUpdated -= UpdateScore;

                _scoreManager = value;
                UpdateScore(_scoreManager.Score);
                _scoreManager.OnScoreUpdated += UpdateScore;
            }
        }

        private void UpdateScore(int score)
        {
            this.Text = score.ToString();
        }
    }
}
