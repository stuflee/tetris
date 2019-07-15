using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris.Core.Game.Colors;
using Tetris.Core.Game.Controller;
using Tetris.Core.Game.Grid;
using Tetris.Core.Game.Score;
using Tetris.Core.Game.Shape;
using Tetris.Helper;
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
            var gameGrid = new GameGrid(8, 20);
            var previewGameGrid = new GameGrid(5, 5);
            var gameGridMgr = new GameGridShapeDecorator(gameGrid);
            var gameController = new GameController(gameGridMgr);
            var scoreManager = new ScoreManager();

            Action updatePreviewPane = () =>
            {
                previewGameGrid.Clear();
                var nextShape = shapeFactory.PeekNext();
                var nextColour = colorFactory.PeekNext();
                previewGameGrid.TryAdd(
                    Array.ConvertAll(nextShape.Points,
                    p => new ColouredPoint(nextColour, p.Move(new Point((previewGameGrid.Width-1) / 2, 3)))));
            };

            var form = new TetrisForm();
            form.PreviewGrid = previewGameGrid;
            form.GameGrid = gameGridMgr;
            form.GameController = gameController;
            form.ScoreManager = scoreManager;

            gameGridMgr.SetShape(shapeFactory.GetNext(), colorFactory.GetNext());
            updatePreviewPane();

            Timer ticky = new Timer()
            {
                Interval = 500,
            };
            ticky.Tick += (a, b) =>
            {
                if (!gameGridMgr.MoveDown())
                {
                    if (!gameGridMgr.CommitShape())
                    {
                        ticky.Stop();
                        return;
                    }
                    gameGridMgr.SetShape(shapeFactory.GetNext(), colorFactory.GetNext());
                    updatePreviewPane();
                    scoreManager.ProcessShapeLanded();
                }
                var rows = gameGridMgr.ClearFullRows();
                if (rows > 0) scoreManager.ProcessRowsRemoved(rows);

                form.Refresh();
            };
            ticky.Start();


            Application.Run(form);
        }
    }
}
