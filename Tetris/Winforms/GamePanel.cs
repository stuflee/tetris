using System.Drawing;
using System.Windows.Forms;
using Tetris.Core.Drawing;
using Tetris.Core.Game.Grid;

namespace Tetris.Winforms
{
    public class GamePanel : Panel
    {
        private IGameGrid _gameGrid;
        private int _xSize;
        private int _ySize;
        private Rect _baseRect;

        public GamePanel()
        {
            DoubleBuffered = true;
            Paint += Panel_Paint;
            XSize = 20;
            YSize = 20;
        }

        public int XSize
        {
            get { return _xSize; }
            set
            {
                _xSize = value;
                _baseRect = new Rect(_xSize, _ySize);
            }
        }

        public int YSize
        {
            get { return _ySize; }
            set
            {
                _ySize = value;
                _baseRect = new Rect(_xSize, _ySize);
            }
        }

        public IGameGrid GameGrid
        {
            get
            {
                return _gameGrid;
            }

            set
            {
                if (value == null)
                    return;

                _gameGrid = value;
                Width = _gameGrid.Width * _xSize + 1;
                Height = _gameGrid.Height * _ySize + 1;
            }
        }

        public void Draw(Graphics g, Point p, Color color)
        {
            using (var brush = new SolidBrush(color))
                g.FillPolygon(brush, _baseRect.ToPoints(p.X * _xSize, p.Y * _ySize));

            using (var pen = new Pen(Color.LightGray))
                g.DrawPolygon(pen, _baseRect.ToPoints(p.X * _xSize, p.Y * _ySize));
        }

        public void DrawGrid(Graphics g, int width, int height)
        {
            using (var brush = new Pen(Color.DarkGray))
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

            using (var brush = new SolidBrush(Color.Black))
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, this.Width, this.Height));

                DrawGrid(e.Graphics, _gameGrid.Width, _gameGrid.Height);
            foreach (var p in _gameGrid)
                Draw(e.Graphics, p.Point, p.Color);
        }

    }
}
