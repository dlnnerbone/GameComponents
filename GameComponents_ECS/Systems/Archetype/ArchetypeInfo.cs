namespace GameComponents.Systems;

public readonly ref struct ArchetypeInfo 
{
    public readonly short[] IndexMap;
    public readonly int ArchetypeID;
    public readonly int InlinePosition;
    
    public ArchetypeInfo(Archetype archetype) 
    {
        ArchetypeID = archetype.ArchetypeID;
        IndexMap = archetype._indexMap;
        InlinePosition = archetype._nextPosition;
    }
    
    public static ArchetypeInfo GetArchetypeInfo(Archetype archetype) => archetype.GetInfo();
}