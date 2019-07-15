using System.Drawing;

namespace Tetris.Game.Shape
{
    public interface ITetrisShape
    {
        ITetrisShape Rotate();

        Point[] Points { get; }
    }
}
