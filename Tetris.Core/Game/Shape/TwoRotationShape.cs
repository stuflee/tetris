using System;
using System.Drawing;

namespace Tetris.Core.Game.Shape
{
    public class TwoRotationShape : ITetrisShape
    {
        public TwoRotationShape(Point[] points) : this(points, false)
        {
        }

        public TwoRotationShape(Point[] points, bool isRotated)        {
            Points = points;
            IsRotated = isRotated;
        }

        public ITetrisShape Rotate()
        {
            int direction = (IsRotated ? -1 : 1);
            return new TwoRotationShape(Array.ConvertAll(Points, p => p.Rotate(direction)), !IsRotated);
        }

        public Point[] Points { get; }

        public bool IsRotated { get; }
    }
}
