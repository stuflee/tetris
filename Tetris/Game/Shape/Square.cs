using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class Square : ITetrisShape
    {
        public Square(Color colour)
        {
            Color = colour;
            Points = new Point[]
                {
                    new Point(1,2),
                    new Point(1,3),
                    new Point(2,2),
                    new Point(2,3)
                };
        }

        public ITetrisShape RotateLeft()
        {
            return this;
        }

        public ITetrisShape RotateRight()
        {
            return this;
        }

        public Color Color { get; }

        public Point[] Points { get; }
    }
}
