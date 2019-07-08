using System.Drawing;

namespace Tetris.Renderer
{
    public interface IRenderer
    {
        void Draw(Graphics g, Point p, Color color);
        void DrawGrid(Graphics g, int width, int height);
    }
}
