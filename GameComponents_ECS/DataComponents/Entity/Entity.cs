namespace GameComponents;

public struct Entity : IEquatable<Entity> 
{
    internal int worldID;
    public readonly int WorldID => worldID;
    
    public Entity() => worldID = EntityTracker.Count();
    
    internal Entity(int worldID) 
    {
        this.worldID = worldID;
        if (worldID == -1) ++EntityTracker._nullEntities;
    }
    
    public static Entity Null => new Entity(-1);
    public static implicit operator int(Entity ent) => ent.worldID;
    
    public readonly bool Equals(Entity entity) => this == entity;
}