namespace GameComponents;

public readonly ref struct EntityInfo 
{
    public readonly int WorldID;
    public readonly ushort ArchetypeOccupation;
    public readonly ushort Position;
    
    public EntityInfo(Entity entityToView) 
    {
        WorldID = entityToView.WorldID;
        ArchetypeOccupation = entityToView.ArchetypeOccupation;
        Position = entityToView.Position;
    }
    
    public override string ToString() => $"Entity ID: {WorldID}, Occupied in Archetype: {ArchetypeOccupation}, Position: {Position}";
}