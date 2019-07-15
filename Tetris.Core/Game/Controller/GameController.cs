using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tetris.Core.Game.Grid;

namespace Tetris.Core.Game.Controller
{
    public delegate bool GameEventHandler();

    public class GameController : IGameController
    {
        private IGameGridShapeDecorator _grid;
        private Dictionary<Direction, GameEventHandler> _events;
        
        public GameController(IGameGridShapeDecorator grid)
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

        public bool KeyPressed(Direction direction)
        {
            GameEventHandler handler;
            if (!_events.TryGetValue(direction, out handler))
                return false;

            handler();
            return true;
        }

        public IDictionary<Direction, GameEventHandler> Events
        { get { return new ReadOnlyDictionary<Direction, GameEventHandler>(_events); } }
    }
}
