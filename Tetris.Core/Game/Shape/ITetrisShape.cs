using System.Drawing;

namespace Tetris.Core.Game.Shape
{
    public interface ITetrisShape
    {
        ITetrisShape Rotate();

        Point[] Points { get; }
    }
}
