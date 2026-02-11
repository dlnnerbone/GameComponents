namespace GameComponents;

public static class EntityTracker 
{
    internal static uint _entityCount = 0;
    public static uint EntityCount => _entityCount;
    
    internal static int Count() => (int)_entityCount++;
}