using Microsoft.Xna.Framework;
using GameComponents;
namespace GameComponents.Entity;
public class ProjectilePool<T> where T : Projectile 
{
    public readonly Stack<T> InactiveProjectiles = new Stack<T>();
    public T GetProjectile() 
    {
        if (InactiveProjectiles.Count > 0)
            return InactiveProjectiles.Pop();
        else return;
    }
    public void ReturnProjectile(T proj) 
    {
        InactiveProjectiles.Push(proj);
        proj.PurgeFlags();
    }
}