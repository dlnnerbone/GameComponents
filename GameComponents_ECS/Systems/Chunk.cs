using GameComponents.Helpers;
using GameComponents.Storages;
namespace GameComponents.Systems;

// a simple Row class for archetypes for expanding and resizing an array of components with an underlying type.

public class Chunk
{
    internal Array _components;
    internal Type UnderlyingType;
    internal int Length => _components.Length;
    private object[] _objects;

    internal object this[int index] => _components.GetValue(index)!;
    internal T[] GetAs<T>() => (T[])_components;

    internal Chunk(uint capacity, Type underlyingType)
    {
        UnderlyingType = underlyingType;
        _components = Array.CreateInstance(UnderlyingType, (int)capacity);
        _objects = (object[])_components; 
    }

    internal void Expand()
    {
        ArrayHelper.CopyAndResize(ref _objects, Length * 2, Length);
    }

    internal void ExpandBy(int amount)
    {
        ArrayHelper.CopyAndResize(ref _objects, Length + amount, Length);
    }

    internal void CompactTo(int indexLength)
    {
        ArrayHelper.CopyAndResize(ref _objects, indexLength, indexLength);
    }
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