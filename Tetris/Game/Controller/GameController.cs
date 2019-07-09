using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tetris.Game.Controller
{
    public delegate bool GameEventHandler();

    public class GameController : IGameController
    {
        private IGameGridManager _grid;
        private Dictionary<Direction, GameEventHandler> _events;
        
        public GameController(IGameGridManager grid)
        {
            _grid = grid ?? throw new ArgumentException("Grid cannot be null in creation of controller.");

            _events = new Dictionary<Direction, GameEventHandler>
            {
                { Direction.Down, _grid.MoveDown },
                { Direction.Up, _grid.Rotate },
                { Direction.Left, _grid.MoveLeft },
                { Direction.Right, _grid.MoveRight }
            };
        }

        public void KeyPressed(Direction direction)
        {
            GameEventHandler handler;
            if (_events.TryGetValue(direction, out handler))
                handler();
        }

        public IDictionary<Direction, GameEventHandler> Events
        { get { return new ReadOnlyDictionary<Direction, GameEventHandler>(_events); } }
    }
}
