using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class LeftL : ITetrisShape
    {
        public LeftL() : this(Rotation.Zero)
        {
        }

        private LeftL(Rotation rotation)
        {
            Rotation = rotation;
            Points = GetPoints(Rotation);
        }

        //We have two phase rotation so we just reverse X and Y.
        private Point[] GetPoints(Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.Zero:
                    return new Point[]
                    {
                        new Point(2,1),
                        new Point(3,1),
                        new Point(3,2),
                        new Point(3,3)
                    };
                case Rotation.First:
                    return new Point[]
                    {
                        new Point(1,2),
                        new Point(1,1),
                        new Point(2,1),
                        new Point(3,1)
                    };
                case Rotation.Second:
                    return new Point[]
                    {
                        new Point(2,3),
                        new Point(1,3),
                        new Point(1,2),
                        new Point(1,1)
                    };
                case Rotation.Third:
                    return new Point[]
                    {
                        new Point(3,1),
                        new Point(3,2),
                        new Point(2,2),
                        new Point(1,2)
                    };
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public ITetrisShape Rotate()
        {
            switch (Rotation)
            {
                case Rotation.Zero:
                    return new LeftL(Rotation.First);
                case Rotation.First:
                    return new LeftL(Rotation.Second);
                case Rotation.Second:
                    return new LeftL(Rotation.Third);
                case Rotation.Third:
                    return new LeftL(Rotation.Zero);
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public Point[] Points { get; }

        public Rotation Rotation { get; }

    }
}
