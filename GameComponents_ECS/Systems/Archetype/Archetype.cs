using System.Collections.Immutable;
using System.Collections.Generic;

namespace GameComponents.Systems;

public partial record Archetype 
{
    // types
    private readonly string collectedTypes;
    
    internal int[] _indexMap;
    
    public override string ToString() => collectedTypes;
    public readonly HashSet<Type> ComponentTypes;
    public readonly int TypeCount;
    // Data
    public readonly Array[] DataComponents;
    public readonly int Capacity;
    
    public Archetype(int dataCapacity, HashSet<Type> types) 
    {
        ComponentTypes = types;
        Capacity = dataCapacity;
        TypeCount = types.Count;
        var typeArray = ComponentTypes.ToArray();
        
        ComponentDictionary.AddRange(typeArray);
        DataComponents = new Array[TypeCount];
        
        _indexMap = new int[ComponentDictionary.GetHighestID() + 1];
        _indexMap.AsSpan().Fill(-1);
        
        for(int i = 0; i < TypeCount; i++) 
        {
            DataComponents[i] = Array.CreateInstance(typeArray[i], Capacity);
            
            int compID = ComponentDictionary.GetID(typeArray[i]);
            _indexMap[compID] = i;
        }
        
        if (typeArray.Length == 0) 
        {
            collectedTypes = string.Empty;
            return;
        }
        
        collectedTypes = string.Join(", ", ComponentTypes) + '.';
    }
    
    public Archetype(int capacity, params Type[] types) : this(capacity, types.ToHashSet()) {}
    public Archetype(int capacity, IEnumerable<Type> types) : this(capacity, types.ToHashSet()) {}
    
    public static Archetype Empty = new Archetype(0, new HashSet<Type>());
    public static Archetype Create(int capacity, HashSet<Type> types) => new Archetype(capacity, types);
    public static Archetype Create(int capacity, params Type[] types) => new Archetype(capacity, types);
    public static Archetype Create(int capacity, IEnumerable<Type> types) => new Archetype(capacity, types);
    
    public ref Array this[int index] => ref DataComponents[index];
    public ImmutableArray<int> GetIndexMap() => _indexMap.ToImmutableArray();
    public ImmutableHashSet<Type> ToImmutableHashSet() => ComponentTypes.ToImmutableHashSet();
}