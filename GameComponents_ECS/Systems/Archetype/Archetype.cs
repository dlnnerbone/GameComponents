using System.Collections.Immutable;
using System.Collections.Generic;

namespace GameComponents.Systems;

public partial record Archetype 
{
    private readonly string _collectedTypes;
    
    internal int _nextPosition;
    internal readonly short[] _indexMap;
    
    public readonly HashSet<Type> FoundTypes;
    public readonly Array[] DataMatrix;
    public readonly int Capacity;
    public readonly int TypeCount;
    public readonly int ArchetypeID;
    
    public Array this[int matrixID] => DataMatrix[matrixID];
    
    public override string ToString() => _collectedTypes;
    
    private Archetype() 
    {
        _collectedTypes = string.Empty;
        _nextPosition = -1;
        _indexMap = null!;
        FoundTypes = null!;
        DataMatrix = null!;
        Capacity = 0;
        TypeCount = 0;
        ArchetypeID = -1;
    }
    
    public Archetype(int capacity, uint archID, HashSet<Type> selectedTypes) 
    {
        Capacity = capacity;
        TypeCount = selectedTypes.Count;
        FoundTypes = selectedTypes;
        _nextPosition = -1;
        ArchetypeID = (int)archID;
        var typeArray = FoundTypes.ToArray();
        
        ComponentDictionary.AddRange(FoundTypes);
        _indexMap = new short[ComponentMetadata.Index + 1];
        _indexMap.AsSpan().Fill(-1);
        DataMatrix = new Array[TypeCount];
        
        for(short i = 0; i < TypeCount; i++) 
        {
            DataMatrix[i] = Array.CreateInstance(typeArray[i], Capacity);
            
            int compID = ComponentDictionary.GetID(typeArray[i]);
            _indexMap[compID] = i;
        }
        
        if (FoundTypes.Count == 0) 
        {
            _collectedTypes = string.Empty;
            return;
        }
        
        _collectedTypes = string.Join(", ", FoundTypes) + '.';
    }
    
    public Archetype(int cap, uint archID, IEnumerable<Type> types) : this(cap, archID, types.ToHashSet()) {}
    public Archetype(int cap, uint archID, params Type[] types) : this(cap, archID, types.ToHashSet()) {}
    
    public static Archetype Null => new Archetype();
}