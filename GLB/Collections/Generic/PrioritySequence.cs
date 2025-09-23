using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GLB.Collections.Generic {

    public class PrioritySequence<TElement, TPriority>
        : IEnumerable<(TElement element, TPriority priority)>
    {
        private readonly IComparer<TPriority> _comparer;
        private readonly List<(TElement element, TPriority priority)> _orderedElements;

        public IComparer<TPriority> Comparer => _comparer;
        public int Count => _orderedElements.Count;
        public IReadOnlyList<(TElement element, TPriority priority)> OrderedElements => _orderedElements;
        public TElement this[int index]
        {
            get { return _orderedElements[index].Item1; }
        }

        public PrioritySequence()
            : this(Comparer<TPriority>.Default) { }

        public PrioritySequence(IComparer<TPriority>? comparer)
        {
            _comparer = comparer ?? Comparer<TPriority>.Default;
            _orderedElements = new();
        }

        public void Clear() => _orderedElements.Clear();

        public TElement Dequeue()
        {
            if (_orderedElements.Count == 0) throw new InvalidOperationException("Queue empty.");
            var first = _orderedElements[0];
            _orderedElements.RemoveAt(0);
            return first.Item1;
        }

        public void RemoveAt(int index)
        {
            _orderedElements.RemoveAt(index);
        }

        public void Enqueue(TElement element, TPriority priority)
        {
            int index = _orderedElements.Count;
            while (index != 0 && _comparer.Compare(_orderedElements[index - 1].priority, priority) > 0)
                index--;
            _orderedElements.Insert(index, (element, priority));
        }

        public TElement Peek()
        {
            if (_orderedElements.Count == 0) throw new InvalidOperationException("Queue empty.");
            return _orderedElements[0].element;
        }

        public bool TryDequeue(
            [MaybeNullWhen(false)] out TElement element,
            [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_orderedElements.Count == 0)
            {
                element = default!;
                priority = default!;
                return false;
            }
            (element, priority) = _orderedElements[0];
            _orderedElements.RemoveAt(0);
            return true;
        }

        public bool TryPeek(
            [MaybeNullWhen(false)] out TElement element,
            [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_orderedElements.Count == 0)
            {
                element = default!;
                priority = default!;
                return false;
            }
            (element, priority) = _orderedElements[0];
            return true;
        }

        public IEnumerator<(TElement element, TPriority priority)> GetEnumerator()
            => _orderedElements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}