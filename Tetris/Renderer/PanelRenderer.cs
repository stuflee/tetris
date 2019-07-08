using System.Drawing;
using System.Windows.Forms;
using Tetris.Game;

namespace Tetris.Renderer
{
    public class PanelRenderer : IRenderer
    {
        private Panel _panel;
        private int _xSize;
        private int _ySize;
        private Rect _baseRect;
        private GameGrid _grid;

        public PanelRenderer(Panel panel, GameGrid grid, int xSize, int ySize)
        {
            _panel = panel;
            _grid = grid;
            _xSize = xSize;
            _ySize = ySize;
            _baseRect = new Rect(_xSize, ySize);
            _panel.Paint += Panel_Paint;
            _grid.Update += () => _panel.Refresh();
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
            DrawGrid(e.Graphics, _grid.Width, _grid.Height);
            foreach (var p in _grid.GetPoints())
                Draw(e.Graphics, p.Point, p.Color);
        }
    }
}
