using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Game.Shape;
using Tetris.Helper;

namespace Tetris.Game.Grid
{
    public class GameGridManager : IEnumerable<ColouredPoint>, IGameGridManager
    {
        private static Point DownOne = new Point(0, 1);
        private static Point LeftOne = new Point(-1, 0);
        private static Point RightOne = new Point(1, 0);

        private IGameGrid _gameGrid;
        private PositionedShape _movingShape;
        private IFactory<ITetrisShape> _shapeFactory;
        private IFactory<Color> _colorFactory;

        public GameGridManager(IGameGrid gameGrid, IFactory<ITetrisShape> shapeFactory, IFactory<Color> colorFactory)
        {
            _shapeFactory = shapeFactory ?? throw new ArgumentException("Shape factory was null");
            _colorFactory = colorFactory ?? throw new ArgumentException("Color factory was null");
            _gameGrid = gameGrid ?? throw new ArgumentException("Game grid was null");

            if (_gameGrid.Width < 5)
                throw new ArgumentOutOfRangeException("Grids have a minimum size of 5x5  to handle shape positioning and rotation.");

            if (_gameGrid.Height < 5)
                throw new ArgumentOutOfRangeException("Grids have a minimum size of 5x5 to handle shape positioning and rotation.");

            _movingShape = GetNextShape();
        }

        public int Width { get { return _gameGrid.Width; } }

        public int Height { get { return _gameGrid.Height; } }

        private PositionedShape GetNextShape()
        {
            var shape = _shapeFactory.GetNext();
            //We want the shape to appear with the first row on the screen.
            var startingY = -shape.Points.Select(p => p.Y).Max();
            var location = new Point((_gameGrid.Width - 1) / 2, startingY);
            var colour = _colorFactory.GetNext();

            return new PositionedShape(shape, colour, location);
        }

        public void Tick()
        {
            if (!MoveDown())
            {
                _gameGrid.AddRange(_movingShape.Shape.Points.Select(p => new ColouredPoint(_movingShape.Color, p.Move(_movingShape.Location))));

                _movingShape = GetNextShape();

                OnShapeLanded?.Invoke();

                var rows = ClearFullRows();
                if (rows > 0)
                    OnRowsRemoved?.Invoke(rows);
            }
            OnGridUpdated?.Invoke();
        }

        public int ClearFullRows()
        {
            var result = _gameGrid.GroupBy(p => p.Point.Y)
                                .OrderBy(grp => grp.Count())
                                .Select(grp => new { Y = grp.Key, Values = grp.ToList() })
                                .Where(val => val.Values.Count() == Width);

            int rowsRemoved = 0;
            result.ForEach(item => {
                    _gameGrid.RemoveRange(item.Values);
                    var toRemove = _gameGrid.Where(p => p.Point.Y < item.Y).ToList();
                    _gameGrid.RemoveRange(toRemove);
                    _gameGrid.AddRange(toRemove.ConvertAll(p => p.Move(new Point(0, 1))));
                    rowsRemoved += 1;
            });

            return rowsRemoved;
        }

        public bool MoveLeft()
        {
            return MoveShapeIfPossible(_movingShape.Location.Move(LeftOne), _movingShape.Shape);
        }

        public bool MoveRight()
        {
            return MoveShapeIfPossible(_movingShape.Location.Move(RightOne), _movingShape.Shape);
        }

        public bool MoveDown()
        {
            return MoveShapeIfPossible(_movingShape.Location.Move(DownOne), _movingShape.Shape);
        }

        public bool Rotate()
        {
            var shape = _movingShape.Shape.Rotate();
            return MoveShapeIfPossible(_movingShape.Location, shape);
        }

        public bool MoveShapeIfPossible(Point proposedLocation, ITetrisShape proposedShape)
        {
            var positionedPoints = Array.ConvertAll(proposedShape.Points, p => p.Move(proposedLocation));

            if (_gameGrid.CanAddPoints(positionedPoints.Where(p => p.Y >= 0)))
            {
                _movingShape = new PositionedShape(proposedShape, _movingShape.Color, proposedLocation);
                OnGridUpdated?.Invoke();
                return true;
            }

            if (!_gameGrid.AreInsideGridBounds(positionedPoints))
                OnGameEnded?.Invoke();

            return false;
        }

        public IEnumerator<ColouredPoint> GetEnumerator()
        {
            foreach (var p in _gameGrid)
                yield return p;

            if (_movingShape != null)
                foreach (var p in _movingShape.Shape.Points)
                    yield return new ColouredPoint(_movingShape.Color, p.Move(_movingShape.Location));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var p in _gameGrid)
                yield return p;

            if (_movingShape != null)
                foreach (var p in _movingShape.Shape.Points)
                    yield return new ColouredPoint(_movingShape.Color, p.Move(_movingShape.Location));
        }

        public event Action OnGridUpdated;

        public event RowsRemoved OnRowsRemoved;

        public event Action OnShapeLanded;

        public event Action OnGameEnded;
    }
}
