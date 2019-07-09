using System;
using System.Drawing;

namespace Tetris.Game.Shape
{
    public class Square : ITetrisShape
    {
        public Square()
        {
            Points = new Point[]
                {
                    new Point(1,2),
                    new Point(1,3),
                    new Point(2,2),
                    new Point(2,3)
                };
        }

        public ITetrisShape Rotate()
        {
            return this;
        }

        public Point[] Points { get; }
    }
}
