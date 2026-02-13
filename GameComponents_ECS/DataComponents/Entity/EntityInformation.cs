namespace GameComponents;

internal readonly struct EntityLocation
{
    public readonly ushort ArchetypeOccupation;
    public readonly ushort Position;
    
    public EntityLocation(ushort archetypeOccup, ushort position)
    {
        ArchetypeOccupation = archetypeOccup;
        Position = position;
    }
    
    public override string ToString() => $"Position/Index: {Position}, Occupied in Archetype {ArchetypeOccupation}";
}