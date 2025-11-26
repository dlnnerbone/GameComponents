using GameComponents.Interfaces;
using Microsoft.Xna.Framework;
using System;
namespace GameComponents;
/*  public struct Particle : IDirection
{
    // private fields
    private Vector2 direction = Vector2.UnitX;
    private float radians = 0f;
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
    public float Age { get; set; }
    public float LifeTime { get; set; }
    public float Speed { get; set; }
    public float SpeedMulti { get; set; }
    public bool IsActive { get; set; }
} */