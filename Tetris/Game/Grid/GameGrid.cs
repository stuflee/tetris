using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tetris.Helper;

namespace Tetris.Game.Grid
{
    public class GameGrid : IGameGrid
    {
        private List<ColouredPoint> _colouredPoints = new List<ColouredPoint>();

        public GameGrid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }

        public int Height { get; }

        public IEnumerator<ColouredPoint> GetEnumerator()
        {
            return _colouredPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _colouredPoints.GetEnumerator();
        }
        
        public int Count()
        {
            return _colouredPoints.Count();
        }

        public void RemoveRange(IEnumerable<ColouredPoint> points)
        {
            points.ForEach(p => _colouredPoints.Remove(p));
        }

        public void AddRange(IEnumerable<ColouredPoint> points)
        {
            if (!AreInsideGridBounds(points.Select(p => p.Point)))
                throw new ArgumentOutOfRangeException("One or more points are beyond the bounds of the grid.");

            if (AreAlreadyPopulated(points.Select(p => p.Point)))
                throw new ArgumentOutOfRangeException("One or more points are already filled.");

            _colouredPoints.AddRange(points);
        }

        public void Clear()
        {
            _colouredPoints.Clear();
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
            return !points.Any(point => !point.IsWithinBounds(Width - 1, Height - 1));
        }

        public bool AreAlreadyPopulated(IEnumerable<Point> points)
        {
            return points.Any(p => _colouredPoints.Any(cp => cp.Point == p));
        }
    }
}
