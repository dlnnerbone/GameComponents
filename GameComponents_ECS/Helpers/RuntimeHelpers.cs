using System.Reflection;

namespace GameComponents.Helpers;

public static class RuntimeHelpers 
{
    internal static readonly Dictionary<Type, bool> _cachedEntries = new();
    
    public static bool IsReference<T>() 
    {
        if (typeof(T).IsValueType) return false;
        return true;
    }
    
    public static bool ContainsReferencesOrIsReference<T>() 
    {
        Type type = typeof(T);
        
        if (_cachedEntries.ContainsKey(type)) return _cachedEntries[type];
        else if (IsReference<T>()) 
        {
            _cachedEntries.Add(type, true);
            return true;
        }
        
        foreach(ref readonly var field in type.GetFields().AsSpan()) 
        {
            if (!field.GetType().IsValueType) 
            {
                _cachedEntries.Add(type, true);
                return true;
            }
        }
        
        foreach(ref readonly var prop in type.GetProperties().AsSpan()) 
        {
            if (!prop.GetType().IsValueType) 
            {
                _cachedEntries.Add(type, true);
                return true;
            }
        }
        
        _cachedEntries.Add(type, false);
        return false;
    }
}