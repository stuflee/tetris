using System.Drawing;

namespace Tetris.Helper
{
    public static class PointsHelper
    {
        public static bool IsWithinBounds(this Point p, int maxX, int maxY)
        {
            return p.X >= 0 && p.X <= maxX && p.Y >= 0 && p.Y <= maxY;
        }

        public static Point Move(this Point p, Point offset)
        {
            return new Point(p.X + offset.X, p.Y + offset.Y);
        }
    }
}
