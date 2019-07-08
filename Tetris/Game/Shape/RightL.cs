﻿using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class RightL : ITetrisShape
    {
        private enum Rotation
        {
            Zero = 0,
            First = 1,
            Second = 2,
            Third = 3
        }

        private readonly Rotation _rotation;

        public RightL(Color colour) : this(colour, Rotation.Zero)
        {
        }

        private RightL(Color colour, Rotation rotation)
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

        public ITetrisShape RotateLeft()
        {
            switch (_rotation)
            {
                case Rotation.Zero:
                    return new RightL(Color, Rotation.First);
                case Rotation.First:
                    return new RightL(Color, Rotation.Second);
                case Rotation.Second:
                    return new RightL(Color, Rotation.Third);
                case Rotation.Third:
                    return new RightL(Color, Rotation.Zero);
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public ITetrisShape RotateRight()
        {
            switch (_rotation)
            {
                case Rotation.Zero:
                    return new RightL(Color, Rotation.Third);
                case Rotation.First:
                    return new RightL(Color, Rotation.Zero);
                case Rotation.Second:
                    return new RightL(Color, Rotation.First);
                case Rotation.Third:
                    return new RightL(Color, Rotation.Second);
                default:
                    throw new ArgumentException("Enum case not implemented.");
            }
        }

        public Color Color { get; }

        public Point[] Points { get; }
    }
}