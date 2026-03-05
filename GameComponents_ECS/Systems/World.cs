using System.ComponentModel.Design.Serialization;
using GameComponents.Storages;
using GameComponents.Systems;
namespace GameComponents;

public class World 
{
    public uint EntityCount = 0;
    public readonly int RootID;
    internal readonly FastStack<Entity> _entities;
    internal readonly Query _query;

    public World(int rootID)
    {
        RootID = rootID;
        _entities = new FastStack<Entity>(4);
        _query = new(this);
    }

    public World(int rootID, int initialEntityCount)
    {
        RootID = rootID;
        _entities = new FastStack<Entity>(initialEntityCount);
        _query = new(this);
    }

    
}