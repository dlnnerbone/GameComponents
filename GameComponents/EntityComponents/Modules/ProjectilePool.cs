using Microsoft.Xna.Framework;
using GameComponents;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class ObjectPool<T> where T : IPoolable 
{
    private readonly Func<T> _projectileFactory;
    public readonly Stack<T> InactiveProjectiles = new Stack<T>();
    // helper getters
    public int Count => InactiveProjectiles.Count;
    
    public ObjectPool(Func<T> factory) 
    {
        _projectileFactory = factory;
    }
    
    public T Request() 
    {
        return InactiveProjectiles.Count > 0 ? InactiveProjectiles.Pop() : _projectileFactory();
    }
    
    public void Push(T proj) 
    {
        InactiveProjectiles.Push(proj);
        proj.Reset();
    }
}