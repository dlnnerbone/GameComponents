using System.ComponentModel.Design.Serialization;
using GameComponents.Storages;
using GameComponents.Systems;
namespace GameComponents;

public class World
{
    public uint EntityCount { get; internal set; } = 0;
    public readonly int RootID;
    internal ushort _defaultArchetypeID = 0;
    internal readonly FastStack<Entity> _entities;
    internal readonly Query _query;

    public ref readonly Entity this[int index] => ref _entities[index];

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
        ushort archetypeOccup = entityAsReference.Occupation;
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
        var position = entityToLook.Occupation;
        return _query._archetypeCollection[position].GetInfo(); 
    }

    /// <summary>
    /// Detach method only moves the Archetype ID into the first index, which should be the first Null archetype.
    /// Note: an internal _defaultArchetypeID variable is used which can be set with the DefaultArchetypeID()
    /// method.    
    /// </summary>
    /// <param name="index"></param>
    public void Detach(int index)
    {
        if (index > _entities.Count - 1) return;
        ref readonly Entity entity = ref _entities[index];

        entity.SetArchetype(_defaultArchetypeID);
    }

    public void SetDefaultArchetype(Archetype archetype) => _defaultArchetypeID = (ushort)archetype.ArchetypeID;
    public void SetDefaultArchetype(ushort defOccup) => _defaultArchetypeID = defOccup;

    
}