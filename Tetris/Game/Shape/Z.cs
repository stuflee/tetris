using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class Z : ITetrisShape
    {
        private enum Orientation
        {
            Vertical,
            Horizontal
        }

        private Orientation _orientation;

        public Z(Color colour) : this(colour, Orientation.Horizontal)
        {
        }

        private Z(Color colour, Orientation orientation)
        {
            Color = colour;
            _orientation = orientation;
            Points = GetPoints(_orientation);
        }

        //We have two phase rotation so we just reverse X and Y.
        private Point[] GetPoints(Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                return new Point[] {
                    new Point(3,3),
                    new Point(2,2),
                    new Point(2,3),
                    new Point(1,2)
                };
            }
            else if (orientation == Orientation.Vertical)
            {
                return new Point[] {
                    new Point(2,1),
                    new Point(1,2),
                    new Point(2,2),
                    new Point(1,3)
                };
            }
            throw new ArgumentException("Enum out of range.");
        }

        public ITetrisShape RotateLeft()
        {
            return Rotate();
        }

        public ITetrisShape RotateRight()
        {
            return Rotate();
        }

        private Z Rotate()
        {
            var orientation = _orientation == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical;
            return new Z(Color, orientation);
        }

        public Color Color { get; }

        public Point[] Points { get; }
    }
}
