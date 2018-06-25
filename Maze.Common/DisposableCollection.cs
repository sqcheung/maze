using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Common
{
    public class DisposableCollection<T> : IReadOnlyCollection<T>, IDisposable
        where T : IDisposable
    {
        readonly IList<T> _storage;

        public T this[int index] => _storage[index];

        public DisposableCollection(IEnumerable<T> items)
        {
            _storage = items.ToArray();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _storage.Count;
        
        public void Dispose()
        {
            foreach (T item in _storage)
            {
                item?.Dispose();
            }
        }
    }
}