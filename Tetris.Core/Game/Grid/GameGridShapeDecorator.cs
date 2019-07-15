using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Core.Game.Shape;
using Tetris.Helper;

namespace Tetris.Core.Game.Grid
{
    public class GameGridShapeDecorator : IGameGridShapeDecorator, IGameGrid
    {
        private static Point DownOne = new Point(0, 1);
        private static Point LeftOne = new Point(-1, 0);
        private static Point RightOne = new Point(1, 0);
        private static Point NullOffset = new Point(0, 0);

        private IEditableGameGrid _gameGrid;
        private PositionedShape _movingShape;

        public GameGridShapeDecorator(IEditableGameGrid gameGrid)
        {
            _gameGrid = gameGrid ?? throw new ArgumentNullException("gameGrid");

            if (_gameGrid.Width < 5)
                throw new ArgumentOutOfRangeException("Grids have a minimum size of 5x5  to handle shape positioning and rotation.");

            if (_gameGrid.Height < 5)
                throw new ArgumentOutOfRangeException("Grids have a minimum size of 5x5 to handle shape positioning and rotation.");
        }

        public int Width { get { return _gameGrid.Width; } }

        public int Height { get { return _gameGrid.Height; } }

        public void SetShape(ITetrisShape shape, Color color)
        {
            var startingY = -shape.Points.Select(p => p.Y).Max() - 1;
            var location = new Point((_gameGrid.Width - 1) / 2, startingY);
            _movingShape = new PositionedShape(shape, color, location);
        }

        public bool CommitShape()
        {
            //If there is no shape it's trivially commitable.
            if (_movingShape == null)
                return true;

            var locatedPoints = _movingShape.Shape.Points.Select(p => new ColouredPoint(_movingShape.Color, p.Move(_movingShape.Location)));
            if (!_gameGrid.TryAdd(locatedPoints))
                return false;

            _movingShape = null;
            return true;
        }

        public int ClearFullRows()
        {
            var result = _gameGrid.GroupBy(p => p.Point.Y)
                                .OrderBy(grp => grp.Key)
                                .Select(grp => new { Y = grp.Key, Values = grp.ToList() })
                                .Where(val => val.Values.Count() == Width);

            int rowsRemoved = 0;
            result.ForEach(item => {
                    _gameGrid.RemoveRange(item.Values);
                    var toRemove = _gameGrid.Where(p => p.Point.Y < item.Y).ToList();
                    _gameGrid.RemoveRange(toRemove);
                    _gameGrid.TryAdd(toRemove.ConvertAll(p => p.Move(new Point(0, 1))));
                    rowsRemoved += 1;
            });
            return rowsRemoved;
        }

        public bool MoveLeft()
        {
            if (_movingShape == null)
                return true;

            return MoveShapeIfPossible(_movingShape.Move(LeftOne));
        }

        public bool MoveRight()
        {
            if (_movingShape == null)
                return true;

            return MoveShapeIfPossible(_movingShape.Move(RightOne));
        }

        public bool MoveDown()
        {
            if (_movingShape == null)
                return true;

            return MoveShapeIfPossible(_movingShape.Move(DownOne));
        }

        public bool Rotate()
        {
            if (_movingShape == null)
                return true;

            return MoveShapeIfPossible(_movingShape.Rotate());
        }

        private bool MoveShapeIfPossible(PositionedShape proposedShape)
        {
            var positionedPoints = Array.ConvertAll(proposedShape.Shape.Points, p => new ColouredPoint(proposedShape.Color, p.Move(proposedShape.Location)));
            if (!_gameGrid.CanAdd(positionedPoints.Where(p => p.Point.Y >= 0)))
                return false;

            _movingShape = proposedShape;
            return true;
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
            foreach (ColouredPoint p in this)
                yield return p;
        }
    }
}

