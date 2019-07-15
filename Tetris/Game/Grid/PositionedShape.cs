using System;
using System.Drawing;
using Tetris.Game.Shape;
using Tetris.Helper;

namespace Tetris.Game.Grid
{
    public class PositionedShape
    {
        public PositionedShape(ITetrisShape shape, Color color, Point location)
        {
            Shape = shape ?? throw new ArgumentNullException("shape");
            Color = color;
            Location = location;
        }
    
        public ITetrisShape Shape { get; }
        public Color Color;
        public Point Location;

        public PositionedShape Move(Point offset)
        {
            return new PositionedShape(Shape, Color, Location.Move(offset));
        }

        public PositionedShape Rotate()
        {
            return new PositionedShape(Shape.Rotate(), Color, Location);
        }
    }
}
