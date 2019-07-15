﻿using System.Collections.Generic;

namespace Tetris.Game
{
    public interface IFactory<T>
    {
        T GetNext();

        T PeekNext();

        IList<T> GetItems { get; }
    }
}
