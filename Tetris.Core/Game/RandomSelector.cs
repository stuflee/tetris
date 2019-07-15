using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tetris.Core.Game
{
    public class RandomSelector<T> : IFactory<T>
    {
        private IList<T> _items;
        private Random _random;
        private int _nextItemIndex = -1;

        public RandomSelector(Random random, IList<T> items)
        {
            _random = random ?? throw new ArgumentNullException("random"); ;

            if (items == null)
                throw new ArgumentNullException("items");

            if (items.Count == 0)
                throw new ArgumentException("Expected more than one item to use in factory.");

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
