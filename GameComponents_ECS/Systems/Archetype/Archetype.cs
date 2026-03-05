using System.Collections;

namespace GameComponents.Systems;

public partial record Archetype 
{
    private readonly string _collectedTypes;
    
    internal int _nextPosition;
    internal readonly sbyte[] _indexMap;
    internal readonly BitArray _bits;
    
    public readonly HashSet<Type> FoundTypes;
    public readonly Chunk[] DataMatrix;
    public readonly byte TypeCount;
    // this will throw an error if the archetype is defined as 'null', beware.
    public int Capacity => DataMatrix[0].Length;
    // this is an ID that helps with corresponding to an index for fast look-ups.
    public readonly int ArchetypeID;
    
    public Chunk this[int matrixID] => DataMatrix[matrixID];
    
    public override string ToString() => _collectedTypes;
    
    internal Archetype() 
    {
        _collectedTypes = string.Empty;
        _nextPosition = -1;
        _indexMap = null!;
        FoundTypes = null!;
        DataMatrix = null!;
        TypeCount = 0;
        ArchetypeID = -1;
        _bits = null!;
    }
    
    internal Archetype(int initialCapacity, int archetypeID, HashSet<Type> typesToOccupy)
    {
        // convert hashset into an array.
        var typeArray = typesToOccupy.ToArray();

        FoundTypes = typesToOccupy;
        _nextPosition = -1;
        TypeCount = (byte)typeArray.Length;
        ArchetypeID = archetypeID;

        // initializing the indexmap's length to the amount of types defined from the Component metadata static class.
        _indexMap = new sbyte[ComponentMetadata.Index + 1];
        _indexMap.AsSpan().Fill(-1);

        // bits array for faster lookup for finding if an archetype contains a specified type.
        _bits = new BitArray(_indexMap.Length, false);
        DataMatrix = new Chunk[TypeCount];

        for(sbyte i = 0; i < TypeCount; i++)
        {
            DataMatrix[i] = new Chunk(initialCapacity, typeArray[i]);

            int compID = ComponentDictionary.GetID(typeArray[i]);
            _indexMap[compID] = i;
            _bits[compID] = true;
        }

        if (Capacity <= 0 || TypeCount == 0)
        {
            _collectedTypes = string.Empty;
            return;
        }

        _collectedTypes = string.Join(", ", FoundTypes) + '.';
    }

    public static Archetype Null => new Archetype();
}