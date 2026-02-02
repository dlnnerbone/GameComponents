using System.Collections;
namespace GameComponents.Storages;

internal struct FastStackEnumerator<T> : IEnumerator<T> 
{
    private T[] _collection;
    private int _nextIndex;
    private uint highestIndex;
    
    internal FastStackEnumerator(FastStack<T> stack) 
    {
        _nextIndex = -1;
        _collection = stack._buffer;
        highestIndex = (uint)stack.CompactCount;
    }
    
    public readonly T Current => _collection[_nextIndex];
    readonly object IEnumerator.Current => Current!;
    
    public bool MoveNext() => ++_nextIndex < highestIndex - 1;
    public void Dispose() => _collection = null!;
    public void Reset() => _nextIndex = -1;
}