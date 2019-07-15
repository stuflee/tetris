using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tetris.Helper;

namespace Tetris.Core.Game.Grid
{
    public class GameGrid : IEditableGameGrid
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

        public void Clear()
        {
            _colouredPoints.Clear();
        }

        public bool TryAdd(IEnumerable<ColouredPoint> points)
        {
            if (!CanAdd(points))
                return false;

            _colouredPoints.AddRange(points);
            return true;
        }

        public bool CanAdd(IEnumerable<ColouredPoint> points)
        {
            if (!AreInsideGridBounds(points))
                return false;

            if (AreAlreadyPopulated(points))
                return false;

            return true;
        }

        private bool AreInsideGridBounds(IEnumerable<ColouredPoint> points)
        {
            return !points.Any(cp => !cp.Point.IsWithinBounds(Width - 1, Height - 1));
        }

        private bool AreAlreadyPopulated(IEnumerable<ColouredPoint> points)
        {
            return points.Any(cpO => _colouredPoints.Any(cpI => cpI.Point == cpO.Point));
        }
    }
}
