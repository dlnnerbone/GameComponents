using GameComponents.Interfaces;
using Microsoft.Xna.Framework;
using System;
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
    
    public float Radians 
    {
        get => radians;
        set 
        {
            radians = value;
            direction = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
    
    public float LayerDepth { get => layerDepth; set => layerDepth = MathHelper.Clamp(value, 0f, 1f); }
    
    public Color Tint { get; set; } = Color.White;
    public float Age { get; set; }
    public float LifeTime { get; set; }
    public float Speed { get; set; }
    public float SpeedMulti { get; set; } = 1f;
    public bool IsActive { get; set; }
    
    
    public Particle() : this(Vector2.Zero) {}
    
    public Particle(Vector2 position) 
    {
        Position = position;
        Age = 0f;
        SpeedMulti = 1f;
    }
    
    public Particle(Vector2 position, float lifeTime) : this(position)
    {
        LifeTime = Math.Abs(lifeTime);
    }
    
    public Particle(Vector2 position, float lifeTime, Vector2 direction) : this(position, lifeTime) 
    {
        Direction = direction;
    }
    
    public Particle(Vector2 position, float lifeTime, Vector2 direction, float speed) : this(position, lifeTime, direction)
    {
        Speed = Math.Abs(speed);
    }
    
    public Particle(Vector2 position, float lifeTime, Vector2 direction, float speed, bool isActive) : this(position, lifeTime, direction, speed) 
    {
        IsActive = isActive;
    }
}