namespace GameComponents.Systems;

internal static class ComponentMeta { public static int nextIndex = 0; }

internal static class ComponentID<T> 
{
    public static readonly int ID = ComponentMeta.nextIndex++;
}

public static class ComponentIDDictionary 
{
    internal static readonly Dictionary<Type, int> ComponentDict = new Dictionary<Type, int>();
    
    public static void Add<T>() 
    {
        if (ComponentDict.ContainsKey(typeof(T))) return;
        ComponentDict.Add(typeof(T), ComponentID<T>.ID);
    }
}