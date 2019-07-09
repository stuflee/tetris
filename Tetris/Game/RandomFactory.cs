using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tetris.Game
{
    public class RandomFactory<T> : IFactory<T>
    {
        private IList<T> _items;
        private Random _random;
        private int _nextItemIndex;

        public RandomFactory(Random random, IList<T> items)
        {
            _random = random;

            _items = new List<T>(items);
        }

        public T GetNext()
        {
            try
            {
                return PeekNext();
            }
            finally
            {
                _nextItemIndex = -1;
            }
        }

        public T PeekNext()
        {
            if (_nextItemIndex == -1)
                _nextItemIndex = _random.Next(_items.Count);

            return _items[_nextItemIndex];
        }

        public IList<T> GetItems
        {
            get { return new ReadOnlyCollection<T>(_items); }
        }
    }
}
