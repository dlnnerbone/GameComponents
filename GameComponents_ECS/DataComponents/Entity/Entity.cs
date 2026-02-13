namespace GameComponents;

public struct Entity : IEquatable<Entity> 
{
    internal int worldID;
    public readonly int WorldID => worldID;
    
    public Entity() => worldID = EntityTracker.Count();
    internal Entity(int id) 
    {
        worldID = id;
        EntityTracker._entityCount++;
    }
    
    public override string ToString() => $"World ID: {worldID}";
    
    public bool IsNull() => worldID == -1;
    public bool Equals(Entity entity) => this == entity;
    public bool Equals(in Entity entity) => this == entity;
    
    public static implicit operator int(Entity ent) => ent.worldID;
    public static Entity Null => new Entity(-1);
    public static bool IsNull(in Entity entity) => entity.worldID == -1;
    
    public static bool Equals(Entity focusedEntity, Entity other) => focusedEntity == other;
    public static bool Equals(in Entity focused, in Entity other) => focused == other;
}