using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class RightL : ITetrisShape
    {
        private readonly Rotation _rotation;

        public RightL() : this(Rotation.Zero)
        {
        }

        private RightL(Rotation rotation)
        {
            _rotation = rotation;
            Points = GetPoints(_rotation);
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
                        new Point(1,1),
                        new Point(1,2),
                        new Point(1,3)
                    };
                case Rotation.First:
                    return new Point[]
                    {
                        new Point(1,2),
                        new Point(1,3),
                        new Point(2,3),
                        new Point(3,3)
                    };
                case Rotation.Second:
                    return new Point[]
                    {
                        new Point(2,3),
                        new Point(3,3),
                        new Point(3,2),
                        new Point(3,1)
                    };
                case Rotation.Third:
                    return new Point[]
                    {
                        new Point(3,2),
                        new Point(3,1),
                        new Point(2,1),
                        new Point(1,1)
                    };
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public ITetrisShape Rotate()
        {
            switch (_rotation)
            {
                case Rotation.Zero:
                    return new RightL(Rotation.First);
                case Rotation.First:
                    return new RightL(Rotation.Second);
                case Rotation.Second:
                    return new RightL(Rotation.Third);
                case Rotation.Third:
                    return new RightL(Rotation.Zero);
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public Color Color { get; }

        public Point[] Points { get; }
    }
}
