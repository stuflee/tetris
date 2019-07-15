using System.Windows.Forms;
using Tetris.Game.Controller;
using Tetris.Game.Grid;
using Tetris.Game.Score;

namespace Tetris.Winforms
{
    public partial class TetrisForm : Form
    {
        public IGameController _gameController;

        public TetrisForm()
        {
            InitializeComponent();
        }

        public IGameGrid GameGrid {
            get { return gamePanel.GameGrid; }
            set { gamePanel.GameGrid = value; }
        }

        public IGameGrid PreviewGrid
        {
            get { return previewPanel.GameGrid; }
            set { previewPanel.GameGrid = value; }
        }


        public ScoreManager ScoreManager {
            get { return lblScoreValue.ScoreManager; }
            set { lblScoreValue.ScoreManager = value; }
        }

        public IGameController GameController { get; set; }

        public void ProcessKeyDown(object sender, KeyEventArgs e)
        {
            if (GameController == null)
                return;

            Direction key;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    key = Direction.Left;
                    break;
                case Keys.Right:
                    key = Direction.Right;
                    break;
                case Keys.Up:
                    key = Direction.Up;
                    break;
                case Keys.Down:
                    key = Direction.Down;
                    break;
                default:
                    return;
            }
            if (GameController.KeyPressed(key))
                Refresh();

        }
    }
}

