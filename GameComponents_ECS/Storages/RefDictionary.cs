using System.Runtime.CompilerServices;
namespace GameComponents.Storages;

public class Ref<T>
{
    private T? _value;
    
    public ref T? Value => ref _value;
    
    public static implicit operator T?(Ref<T> other) => other.Value;
    public static implicit operator Ref<T>(T other) => new Ref<T>(other);
    
    public Ref() => _value = default;
    public Ref(T initialValue) => _value = initialValue;
}

public class RefDictionary<TKey, TValue> : IEnumerable<Ref<TValue>> where TKey : notnull
{
    private readonly Dictionary<TKey, Ref<TValue>> _dictionary;
    public Ref<TValue>[] AsArray { get; protected set; }
    
    public Dictionary<TKey, Ref<TValue>> GetInternalDictionary() => _dictionary;
    
    public Ref<TValue> this[TKey key] => _dictionary[key];
    
    private void _updateArray() => AsArray = _dictionary.Values.ToArray();
    
    public RefDictionary(int initialSize = 0) 
    {
        _dictionary = new Dictionary<TKey, Ref<TValue>>(initialSize);
        AsArray = _dictionary.Values.ToArray();
    }
    
    public void Add(TKey key, TValue value) 
    {
        _dictionary.Add(key, value);
        _updateArray();
    }
    
    public bool TryAdd(TKey key, TValue value) 
    {
        bool isAdded = _dictionary.TryAdd(key, value);
        _updateArray();
        return isAdded;
    }
    
    public void Remove(TKey key) 
    {
        _dictionary.Remove(key);
        _updateArray();
    }
    
    public IEnumerator<Ref<TValue>> GetEnumerator() 
    {
        for (int i = AsArray.Length - 1; i >= 0; i--) 
        {
            yield return AsArray[i];
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}