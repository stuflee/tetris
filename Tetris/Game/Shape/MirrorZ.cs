using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class MirrorZ : ITetrisShape
    {
        public MirrorZ() : this(Orientation.Horizontal)
        {
        }

        private MirrorZ(Orientation orientation)
        {
            Orientation = orientation;
            Points = GetPoints(Orientation);
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

        public ITetrisShape Rotate()
        {
            var orientation = Orientation == Orientation.Vertical ? Orientation.Horizontal : Orientation.Vertical;
            return new MirrorZ(orientation);
        }

        public Point[] Points { get; }

        public Orientation Orientation { get; }
    }
}
