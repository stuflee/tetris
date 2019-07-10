using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Tetris.Helper;

namespace Tetris.Game.Grid
{
    public class GameGrid : IEnumerable<ColouredPoint>
    {
        private List<ColouredPoint> _colouredPoints = new List<ColouredPoint>();

        public GameGrid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }

        public int Height { get; }

        public int MaxX { get { return Width - 1; } }

        public int MaxY { get { return Height - 1; } }

        public IList<ColouredPoint> FilledPoints
        {
            get { return new ReadOnlyCollection<ColouredPoint>(_colouredPoints); }
        }

        public IEnumerator<ColouredPoint> GetEnumerator()
        {
            return _colouredPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _colouredPoints.GetEnumerator();
        }
        
        public void AddRange(IEnumerable<ColouredPoint> points)
        {
            if (!AreInsideGridBounds(points.Select(p => p.Point)))
                throw new ArgumentOutOfRangeException("One or more points are beyond the bounds of the grid.");

            if (AreAlreadyPopulated(points.Select(p => p.Point)))
                throw new ArgumentOutOfRangeException("One or more points are already filled.");

            _colouredPoints.AddRange(points);
        }

        public int ClearFullRows()
        {
            int rowsCleared = 0;
            int i = MaxY;
            while (i >= 0)
            {
                IList<ColouredPoint> matchingPoints = _colouredPoints.Where(p => p.Point.Y == i).ToList();
                if (matchingPoints.Count() == 0)
                    break;

                if (matchingPoints.Count() == Width)
                {
                    rowsCleared += 1;

                    foreach (var p in matchingPoints)
                        _colouredPoints.Remove(p);

                    for (int j = 0; j < _colouredPoints.Count; j++)
                    {
                        if (_colouredPoints[j].Point.Y < i)
                            _colouredPoints[j] = _colouredPoints[j].Move(new Point(0, 1));
                    }
                }
                else
                {
                    i -= 1;
                }
            }
            return rowsCleared;
        }

        public bool CanAddPoints(IEnumerable<Point> points)
        {
            if (!AreInsideGridBounds(points))
                return false;

            if (AreAlreadyPopulated(points))
                return false;

            return true;
        }

        public bool AreInsideGridBounds(IEnumerable<Point> points)
        {
            return !points.Any(point => !point.IsWithinBounds(MaxX, MaxY));
        }

        public bool AreAlreadyPopulated(IEnumerable<Point> points)
        {
            return points.Any(p => _colouredPoints.Any(cp => cp.Point == p));
        }
    }
}
