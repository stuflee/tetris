﻿using System;

namespace Tetris.Game
{
    public delegate void RowsRemoved(int count);

    public interface IGameGridManager
    {
        event RowsRemoved OnRowsRemoved;
        event Action OnShapeLanded;

        bool MoveDown();
        bool Rotate();
        bool MoveRight();
        bool MoveLeft();
    }
}