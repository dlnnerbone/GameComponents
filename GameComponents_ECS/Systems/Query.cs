using GameComponents.Storages;
namespace GameComponents.Systems;

public class Query 
{
    internal World _world;
    internal FastStack<Archetype> _archetypeCollection;

    public Query(World world) 
    {
        _world = world;
        _archetypeCollection = new FastStack<Archetype>(4);
        _archetypeCollection.Push(Archetype.Null);
    }

    public void CreateArchetype(int capacity, HashSet<Type> types)
    {
        Archetype archetype = new Archetype(capacity, _archetypeCollection._nextIndex, types);
        _archetypeCollection.Push(archetype);
    }

    public void CreateArchetype(int capacity, HashSet<Type> types, out int index)
    {
        Archetype archetype = new Archetype(capacity, _archetypeCollection._nextIndex, types);
        index = archetype._nextPosition;
        _archetypeCollection.Push(archetype);
    }
}