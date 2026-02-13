namespace GameComponents;

public readonly ref struct EntityInfo 
{
    public readonly int WorldID;
    public readonly ushort ArchetypeOccupation;
    public readonly ushort Position;
    
    internal EntityInfo(in Entity ent, EntityLocation location) 
    {
        WorldID = ent.worldID;
        ArchetypeOccupation = location.ArchetypeOccupation;
        Position = location.Position;
    }
    
    public override string ToString() => $"ID: {WorldID}, Occupation: {ArchetypeOccupation}, Position: {Position}";
}