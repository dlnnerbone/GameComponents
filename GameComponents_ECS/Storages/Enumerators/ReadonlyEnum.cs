using System.Collections;
namespace GameComponents.Storages;

public struct ReadonlyEnumerator<T> : IEnumerator<T> 
{
    private T[] _collection;
    private int _nextIndex;
    
    public ReadonlyEnumerator(T[] _buffer) 
    {
        _nextIndex = -1;
        _collection = _buffer;
    }
    
    public readonly T Current => _collection[_nextIndex];
    readonly object IEnumerator.Current => Current!;
    
    public bool MoveNext() 
    {
        return ++_nextIndex < _collection.Length - 1;
    }
    
    public void Reset() => _nextIndex = -1;
    public void Dispose() => _collection = null!;
 }