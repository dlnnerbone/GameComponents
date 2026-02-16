using System.Collections;
namespace GameComponents.Systems;

public readonly ref struct ArchetypeInfo 
{
    public readonly sbyte[] IndexMap;
    public readonly BitArray Bits;
    public readonly int ArchetypeID;
    public readonly int InlinePosition;
    
    public ArchetypeInfo(Archetype archetype) 
    {
        ArchetypeID = archetype.ArchetypeID;
        IndexMap = archetype._indexMap;
        Bits = archetype._bits;
        InlinePosition = archetype._nextPosition;
    }
    
    public static ArchetypeInfo GetArchetypeInfo(Archetype archetype) => archetype.GetInfo();
}