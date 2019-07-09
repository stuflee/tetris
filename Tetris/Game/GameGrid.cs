using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Game.Shape;

namespace Tetris.Game
{
    public class GameGrid : IGameGrid
    {
        public class ColouredPoint
        {
            public Color Color;
            public Point Point;
        }

        private class MovingShape
        {
            public ITetrisShape shape;
            public Point location;
        }

        private List<ColouredPoint> _points;
        private MovingShape _movingShape;
        private List<Func<Color, ITetrisShape>> _shapeFactory;
        private List<Color> _colours;
        private int _maxX;
        private int _maxY;
        private Random r = new Random();


        public GameGrid(int width, int height)
        {
            Width = width;
            Height = height;
            _maxX = width - 1;
            _maxY = height - 1;
            _points = new List<ColouredPoint>();
            _shapeFactory = new List<Func<Color, ITetrisShape>> {
                c => new LeftL(c),
                c => new RightL(c),
                c => new Line(c),
                c => new Square(c),
                c => new Tripod(c),
                c => new Z(c),
                c => new MirrorZ(c)
            };

            _colours = new List<Color> { 
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Purple,
                Color.Orange,
                Color.Turquoise
            };

            _movingShape = GetNextShape();
        }

        public GameGrid()
        {
        }

        public int Width { get; }

        public int Height { get; }

        private MovingShape GetNextShape()
        {
            return new MovingShape()
            {
                shape = _shapeFactory[r.Next(_shapeFactory.Count)](GetNextColor()),
                location = new Point(_maxX / 2 - 2, -3)
            };
        }

        private Color GetNextColor()
        {
            return _colours[r.Next(_colours.Count)];
        }

        public IEnumerable<ColouredPoint> GetPoints()
        {
            foreach (var p in _points)
                yield return p;

            foreach (var p in _movingShape.shape.Points)
                yield return new ColouredPoint() { Color = _movingShape.shape.Color, Point = new Point(p.X + _movingShape.location.X, p.Y + _movingShape.location.Y) };
        }

        public void Tick()
        {
            if (!MoveDown())
            {
                int x = _movingShape.location.X;
                int y = _movingShape.location.Y;
                _points.AddRange(_movingShape.shape.Points.Select(p => new ColouredPoint() { Color = _movingShape.shape.Color, Point = new Point(p.X + x, p.Y + y) } ));

                ClearFullRows();

                _movingShape = GetNextShape();

                OnShapeLanded?.Invoke();
            }
            Update?.Invoke();
        }

        public void ClearFullRows()
        {
            int rowsCleared = 0;
            int i = _maxY;
            while (i >= 0)
            {
                IList<ColouredPoint> matchingPoints = _points.Where(p => p.Point.Y == i).ToList();
                if (matchingPoints.Count() == 0)
                    break;

                if (matchingPoints.Count() == Width)
                {
                    rowsCleared += 1;

                    foreach (var p in matchingPoints)
                        _points.Remove(p);

                    for (int j = 0; j < _points.Count; j++)
                    {
                        if (_points[j].Point.Y < i)
                            _points[j].Point.Y = _points[j].Point.Y + 1;
                    }
                }
                else
                {
                    i -= 1;
                }
            }

            if (rowsCleared > 0)
                OnRowsRemoved?.Invoke(rowsCleared);
        }

        public bool MoveLeft()
        {
            return MoveShapeIfPossible(_movingShape.location.X - 1, _movingShape.location.Y, _movingShape.shape);

        }

        public bool MoveRight()
        {
            return MoveShapeIfPossible(_movingShape.location.X + 1, _movingShape.location.Y, _movingShape.shape);
        }

        public bool MoveDown()
        {
            return MoveShapeIfPossible(_movingShape.location.X, _movingShape.location.Y + 1, _movingShape.shape);
        }

        public bool RotateLeft()
        {
            var shape = _movingShape.shape.RotateLeft();
            return MoveShapeIfPossible(_movingShape.location.X, _movingShape.location.Y, shape);
        }

        public bool MoveShapeIfPossible(int proposedX, int proposedY, ITetrisShape proposedShape)
        {
            if (ShapeCanMove(proposedX, proposedY, proposedShape))
            {
                _movingShape.location.X = proposedX;
                _movingShape.location.Y = proposedY;
                _movingShape.shape = proposedShape;
                Update?.Invoke();
                return true;
            }

            if (ShapeIsOffTheScreen(proposedX, proposedY, proposedShape))
                OnGameEnded?.Invoke();

            return false;
        }

        public bool ShapeCanMove(int proposedX, int proposedY, ITetrisShape proposedShape)
        {
            var cannotMove = proposedShape.Points.Any(pInner =>
                (pInner.X + proposedX < 0 || pInner.X + proposedX == _maxX + 1) ||
                (pInner.Y + proposedY == _maxY + 1) ||
                _points.Any(p => (p.Point.X == (pInner.X + proposedX) && p.Point.Y == (pInner.Y + proposedY))));

            return !cannotMove;
        }

        public bool ShapeIsOffTheScreen(int proposedX, int proposedY, ITetrisShape proposedShape)
        {
            var isOffTheScreen = proposedShape.Points.Any(pInner =>
                pInner.X + proposedX < 0 ||
                pInner.Y + proposedY < 0);

            return isOffTheScreen;
        }
        
        public event Action Update;

        public event RowsRemoved OnRowsRemoved;

        public event Action OnShapeLanded;

        public event Action OnGameEnded;
    }
}
