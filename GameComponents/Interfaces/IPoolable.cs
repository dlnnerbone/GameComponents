using Microsoft.Xna.Framework;
namespace GameComponents.Interfaces;
public interface IPoolable<T> 
{
    abstract void Reset();
}