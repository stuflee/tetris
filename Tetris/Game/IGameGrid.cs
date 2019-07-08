namespace Tetris.Game
{
    public delegate void RowsRemoved(int count);

    public interface IGameGrid
    {
        event RowsRemoved OnRowsRemoved;
    }
}
