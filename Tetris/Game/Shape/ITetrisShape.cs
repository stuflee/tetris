using System.Drawing;

namespace Tetris.Game.Shape
{
    public interface ITetrisShape
    {
        ITetrisShape RotateLeft();

        ITetrisShape RotateRight();

        Color Color { get; }

        Point[] Points { get; }
    }
}
