using System;
namespace GameComponents.Systems;

public partial record Archetype 
{
    
    public ref T GetComponent<T>(int columnIndex) 
    {
        int typeID = ComponentID<T>.ID;
        
        if ((uint)typeID > (uint)_indexMap.Length) 
        {
            throw new ArgumentOutOfRangeException("Missing Component, check if type is within the range of the ID dictionary.");
        }
        
        int rowIndex = _indexMap[typeID];
        if (rowIndex == -1) throw new ArgumentNullException($"There is no such component in the Archetype.");
        
        T[] _buffer = (T[])DataComponents[rowIndex];
        return ref _buffer[columnIndex];
    }
    
    public T GetComponentAsCopy<T>(int columnIndex) 
    {
        ref var comp = ref GetComponent<T>(columnIndex);
        return comp;
    } 
}