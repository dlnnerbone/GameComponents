using System.Collections;
using GameComponents.Helpers;
namespace GameComponents.Storages;

// under wip, do not use.

/// <summary>
/// a List class that doesn't aim to remove elements from a list (resizing arrays) and instead turns their values to their defaults.
/// </summary>
/// <typeparam name="T">Type</typeparam>
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
        else if (nextIndex > Length - 1) ArrayHelper.CopyAndResize(ref _buffer, Length * 2, _buffer.Length);
        
        _buffer[nextIndex++] = comp;
    }
    
    // unfinished you dipshits
    
    /// <summary>
    /// this method uses an index, retrieves the value, and turns it into default. note that this doesn't actually remove the element from the array,
    /// and instead turns the value into it's default value or null.
    /// </summary>
    /// <param name="index">the index to 'remove'</param>
    public void DefaultAt(int index) 
    {
        if (index < 0 || index >= Length) return;
        _buffer[index] = default!;
    }
    
    public void DefaultAt(int index, out T oldValue) 
    {
        if (index < 0 || index >= Length) 
        {
            oldValue = default!;
            return;
        }
        
        oldValue = _buffer[index];
        _buffer[index] = default!;
    }
    
    public void Replace(int index1, int index2) 
    {
        if (index1 < 0 || index1 >= Length || index2 < 0 || index2 >= Length || Length <= 2) return;
        var index1Value = _buffer[index1];
        var index2Value = _buffer[index2];
        
        _buffer[index1] = index2Value;
        _buffer[index2] = index1Value;
    }
    
    public void AddRange(IEnumerable<T> collection) 
    {
        for(int i = 0; i < collection.ToArray().Length; i++) 
        {
            Add(collection.ToArray()[i]);
        }
    }
    
    public IEnumerator<T> GetEnumerator() 
    {
        for (int i = 0; i <= nextIndex; i++) 
        {
            yield return _buffer[i];
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}