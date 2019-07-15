namespace Tetris.Core.Game.Controller
{
    public enum Direction
    {
        Up, 
        Down,
        Left,
        Right
    }

    public delegate void KeyPress(Direction direction);

    public interface IGameController
    {
        bool KeyPressed(Direction direction);
    }
}
