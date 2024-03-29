﻿using System.Collections.Generic;
using System.Drawing;
using Tetris.Core.Game.Shape;

namespace Tetris.Core.Game.Grid
{
    public delegate void RowsRemoved(int count);

    public interface IGameGridShapeDecorator : IEnumerable<ColouredPoint>
    {
        int Width { get; }
        int Height { get; }

        bool MoveDown();
        bool Rotate();
        bool MoveRight();
        bool MoveLeft();

        void SetShape(ITetrisShape shape, Color color);
        bool CommitShape();
    }
}
