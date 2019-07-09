using System;
using System.Windows.Forms;
using Tetris.Game;
using Tetris.Game.Colors;
using Tetris.Game.Controller;
using Tetris.Game.Score;
using Tetris.Game.Shape;
using Tetris.Winforms;

namespace Tetris
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var shapeFactory = new TetrisShapeFactory(new Random());
            var colorFactory = new ColorFactory(new Random());
            var gameGrid = new GameGridManager(shapeFactory, colorFactory, 8, 20);
            var gameController = new GameController(gameGrid);
            var scoreManager = new ScoreManager(gameGrid);

            var form = new TetrisForm();
            form.GameGrid = gameGrid;
            form.GameController = gameController;
            form.ScoreManager = scoreManager;

            Timer ticky;
            ticky = new Timer();
            ticky.Interval = 500;
            ticky.Tick += (a, b) => gameGrid.Tick();
            ticky.Start();


            Application.Run(form);
        }
    }
}
