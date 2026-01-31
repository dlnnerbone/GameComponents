using GameComponents.Helpers;
using System.Runtime.CompilerServices;
namespace GameComponents.Storages;

public struct FastStack<T> : IEnumerable<T>, IReadOnlyCollection<T> 
{
    private T[] _buffer;
    private bool _componentIsReference = Helpers.RuntimeHelpers.IsReferenceOrContainsReferences<T>();
    private byte _nextIndex = 0;
    
    public bool HasElements => _buffer.Length > 0;
    public ref readonly T this[int index] => ref _buffer[index];
    public readonly Span<T> AsSpan() => _buffer.AsSpan();
    public readonly Type GetUnderlyingType() => typeof(T);
    public readonly int Count => _buffer.Length;
    
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
    
    // interface members
    
    public IEnumerator<T> GetEnumerator() 
    {
        foreach (T comp in _buffer) yield return comp;
    }
    
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}