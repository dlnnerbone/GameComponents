using Microsoft.Xna.Framework;
namespace GameComponents;

public struct PositionComponent 
{
    public Vector2 Position;
    
    public float X { get => Position.X; set => Position.X = value; }
    public float Y { get => Position.Y; set => Position.Y = value; }
}