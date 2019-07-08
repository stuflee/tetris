using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class LeftL : ITetrisShape
    {
        private enum Rotation
        {
            Zero = 0,
            First = 1,
            Second = 2,
            Third = 3
        }

        private readonly Rotation _rotation;

        public LeftL(Color colour) : this(colour, Rotation.Zero)
        {
        }

        private LeftL(Color colour, Rotation rotation)
        {
            Color = colour;
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

        public ITetrisShape RotateLeft()
        {
            switch (_rotation)
            {
                case Rotation.Zero:
                    return new LeftL(Color, Rotation.First);
                case Rotation.First:
                    return new LeftL(Color, Rotation.Second);
                case Rotation.Second:
                    return new LeftL(Color, Rotation.Third);
                case Rotation.Third:
                    return new LeftL(Color, Rotation.Zero);
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public ITetrisShape RotateRight()
        {
            switch (_rotation)
            {
                case Rotation.Zero:
                    return new LeftL(Color, Rotation.Third);
                case Rotation.First:
                    return new LeftL(Color, Rotation.Zero);
                case Rotation.Second:
                    return new LeftL(Color, Rotation.First);
                case Rotation.Third:
                    return new LeftL(Color, Rotation.Second);
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public Color Color { get; }

        public Point[] Points { get; }

    }
}
