using GameComponents.Systems;
namespace GameComponents;

public struct Entity : IEquatable<Entity> 
{
    internal int WorldID;
    internal ushort Occupation;
    internal ushort Position;
    
    internal Entity(int id, ushort archetypeOccup, ushort position) 
    {
        WorldID = id;
        Occupation = archetypeOccup;
        Position = position;
    }

    internal void SetArchetype(ushort occupation) => Occupation = occupation;
    internal void SetArchetype(Archetype archetype) => Occupation = (ushort)archetype.ArchetypeID;
    internal void SetPosition(ushort position) => Position = position;
    
    public override string ToString() => $"World ID: {WorldID}";
    
    public bool IsNull() => WorldID == -1;
    public bool Equals(Entity entity) => this == entity;
    public bool Equals(in Entity entity) => this == entity;
    
    public EntityInfo GetInfo() => new(this);
    
    public static implicit operator int(Entity ent) => ent.WorldID;
    public static Entity Null => new(-1, 0, 0);
    public static bool IsNull(in Entity entity) => entity.WorldID == -1;
    public static EntityInfo GetInfo(Entity entity) => new EntityInfo(entity);
    
    public static bool Equals(Entity focusedEntity, Entity other) => focusedEntity == other;
    public static bool Equals(in Entity focused, in Entity other) => focused == other;

    public ref struct EntityInfo
    {
        public readonly int WorldID;
        public readonly ushort Occupation;
        public readonly ushort Position;

        internal EntityInfo(Entity entity)
        {
            this.WorldID = entity.WorldID;
            this.Occupation = entity.Occupation;
            this.Position = entity.Position;
        }

        public override string ToString() => $"{WorldID}, {Occupation}, {Position}";
    }

}