using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Game.Shape;
using Tetris.Helper;

namespace Tetris.Game.Grid
{
    public class GameGridManager : IGameGridManager
    {
        private static Point DownOne = new Point(0, 1);
        private static Point LeftOne = new Point(-1, 0);
        private static Point RightOne = new Point(1, 0);

        private GameGrid _gameGrid;
        private PositionedShape _movingShape;
        private IFactory<ITetrisShape> _shapeFactory;
        private IFactory<Color> _colorFactory;

        public GameGridManager(GameGrid gameGrid, IFactory<ITetrisShape> shapeFactory, IFactory<Color> colorFactory)
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
            var location = new Point(_gameGrid.MaxX / 2 - 2, -3);
            var shape = _shapeFactory.GetNext();
            var colour = _colorFactory.GetNext();

            return new PositionedShape(shape, colour, location);
        }

        public IEnumerable<ColouredPoint> GetPoints()
        {
            foreach (var p in _gameGrid)
                yield return p;

            if (_movingShape != null)
                foreach (var p in _movingShape.Shape.Points)
                    yield return new ColouredPoint( _movingShape.Color, p.Move(_movingShape.Location));
        }

        public void Tick()
        {
            if (!MoveDown())
            {
                _gameGrid.AddRange(_movingShape.Shape.Points.Select(p => new ColouredPoint(_movingShape.Color, p.Move(_movingShape.Location))));

                _movingShape = GetNextShape();

                OnShapeLanded?.Invoke();

                var rows = _gameGrid.ClearFullRows();
                if (rows > 0)
                    OnRowsRemoved?.Invoke(rows);
            }
            OnGridUpdated?.Invoke();
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

        public event Action OnGridUpdated;

        public event RowsRemoved OnRowsRemoved;

        public event Action OnShapeLanded;

        public event Action OnGameEnded;
    }
}
