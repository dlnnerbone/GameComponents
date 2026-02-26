using System.ComponentModel.Design.Serialization;
using GameComponents.Storages;
using GameComponents.Systems;
namespace GameComponents;

public class World 
{
    // reference to the query class.
    internal readonly Query _query;
    // stack of entities for storing.
    internal readonly FastStack<Entity> _entities;
    // the root id of the world, is used to identify a world if there are multiple.
    public readonly ushort RootID;
    internal uint _entityCount;

    public World(ushort rootID)
    {
        _query = new Query(this);
        _entities = new FastStack<Entity>(16);
        RootID = rootID;
    }

    public World(ushort rootID, int initialEntityCount)
    {
        _query = new(this);
        _entities = new FastStack<Entity>(initialEntityCount);
        RootID = rootID;
    }

    //

    public void CreateEntity(ushort archetypeOccupation)
    {
        int id = (int)_entityCount++;
        Archetype arch = _query._archetypeCollection[archetypeOccupation];
        ushort positionID = (ushort)arch.Forward(out bool isGreater);

        if (isGreater)
        {
            
        }

        Entity entity = new(id, archetypeOccupation, positionID);
        _entities.Push(entity);
    }


}