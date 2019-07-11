using System.Collections.Generic;
using System.Drawing;

namespace Tetris.Game.Grid
{
    public interface IGameGrid : IEnumerable<ColouredPoint>
    {
        int Width { get; }

        int Height { get; }

        void RemoveRange(IEnumerable<ColouredPoint> points);

        void AddRange(IEnumerable<ColouredPoint> points);

        int Count();

        void Clear();

        bool CanAddPoints(IEnumerable<Point> points);

        bool AreInsideGridBounds(IEnumerable<Point> points);

        bool AreAlreadyPopulated(IEnumerable<Point> points);
    }
}
