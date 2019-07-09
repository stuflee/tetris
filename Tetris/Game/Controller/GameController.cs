using System;
using System.Collections.Generic;

namespace Tetris.Game.Controller
{
    public delegate bool GameEventHandler();

    public class GameController : IGameController
    {
        private GameGrid _grid;
        private Dictionary<Direction, GameEventHandler> _events;
        
        public GameController(GameGrid grid)
        {
            _grid = grid;
            _events = new Dictionary<Direction, GameEventHandler>();
            _events.Add(Direction.Down, _grid.MoveDown);
            _events.Add(Direction.Up, _grid.RotateLeft);
            _events.Add(Direction.Left, _grid.MoveLeft);
            _events.Add(Direction.Right, _grid.MoveRight);
        }

        public void KeyPressed(Direction direction)
        {
            GameEventHandler handler;
            if (_events.TryGetValue(direction, out handler))
                handler();
        }
    }
}
