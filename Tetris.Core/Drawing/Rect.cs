using System.Drawing;

namespace Tetris.Core.Drawing
{
    public class Rect
    {
        public Rect(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; private set;}

        public int Height { get; private set; }

        public Point[] ToPoints(int x, int y)
        {
            var points = new Point[4];
            for (int i=0; i<4; i++)
            {
                points[i].X = x + Width * ((i == 1 || i == 2) ? 1 : 0);
                points[i].Y = y + Height * ((i == 2 || i == 3) ? 1 : 0);
            }
            return points;
        }
    }
}
