using System;
using System.Drawing;

namespace Tetris.Core.Game.Shape
{
    public class FourRotationShape : ITetrisShape
    {
        public FourRotationShape(Point[] points)
        {
            Points = points ?? throw new ArgumentNullException("points");
        }

        public ITetrisShape Rotate()
        {
            return new FourRotationShape(Array.ConvertAll(Points, p => p.Rotate()));
        }

        public Point[] Points { get; }
    }
}
