using System.Collections;
using GameComponents.Helpers;
namespace GameComponents.Storages;

// under wip, do not use.

public class FastList<T> : IEnumerable<T> 
{
    private int nextIndex = 0;
    private T[] _buffer;
    
    public T[] Buffer => _buffer;
    public int Length => _buffer.Length;
    
    public ref T this[int index] => ref _buffer[index];
    public ref T GetUpperElement() => ref _buffer[nextIndex];
    public bool HasElements => Buffer.Length >= 1;
    
    public Span<T> AsSpan() => _buffer.AsSpan();
    // constructor
    
    public FastList(int initialCapacity = 0) 
    {
        _buffer = new T[initialCapacity];
        nextIndex = 0;
    }
    
    public FastList(T[] initialBuffer) 
    {
        _buffer = initialBuffer;
        nextIndex = 0;
    }
    
    // static constructors
    
    public static FastList<T> Create(int initialCapacity) => new FastList<T>(initialCapacity);
    public static FastList<T> Create(T[] initialBuffer) => new FastList<T>(initialBuffer);
    
    // methods for adding, removing, and peeking, etc.
    
    public void Add(T comp) 
    {
        if (Length <= 0) ArrayHelper.Resize(ref _buffer, 1);
        else if (nextIndex > Length - 1) ArrayHelper.CopyAndResize(ref _buffer, Length * 2);
        
        _buffer[nextIndex++] = comp;
    }
    
    public void RemoveAt(int index) 
    {
        if (index < 0 || index >= Length) return;
        
    }
    
    public IEnumerator<T> GetEnumerator() 
    {
        foreach(var obj in Buffer) 
        {
            yield return obj;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}