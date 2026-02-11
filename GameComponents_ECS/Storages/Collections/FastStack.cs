using GameComponents.Helpers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace GameComponents.Storages;

public struct FastStack<T> : IEnumerable<T>, IReadOnlyCollection<T> 
{
    internal T[] _buffer;
    internal byte _nextIndex = 0;
    
    public bool HasElements => _buffer.Length > 0;
    public ref readonly T this[int index] => ref _buffer[index];
    public readonly Span<T> AsSpan() => MemoryMarshal.CreateSpan(ref _buffer[0], CompactCount);
    public readonly Type GetUnderlyingType() => typeof(T);
    public readonly int Count => _buffer.Length;
    public readonly int CompactCount => _nextIndex;
    
    // constructors
    public FastStack() => _buffer = Array.Empty<T>();
    
    public FastStack(int initialCapacity) 
    {
        _buffer = new T[initialCapacity];
    }
    
    public static FastStack<T> Create(int initialCapacity) => new FastStack<T>(initialCapacity);
    public static FastStack<T> Create(T[] initialBuffer) => new FastStack<T>() 
    {
        _buffer = initialBuffer
    };
    
    public void Push(T comp) 
    {
        if (!HasElements) ArrayHelper.Resize(ref _buffer, 4);
        else if (_nextIndex > Count - 1) ArrayHelper.CopyAndResize(ref _buffer, Count * 2, Count);
        
        _buffer[_nextIndex++] = comp;
    }
    
    public T Pop()
    {
        if (_nextIndex < 1) throw new ArgumentOutOfRangeException("trying to pop an element from an untouched or empty stack is invalid.");
        
        int originalCue = _nextIndex;
        T returnableComp = _buffer[_nextIndex--];
        _buffer[originalCue] = default!;
        
        return returnableComp;
    }
    
    public bool TryPop(out T result)
    {
        if (_nextIndex < 1) 
        {
            result = default!;
            return false;
        }
        
        int originalCue = _nextIndex;
        result = _buffer[_nextIndex--];
        _buffer[originalCue] = default!;
        
        return true;
    }
    
    public void Compact()
    {
        ArrayHelper.CopyAndResize(ref _buffer, CompactCount, CompactCount);
    }
    
    public void ForEach(FastStackHelper<T>.Loop loop) 
    {
        FastStackHelper<T>.ForEach(ref this, loop);
    }
    
    public IEnumerator<T> GetEnumerator() => new FastStackEnumerator<T>(this);
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}