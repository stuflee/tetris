using System.Collections.Generic;
using System.Drawing;
using Tetris.Game.Shape;

namespace Tetris.Game.Grid
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
