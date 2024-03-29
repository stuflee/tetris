﻿using System.Drawing;

namespace Tetris.Core.Game.Shape
{
    public class StaticShape : ITetrisShape
    {
        public StaticShape(Point[] points)
        {
            Points = points;
        }

        public ITetrisShape Rotate()
        {
            return this;
        }

        public Point[] Points { get; }
    }
}
