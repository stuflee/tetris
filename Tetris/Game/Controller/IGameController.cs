namespace Tetris.Game.Controller
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
        void KeyPressed(Direction direction);
    }
}
