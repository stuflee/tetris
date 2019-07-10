using System.Drawing;
using System.Windows.Forms;
using Tetris.Game.Grid;
using Tetris.Renderer;

namespace Tetris.Winforms
{
    public class GamePanel : Panel
    {
        private GameGridManager _gameGrid;
        private int _xSize;
        private int _ySize;
        private Rect _baseRect;

        public GamePanel()
        {
            DoubleBuffered = true;
            Paint += Panel_Paint;
            _xSize = 20;
            _ySize = 20;
            _baseRect = new Rect(_xSize, _ySize);
        }

        public GameGridManager GameGrid
        {
            get
            {
                return _gameGrid;
            }

            set
            {
                if (value == null)
                    return;

                if (_gameGrid != null)
                    _gameGrid.OnGridUpdated -= Refresh;

                _gameGrid = value;
                _gameGrid.OnGridUpdated += Refresh;
                Width = _gameGrid.Width * _xSize + 1;
                Height = _gameGrid.Height * _ySize + 1;
            }
        }

        public void Draw(Graphics g, Point p, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                g.FillPolygon(brush, _baseRect.ToPoints(p.X * _xSize, p.Y * _ySize));
            }
        }

        public void DrawGrid(Graphics g, int width, int height)
        {
            using (var brush = new Pen(Color.LightGray))
            {
                for (int i = 0; i <= width; i++)
                    g.DrawLine(brush, new Point(i * _xSize, 0), new Point(i * _xSize, height * _ySize));

                for (int i = 0; i <= height; i++)
                    g.DrawLine(brush, new Point(0, i * _ySize), new Point(width * _xSize, i * _ySize));
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            if (_gameGrid == null)
                return;

            DrawGrid(e.Graphics, _gameGrid.Width, _gameGrid.Height);
            foreach (var p in _gameGrid.GetPoints())
                Draw(e.Graphics, p.Point, p.Color);
        }

    }
}
