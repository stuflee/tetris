using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tetris.Game.Controller
{
    public delegate bool GameEventHandler();

    public class WinFormsGameController : IGameController, IDisposable
    {
        private GameGrid _grid;
        private Dictionary<Direction, GameEventHandler> _events;
        private Control _control;
        
        public WinFormsGameController(Control mainControl, GameGrid grid)
        {
            _grid = grid;
            _control = mainControl;
            _events = new Dictionary<Direction, GameEventHandler>();
            _events.Add(Direction.Down, _grid.MoveDown);
            _events.Add(Direction.Up, _grid.RotateLeft);
            _events.Add(Direction.Left, _grid.MoveLeft);
            _events.Add(Direction.Right, _grid.MoveRight);
            _control.KeyDown += ProcessKeyDown;
        }

        public void KeyPressed(Direction direction)
        {
            GameEventHandler handler;
            if (_events.TryGetValue(direction, out handler))
                handler();
        }

        public void ProcessKeyDown(object sender, KeyEventArgs e)
        {
            Direction key;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    key = Direction.Left;
                    break;
                case Keys.Right:
                    key = Direction.Right;
                    break;
                case Keys.Up:
                    key = Direction.Up;
                    break;
                case Keys.Down:
                    key = Direction.Down;
                    break;
                default:
                    return;
            }
            KeyPressed(key);
        }

        public void Dispose()
        {
            _control.KeyDown -= ProcessKeyDown;
        }
    }
}
