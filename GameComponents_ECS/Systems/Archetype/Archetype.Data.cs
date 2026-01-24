using System.Linq;
using System;
namespace GameComponents.Systems;

public partial record Archetype 
{
    private readonly int[] _indexMap;
    
    public readonly Array[] DataComponents;
    public readonly int Capacity;
    
    public ref T GetComponent<T>(int rowIndex, int columnIndex) 
    {
        T[] array = (T[])DataComponents[rowIndex];
        return ref array[columnIndex];
    }
}