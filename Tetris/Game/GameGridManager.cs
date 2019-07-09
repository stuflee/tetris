using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Game.Shape;
using Tetris.Helper;

namespace Tetris.Game
{
    public class GameGridManager : IGameGridManager
    {
        public class ColouredPoint
        {
            public Color Color;
            public Point Point;
        }

        private class MovingShape
        {
            public ITetrisShape shape;
            public Color color;
            public Point location;
        }

        private List<ColouredPoint> _points = new List<ColouredPoint>();
        private MovingShape _movingShape;
        private IFactory<ITetrisShape> _shapeFactory;
        private IFactory<Color> _colorFactory;

        public GameGridManager(IFactory<ITetrisShape> shapeFactory, IFactory<Color> colorFactory, int width, int height)
        {
            _shapeFactory = shapeFactory ?? throw new ArgumentException("Shape factory was null");
            _colorFactory = colorFactory ?? throw new ArgumentException("Color factory was null"); ;

            if (width < 5)
                throw new ArgumentOutOfRangeException("Grids have a minimum size of 5x5  to handle shape positioning and rotation.");

            if (height < 5)
                throw new ArgumentOutOfRangeException("Grids have a minimum size of 5x5 to handle shape positioning and rotation.");

            Width = width;
            Height = height;
        }

        public int Width { get; }

        public int Height { get; }

        private int MaxX { get { return Width - 1; } }

        private int MaxY { get { return Height - 1; } }

        private MovingShape GetNextShape()
        {
            return new MovingShape()
            {
                shape = _shapeFactory.GetNext(),
                color = _colorFactory.GetNext(),
                location = new Point(MaxX / 2 - 2, -3)
            };
        }

        public IEnumerable<ColouredPoint> GetPoints()
        {
            foreach (var p in _points)
                yield return p;

            foreach (var p in _movingShape.shape.Points)
                yield return new ColouredPoint() { Color = _movingShape.color, Point = p.Move(_movingShape.location) };
        }

        public void Tick()
        {
            if (_movingShape == null)
                _movingShape = GetNextShape();

            if (!MoveDown())
            {
                _points.AddRange(_movingShape.shape.Points.Select(p => new ColouredPoint() { Color = _movingShape.color, Point = p.Move(_movingShape.location) } ));

                var rows = ClearFullRows();
                if (rows > 0)
                    OnRowsRemoved?.Invoke(rows);

                _movingShape = null;

                OnShapeLanded?.Invoke();
            }
            OnGridUpdated?.Invoke();
        }

        public int ClearFullRows()
        {
            int rowsCleared = 0;
            int i = MaxY;
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
            return rowsCleared;
        }

        public bool MoveLeft()
        {
            Point proposed = new Point(_movingShape.location.X - 1, _movingShape.location.Y);
            return MoveShapeIfPossible(proposed, _movingShape.shape);
        }

        public bool MoveRight()
        {
            Point proposed = new Point(_movingShape.location.X + 1, _movingShape.location.Y);
            return MoveShapeIfPossible(proposed, _movingShape.shape);
        }

        public bool MoveDown()
        {
            Point proposed = new Point(_movingShape.location.X, _movingShape.location.Y + 1);
            return MoveShapeIfPossible(proposed, _movingShape.shape);
        }

        public bool Rotate()
        {
            var shape = _movingShape.shape.Rotate();
            return MoveShapeIfPossible(_movingShape.location, shape);
        }

        public bool MoveShapeIfPossible(Point proposedLocation, ITetrisShape proposedShape)
        {
            if (ShapeCanMove(proposedLocation, proposedShape))
            {
                _movingShape.location = proposedLocation;
                _movingShape.shape = proposedShape;
                OnGridUpdated?.Invoke();
                return true;
            }

            if (ShapeIsOffTheScreen(proposedLocation, proposedShape))
                OnGameEnded?.Invoke();

            return false;
        }

        public bool ShapeCanMove(Point proposedLocation, ITetrisShape proposedShape)
        {
            int proposedX = proposedLocation.X;
            int proposedY = proposedLocation.Y;

            var cannotMove = Array.ConvertAll(proposedShape.Points, p => p.Move(proposedLocation))
                .Where(p => p.Y >= 0) //We only care about points already on the grid.
                .Any(pInner => !pInner.IsWithinBounds(MaxX, MaxY) || _points.Any(cp => cp.Point == pInner));

            return !cannotMove;
        }

        public bool ShapeIsOffTheScreen(Point proposedLocation, ITetrisShape proposedShape)
        {
            return proposedShape.Points.Any(p => p.IsWithinBounds(MaxX, MaxY));
        }

        public event Action OnGridUpdated;

        public event RowsRemoved OnRowsRemoved;

        public event Action OnShapeLanded;

        public event Action OnGameEnded;
    }
}
