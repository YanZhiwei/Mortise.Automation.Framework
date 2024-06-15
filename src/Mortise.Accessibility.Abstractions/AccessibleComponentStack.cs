namespace Mortise.Accessibility.Abstractions;

public sealed class AccessibleComponentStack<T> : Stack<T>
{
    private readonly HashSet<int> _hashSet = new();
    public new void Push(T item)
    {
        var itemHashCode = item.GetHashCode();
        if (!_hashSet.Contains(itemHashCode))
        {
            base.Push(item);
            _hashSet.Add(itemHashCode);
        }
    }

    public new T Pop()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        var item = base.Pop();
        _hashSet.Remove(item.GetHashCode());
        return item;
    }

    public new T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        return base.Peek();
    }

    public new bool Contains(T item)
    {
        return _hashSet.Contains(item.GetHashCode());
    }
}