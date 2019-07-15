using System.Drawing;
using System;

namespace Tetris.Core.Game.Shape
{
    public static class PointHelper
    {
        public static Point Rotate(this Point p, int numberOfQuadrants = 1)
        {
            double sin = Math.Sin(numberOfQuadrants * Math.PI / 2);
            double cos = Math.Cos(numberOfQuadrants * Math.PI / 2);

            return new Point((int)Math.Round(p.X * cos - p.Y * sin),
                             (int)Math.Round(p.X * sin + p.Y * cos));
        }
    }
}
