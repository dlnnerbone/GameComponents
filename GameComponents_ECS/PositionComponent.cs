using Microsoft.Xna.Framework;
namespace GameComponents;

public struct PositionComponent 
{
    public Vector2 Position;
    
    public float X { get => Position.X; set => Position.X = value; }
    public float Y { get => Position.Y; set => Position.Y = value; }
    
    public PositionComponent(Vector2 pos) => Position = pos;
    
    public PositionComponent(float x, float y) => Position = new Vector2(x, y);
}