using Microsoft.Xna.Framework;
using GameComponents;
namespace GameComponents.Entity;
public class ProjectilePool<T> where T : Projectile 
{
    private readonly Func<T> _projectileFactory;
    public readonly Stack<T> InactiveProjectiles = new Stack<T>();
    
    public ProjectilePool(Func<T> factory) 
    {
        _projectileFactory = factory;
    }
    
    public T GetProjectile() 
    {
        if (InactiveProjectiles.Count > 0)
            return InactiveProjectiles.Pop();
        else return _projectileFactory();
    }
    
    public void ReturnProjectile(T proj) 
    {
        InactiveProjectiles.Push(proj);
        proj.Reset();
    }
}