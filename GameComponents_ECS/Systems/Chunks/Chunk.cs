using GameComponents.Helpers;
using GameComponents.Storages;
namespace GameComponents.Systems;

// a simple Row class for archetypes for expanding and resizing an array of components with an underlying type.

public class Chunk
{
    internal Array _components;
    internal Type UnderlyingType;
    public int Length => _components.Length;

    public object this[int index] => _components.GetValue(index)!;
    internal T[] GetAs<T>() => (T[])_components;

    internal Chunk(uint capacity, Type underlyingType)
    {
        UnderlyingType = underlyingType;
        _components = Array.CreateInstance(UnderlyingType, (int)capacity);
    }

    internal void Expand()
    {
        Array newArray = Array.CreateInstance(UnderlyingType, Length * 2);
        for(int i = 0; i < Length; i++)
        {
            newArray.SetValue(_components.GetValue(i), i);
        }
        _components = newArray;
    }

    internal void ExpandBy(int amount)
    {
        int value = Length + amount;
        int max = Math.Max(Length, value);

        Array newArray = Array.CreateInstance(UnderlyingType, max);
        for(int i = 0; i < Length; i++)
        {
            newArray.SetValue(_components.GetValue(i), i);
        }
        _components = newArray;
    }

    internal void CompactTo(int index)
    {
        if (index > Length - 1) throw new ArgumentOutOfRangeException($"index of {index} is out of the bounds of the array.");

        Array newArray = Array.CreateInstance(UnderlyingType, index);
        for(int i = 0; i < index; i++)
        {
            newArray.SetValue(_components.GetValue(i), i);
        }
        _components = newArray;
    }

    public ChunkViewer GetChunkViewer() => new ChunkViewer(this);
}

public ref struct ChunkViewer
{
    public readonly Array Components;
    public readonly Type UnderlyingType;

    public ChunkViewer(Chunk chunk)
    {
        Components = chunk._components;
        UnderlyingType = chunk.UnderlyingType;
    }

    public static T[] GetComponents<T>(Chunk chunk) => (T[])chunk._components;
}