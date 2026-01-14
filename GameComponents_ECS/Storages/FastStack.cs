using System.Collections;
namespace GameComponents.Storages;

// under wip, do not use.

public struct FastList<T> : IEnumerable<T> 
{
    private int nextIndex = 0;
    
    public T[] Buffer { get; private set; }
    public int Length => Buffer.Length;
    
    public ref T Top => ref Buffer[Buffer.Length - 1];
    public bool HasElements => Buffer.Length <= 0;
    
    public readonly ref T this[int index] => ref Buffer[index];
    public int GetValidIndex() => nextIndex;
    
    public FastList(int initialBufferSize) 
    {
        nextIndex = 0;
        Buffer = new T[initialBufferSize];
    }
    // static
    public static FastList<T> Create(int initialBufferSize) => new FastList<T>(initialBufferSize);
    
    public static FastList<T> Create(T[] buffer) => new FastList<T>() 
    {
        Buffer = buffer,
        nextIndex = 0
    };
    
    // Add, Removal, Etc.
    
    
    // Attaining the Buffer as span.
    
    public readonly Span<T> AsSpan() => Buffer.AsSpan();
    
    // the interface IEnumerable
    
    public IEnumerator<T> GetEnumerator() 
    {
        foreach(var obj in Buffer) 
        {
            yield return obj;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}