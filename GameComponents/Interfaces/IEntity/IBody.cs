using Microsoft.Xna.Framework;
namespace GameComponents.Interfaces;
public interface IBodyComponent 
{
    Rectangle Bounds { get; }
    Vector2 Size { get; set;}
    Vector2 Position { get; set;}
}