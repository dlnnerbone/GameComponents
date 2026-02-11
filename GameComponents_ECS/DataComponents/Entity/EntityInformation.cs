namespace GameComponents;

internal ref struct EntityInfo
{
    public readonly int WorldID;
    public readonly ushort Position;
    public readonly ushort Version;
    
    public EntityInfo(int worldID, ushort position, ushort version) 
    {
        WorldID = worldID;
        Position = position;
        Version = version;
    }
    
    public EntityInfo(ref Entity entity, ushort position, ushort version) : this(entity.worldID, position, version) {}
    
    public override string ToString() => $"Entity ID: {WorldID}, Position/Index: {Position}, Entity Version: {Version}";
}