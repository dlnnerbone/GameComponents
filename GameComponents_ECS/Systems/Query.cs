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
    }


}