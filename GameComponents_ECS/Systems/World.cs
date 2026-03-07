using System.ComponentModel.Design.Serialization;
using GameComponents.Storages;
using GameComponents.Systems;
namespace GameComponents;

public class World 
{
    public uint EntityCount { get; internal set; } = 0;
    public readonly int RootID;
    internal readonly FastStack<Entity> _entities;
    internal readonly Query _query;

    public ref readonly Entity Retrieve(int worldID) => ref _entities[worldID];

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

    public void CreateEntity(Archetype archetype)
    {
        if (archetype.ArchetypeID == -1) throw new ArgumentNullException("Attempting to ssign an entity to a null archetype is invalid.");
        ushort nextPosition = (ushort)archetype.Forward(out bool isGreater);

        if (isGreater) archetype.Expand();

        Entity entity = new Entity((int)EntityCount++, (ushort)archetype.ArchetypeID, nextPosition);
        _entities.Push(entity);
    }

    public void CreateEntity(Entity entityAsReference)
    {
        if (entityAsReference.WorldID == -1) throw new ArgumentNullException($"entity is null and can't be used to find an archetype.");
        ushort archetypeOccup = entityAsReference.ArchetypeOccupation;
        Archetype selectedArchetype = _query._archetypeCollection[archetypeOccup];
        ushort nextPosition = (ushort)selectedArchetype.Forward(out bool isGreater);

        if (isGreater) selectedArchetype.Expand();

        Entity entity = new Entity((int)EntityCount++, archetypeOccup, nextPosition);
        _entities.Push(entity);
    }

    public void CreateNullEntity()
    {
        _entities.Push(Entity.Null);
        ++EntityCount;
    }

    public ArchetypeInfo GetArchetypeInfoFrom(Entity entityToLook)
    {
        if (entityToLook.IsNull()) throw new ArgumentNullException($"Entity is null");
        var position = entityToLook.ArchetypeOccupation;
        return _query._archetypeCollection[position].GetInfo(); 
    }

    public void Detach(int index)
    {
        if (index > _entities.Count - 1) return;
        ref readonly Entity entity = ref _entities[index];

        entity.SetArchetype(0);
    }
    
}