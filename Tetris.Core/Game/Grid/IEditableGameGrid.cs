using System;
using System.Collections.Generic;

namespace Tetris.Core.Game.Grid
{
    public interface IEditableGameGrid : IGameGrid
    {
        int Count();

        bool CanAdd(IEnumerable<ColouredPoint> points);

        bool TryAdd(IEnumerable<ColouredPoint> points);

        void RemoveRange(IEnumerable<ColouredPoint> points);

        void Clear();
    }
}
