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
    public Ref<TValue>[] ValueArray() => _dictionary.Values.ToArray();
    public TKey[] KeyArray => _dictionary.Keys.ToArray();
    public Dictionary<TKey, Ref<TValue>> GetInternalDictionary() => _dictionary;
    
    public Ref<TValue> this[TKey key] 
    {
        get => _dictionary[key];
        set => _dictionary[key] = value;
    }
    
    public RefDictionary(int initialSize = 0) 
    {
        _dictionary = new Dictionary<TKey, Ref<TValue>>(initialSize);
    }
    
    public void Add(TKey key, TValue value) => _dictionary.Add(key, value);
    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
    public bool Remove(TKey key) => _dictionary.Remove(key);
    public bool ContainsValue(Ref<TValue> value) => _dictionary.ContainsValue(value);
    
    
    public IEnumerator<Ref<TValue>> GetEnumerator()
    {
        foreach(var item in ValueArray()) 
        {
            yield return item;
        }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}