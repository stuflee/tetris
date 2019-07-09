using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class Tripod : ITetrisShape
    {
        private enum Rotation
        {
            Zero = 0,
            First = 1,
            Second = 2,
            Third = 3
        }

        private readonly Rotation _rotation;

        public Tripod() : this(Rotation.Zero)
        {
        }

        private Tripod(Rotation rotation)
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
                        new Point(1,3),
                        new Point(2,3),
                        new Point(3,3),
                        new Point(2,2)
                    };
                case Rotation.First:
                    return new Point[]
                    {
                        new Point(3,1),
                        new Point(3,2),
                        new Point(3,3),
                        new Point(2,2)
                    };
                case Rotation.Second:
                    return new Point[]
                    {
                        new Point(1,1),
                        new Point(2,1),
                        new Point(3,1),
                        new Point(2,2)
                    };
                case Rotation.Third:
                    return new Point[]
                    {
                        new Point(1,1),
                        new Point(1,2),
                        new Point(1,3),
                        new Point(2,2)
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
                    return new Tripod(Rotation.First);
                case Rotation.First:
                    return new Tripod(Rotation.Second);
                case Rotation.Second:
                    return new Tripod(Rotation.Third);
                case Rotation.Third:
                    return new Tripod(Rotation.Zero);
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public Point[] Points { get; }
    }
}
