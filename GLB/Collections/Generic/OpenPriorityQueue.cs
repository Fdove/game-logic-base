using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GLB.Collections.Generic {

    public class OpenPriorityQueue<TElement, TPriority>
        : IEnumerable<(TElement element, TPriority priority)>
    {
        private readonly IComparer<TPriority> _comparer;
        private readonly List<(TElement element, TPriority priority)> _orderedItems;

        public IComparer<TPriority> Comparer => _comparer;
        public int Count => _orderedItems.Count;
        public IReadOnlyList<(TElement element, TPriority priority)> OrderedItems => _orderedItems;

        public OpenPriorityQueue()
            : this(Comparer<TPriority>.Default) { }

        public OpenPriorityQueue(IComparer<TPriority>? comparer)
        {
            _comparer = comparer ?? Comparer<TPriority>.Default;
            _orderedItems = new();
        }

        public void Clear() => _orderedItems.Clear();

        public TElement Dequeue()
        {
            if (_orderedItems.Count == 0) throw new InvalidOperationException("Queue empty.");
            var first = _orderedItems[0];
            _orderedItems.RemoveAt(0);
            return first.Item1;
        }

        public void RemoveAt(int index)
        {
            _orderedItems.RemoveAt(index);
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            int index = _orderedItems.Count;
            while (index != 0 && _comparer.Compare(_orderedItems[index - 1].priority, priority) > 0)
                index--;
            _orderedItems.Insert(index, (element, priority));
        }

        public TElement Peek()
        {
            if (_orderedItems.Count == 0) throw new InvalidOperationException("Queue empty.");
            return _orderedItems[0].element;
        }

        public bool TryDequeue(
            [MaybeNullWhen(false)] out TElement element,
            [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_orderedItems.Count == 0)
            {
                element = default!;
                priority = default!;
                return false;
            }
            (element, priority) = _orderedItems[0];
            _orderedItems.RemoveAt(0);
            return true;
        }

        public bool TryPeek(
            [MaybeNullWhen(false)] out TElement element,
            [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_orderedItems.Count == 0)
            {
                element = default!;
                priority = default!;
                return false;
            }
            (element, priority) = _orderedItems[0];
            return true;
        }

        public IEnumerator<(TElement element, TPriority priority)> GetEnumerator()
            => _orderedItems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}