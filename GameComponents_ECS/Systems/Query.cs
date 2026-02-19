using GameComponents.Storages;
namespace GameComponents.Systems;

public class Query 
{
    internal World _world;
    
    public Query(World world) 
    {
        _world = world;
    }
}