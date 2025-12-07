using GameComponents.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Rendering;
public struct Particle : IDirection
{
    // private fields
    private Vector2 direction = Vector2.UnitX;
    private float radians = 0f;
    private float layerDepth = 1f;
    // public properties
    
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Direction 
    {
        get => direction;
        set 
        {
            direction = Vector2.Normalize(value);
            radians = (float)Math.Atan2(-direction.Y, direction.X);
        }
    }
    
    public Vector2 Scale { get; set; } = Vector2.One;
    
    public float Radians 
    {
        get => radians;
        set 
        {
            radians = value;
            direction = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
    
    public SpriteEffects Effects { get; set; }
    public float LayerDepth { get => layerDepth; set => layerDepth = MathHelper.Clamp(value, 0f, 1f); }
    public Color Color { get; set; } = Color.White;
    public bool IsActive { get; set; } = true;
    public byte ID { get; set; } = 0;
    
    public Particle() : this(Vector2.Zero) {}
    
    public Particle(Vector2 position) 
    {
        Position = position;
    }
    
    public Particle(Vector2 position, byte id) : this(position)
    {
        ID = (byte)Math.Abs(id);
    }
}