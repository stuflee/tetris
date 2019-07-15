using System.Collections.Generic;

namespace Tetris.Game.Grid
{
    public interface IGameGrid : IEnumerable<ColouredPoint>
    {
        int Width { get; }

        int Height { get; }
    }
}
