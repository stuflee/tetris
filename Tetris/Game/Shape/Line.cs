using System.Drawing;

namespace Tetris.Game.Shape
{
    public class Line : ITetrisShape
    {
        public Line()
        {
            Points = new Point[]
                {
                    new Point(2,0),
                    new Point(2,1),
                    new Point(2,2),
                    new Point(2,3)
                };
        }

        private Line(Point[] points)
        {
            Points = points;
        }

        //We have two phase rotation so we just reverse X and Y.
        private Point[] RotatePoints()
        {
            return new Point[]
            {
                new Point(Points[0].Y, Points[0].X),
                new Point(Points[1].Y, Points[1].X),
                new Point(Points[2].Y, Points[2].X),
                new Point(Points[3].Y, Points[3].X)
            };
        }

        public ITetrisShape Rotate()
        {
            return new Line(RotatePoints());
        }

        public Point[] Points { get; }
    }
}
