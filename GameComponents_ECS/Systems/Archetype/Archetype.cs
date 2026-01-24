using System.Collections.Immutable;
using System.Collections.Generic;

namespace GameComponents.Systems;

public partial record Archetype 
{
    public Archetype(int capacity, HashSet<Type> typeData) 
    {
        Capacity = capacity;
        ComponentTypes = typeData;
        TypeCount = typeData.Count;
        var toArr = ComponentTypes.ToArray();
        
        DataComponents = new Array[TypeCount];
        _indexMap = new int[ComponentMeta.nextIndex + 1];
        
        for(int i = 0; i < TypeCount; i++) DataComponents[i] = Array.CreateInstance(toArr[i], capacity);
        
        for(int i = 0; i < _indexMap.Length; i++) 
        {
            
        }
        
        if (ComponentTypes.Count < 1) 
        {
            collectedTypes = string.Empty;
            return;
        }
        
        collectedTypes = string.Join(", ", ComponentTypes) + '.';
    }
    
    public ref Array this[int index] => ref DataComponents[index];
    
    public Archetype(int capacity, params Type[] types) : this(capacity, types.ToHashSet()) {}
    
    public static Archetype Empty => new Archetype(0, Array.Empty<Type>());
    public static Archetype Create(int capacity, HashSet<Type> types) => new Archetype(capacity, types);
    public static Archetype Create(int capacity, params Type[] types) => new Archetype(capacity, types);
    
    public ImmutableHashSet<Type> ToImmutableHashset() => ComponentTypes.ToImmutableHashSet();
    public ImmutableArray<Type> ToImmutableArray() => ComponentTypes.ToImmutableArray();
    public ImmutableList<Type> ToImmutableList() => ComponentTypes.ToImmutableList();
    
    public List<Type> ToList() => ComponentTypes.ToList();
}