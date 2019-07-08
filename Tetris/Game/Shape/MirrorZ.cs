using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class MirrorZ : ITetrisShape
    {
        private enum Orientation
        {
            Vertical,
            Horizontal
        }

        private Orientation _orientation;

        public MirrorZ(Color colour) : this(colour, Orientation.Horizontal)
        {
        }

        private MirrorZ(Color colour, Orientation orientation)
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
                    new Point(1,3),
                    new Point(2,2),
                    new Point(2,3),
                    new Point(3,2)
                };
            }
            else if (orientation == Orientation.Vertical)
            {
                return new Point[] {
                    new Point(1,1),
                    new Point(1,2),
                    new Point(2,2),
                    new Point(2,3)
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

        private MirrorZ Rotate()
        {
            var orientation = _orientation == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical;
            return new MirrorZ(Color, orientation);
        }

        public Color Color { get; }

        public Point[] Points { get; }
    }
}
