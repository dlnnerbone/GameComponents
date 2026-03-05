namespace GameComponents.Systems;

public partial record Archetype 
{
    public ref TComp GetComponent<TComp>(int columnIndex) 
    {
        int compID = ComponentID<TComp>.ID;
        if ((uint)compID > _indexMap.Length - 1) throw new NullReferenceException("Attempted to search for a component not defined in an archetype.");
        
        int rowIndex = _indexMap[compID];
        if (rowIndex == -1) throw new NullReferenceException("component not found in Archetype.");
        
        TComp[] components = DataMatrix[rowIndex].GetAs<TComp>();
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

    internal void Expand()
    {
        if ((uint)Capacity == 0 || TypeCount == 0) throw new ArgumentException("Can't expand this archetype because of improper construction.");
        for(int i = 0; i < TypeCount; i++) DataMatrix[i].Expand();
    }

    internal void ExpandBy(uint amount)
    {
        if ((uint)Capacity == 0 || TypeCount == 0) throw new ArgumentException("Can't expand this archetype because of improper construction.");
        for(int i = 0; i < TypeCount; i++) DataMatrix[i].ExpandBy((int)amount);
    }
    
    public ArchetypeInfo GetInfo() => new ArchetypeInfo(this);
    
    public override int GetHashCode() => HashCode.Combine(FoundTypes, _collectedTypes, TypeCount, _indexMap, ArchetypeID, _bits);
}