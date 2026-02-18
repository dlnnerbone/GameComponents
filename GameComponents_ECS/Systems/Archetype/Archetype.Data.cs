namespace GameComponents.Systems;

public partial record Archetype 
{
    public ref TComp GetComponent<TComp>(int columnIndex) 
    {
        int compID = ComponentID<TComp>.ID;
        if ((uint)compID > _indexMap.Length - 1) throw new NullReferenceException("Attempted to search for a component not defined in an archetype.");
        
        int rowIndex = _indexMap[compID];
        if (rowIndex == -1) throw new NullReferenceException("component not found in Archetype.");
        
        TComp[] components = (TComp[])DataMatrix[rowIndex];
        return ref components[columnIndex];
    }
    
    public int Forward() => ++_nextPosition;
    internal int Retreat() => _nextPosition--;
    
    internal int Forward(out bool isGreaterThanCapacity) 
    {
        int value = Forward();
        isGreaterThanCapacity = value > Capacity - 1;
        return value;
    }
    
    public ArchetypeInfo GetInfo() => new ArchetypeInfo(this);
    
    public override int GetHashCode() => HashCode.Combine(Capacity, FoundTypes, _collectedTypes, TypeCount, _indexMap, ArchetypeID, _bits);
}