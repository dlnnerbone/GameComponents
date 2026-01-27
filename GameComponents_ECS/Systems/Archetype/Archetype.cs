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
        TypeCount = types.Count;
        Capacity = dataCapacity;
        
        ComponentTypes = types;
        DataComponents = new Array[TypeCount];
        Type[] fixedTypes = types.ToArray();
        
        for(int i = 0; i < TypeCount; i++) 
        {
            DataComponents[i] = Array.CreateInstance(fixedTypes[i], Capacity);
        }
        
        _indexMap = new int[ComponentMeta.NextID + 1];
        _indexMap.AsSpan().Fill(-1);
        
        for(int i = 0; i < fixedTypes.Length; i++) 
        {
            if (!IDDictionary.GetInternalDictionary().ContainsKey(fixedTypes[i])) 
            {
                throw new ArgumentOutOfRangeException($"{fixedTypes[i].GetType()} can't be found inside the ID Dictionary.");
            }
            
            int compID = IDDictionary.GetValue(fixedTypes[i]);
            _indexMap[compID] = i;
        }
        
        if (fixedTypes.Length == 0) 
        {
            collectedTypes = string.Empty;
            return;
        }
        
        collectedTypes = string.Join(", ", types) + '.';
    }
    
    public Archetype(int capacity, params Type[] types) : this(capacity, types.ToHashSet()) {}
    public Archetype(int capacity, IEnumerable<Type> types) : this(capacity, types.ToHashSet()) {}
    
    public ref Array this[int index] => ref DataComponents[index];
    public ImmutableArray<int> GetIndexMap() => _indexMap.ToImmutableArray();
    public ImmutableHashSet<Type> ToImmutableHashSet() => ComponentTypes.ToImmutableHashSet();
}