using System;
using System.Drawing;
using Tetris.Helper;

namespace Tetris.Game.Grid
{
    public class ColouredPoint : IEquatable<ColouredPoint>
    {
        public ColouredPoint(Color c, Point p)
        {
            Color = c;
            Point = p;
        }

        public ColouredPoint Move(Point offset)
        {
            return new ColouredPoint(Color, Point.Move(offset));
        }

        public Color Color { get; }
        public Point Point { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is ColouredPoint))
                return false;

            ColouredPoint other = (ColouredPoint)obj;
            return Equals(other);
        }

        public bool Equals(ColouredPoint other)
        {
            if (other == null)
                return false;

            return other.Color == Color && other.Point == Point;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode() + 17 * Point.GetHashCode();
        }
    }
}
