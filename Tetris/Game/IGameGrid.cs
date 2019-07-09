using System;

namespace Tetris.Game
{
    public delegate void RowsRemoved(int count);

    public interface IGameGrid
    {
        event RowsRemoved OnRowsRemoved;
        event Action OnShapeLanded;

        bool MoveDown();
        bool RotateLeft();
        bool MoveRight();
        bool MoveLeft();
    }
}
