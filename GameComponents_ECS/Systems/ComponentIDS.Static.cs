using System.Collections.Immutable;

namespace GameComponents.Systems;

internal static class ComponentMetadata 
{
    public static int Index = 0;
}

public static class ComponentID<T> 
{
    public static readonly int ID = ComponentDictionary.GetID<T>();
}

public static class ComponentDictionary 
{
    internal static readonly Dictionary<Type, int> _componentDictionary = [];
    
    public static int GetValue(Type type) => _componentDictionary[type];
    public static ImmutableDictionary<Type, int> GetInternalDictionary() => _componentDictionary.ToImmutableDictionary();
    
    public static void Add(Type type) 
    {
        if (_componentDictionary.ContainsKey(type)) return;
        _componentDictionary.Add(type, ComponentMetadata.Index++);
    }
    public static void Add<T>() => Add(typeof(T));
    
    public static int GetID(Type type) 
    {
        if (!_componentDictionary.ContainsKey(type)) Add(type);
        
        return _componentDictionary[type];
    }
    
    public static int GetID<T>() => GetID(typeof(T));
    
    public static void AddRange(IEnumerable<Type> typesToAdd) 
    {
        var toArr = typesToAdd.ToArray();
        
        for(int i = 0; i < toArr.Length; i++) 
        {
            Add(toArr[i]);
        }
    }
    
    public static int GetHighestID() => ComponentMetadata.Index;
    
}