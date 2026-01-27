using System.Collections.Immutable;

namespace GameComponents.Systems;

internal static class ComponentMeta { public static int NextID = 0; }

internal static class ComponentID<T>
{
    internal static Type TargetType = typeof(T);
    internal static readonly int ID = ComponentMeta.NextID++;
}

public static class IDDictionary 
{
    internal static readonly Dictionary<Type, byte> _componentIDDictionary = [];
    internal static byte GetValue(Type type) => _componentIDDictionary[type];
    
    public static ImmutableDictionary<Type, byte> GetInternalDictionary() => _componentIDDictionary.ToImmutableDictionary();
    
    public static void Add<T>() 
    {
        Type type = typeof(T);
        if (_componentIDDictionary.ContainsKey(type)) return;
        _componentIDDictionary.Add(type, (byte)ComponentID<T>.ID);
    }
}