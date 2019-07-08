using System;
using System.Windows.Forms;
using Tetris.Game;
using Tetris.Game.Controller;
using Tetris.Renderer;
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

            var form = new TetrisForm();
            var gamePanel = form.GamePanel;
            var grid = new GameGrid(gamePanel.Width / 20, gamePanel.Height / 20);
            var renderer = new PanelRenderer(form.GamePanel, grid, 20, 20);

            var gameController = new GameController(grid);
            form.KeyDown += (sender, e) =>
                {
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
                            break;
                    }
                    gameController.KeyPressed(key);
                };


            Timer ticky;
            ticky = new Timer();
            ticky.Interval = 500;
            ticky.Tick += (a, b) => grid.Tick();
            ticky.Start();


            Application.Run(form);
        }
    }
}
