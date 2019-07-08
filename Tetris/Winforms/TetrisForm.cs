using System;
using System.Windows.Forms;
using Tetris.Game;
using Tetris.Renderer;

namespace Tetris.Winforms
{
    public partial class TetrisForm : Form
    {

        public TetrisForm()
        {
            InitializeComponent();
        }

        public Panel GamePanel { get { return gamePanel; } }
    }
}

