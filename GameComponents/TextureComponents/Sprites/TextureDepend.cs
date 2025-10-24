using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Interfaces;
using System;
namespace GameComponents.Rendering;
public abstract class TextureDependencies : ITexture 
{
    // private fields
    protected Vector2 direction = Vector2.UnitX;
    protected Vector2 scale = Vector2.One;
    protected Vector2 origin = Vector2.Zero;
    protected float depth = 0;
    protected float radians = 0;
    protected SpriteEffects effects = SpriteEffects.None;
    protected Color color = Color.White;
    protected bool isVisible = true;
    // public properties
    public readonly Vector2 DScale = Vector2.One;
    public readonly Vector2 D_Direction = Vector2.UnitX;
    public const float DRadians = 0;
    public readonly Color DColor = Color.White; 
    
    public virtual Vector2 Direction 
    {
        get => direction;
        set 
        {
            direction = value != Vector2.Zero ? Vector2.Normalize(value) : Vector2.UnitX;
            radians = (float)Math.Atan2(direction.Y, direction.X);
        }
    }
    public virtual Vector2 Scale { get => scale; set => scale = new Vector2(Math.Abs(value.X), Math.Abs(value.Y)); }
    public virtual Vector2 Origin { get => origin; set => origin = value; }
    public virtual SpriteEffects Effects { get => effects; set => effects = value; }
    public virtual Color Color { get => color; set => color = value; }
    public virtual float LayerDepth { get => depth; set => depth = MathHelper.Clamp(value, 0f, 1f); }
    public virtual float Radians 
    {
        get => radians;
        set 
        {
            radians = value;
            direction = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
    // extra properties
    public virtual float R { get => Color.R; set => Color = new Color(value, Color.G, Color.B, Color.A); }
    public virtual float G { get => Color.G; set => Color = new Color(Color.R, value, Color.B, Color.A); }
    public virtual float B { get => Color.B; set => Color = new Color(Color.R, Color.G, value, Color.A); }
    public virtual float Opacity { get => Color.A; set => Color = new Color(Color.R, Color.G, Color.B, value); }

    public bool IsVisible 
    {
        get => Opacity > 0;
        set => Opacity = !value ? 0 : Opacity;
    }
    
    public virtual bool Flip_H 
    {
        get => (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;
        set 
        {
            effects = value ? effects | SpriteEffects.FlipHorizontally : effects & ~SpriteEffects.FlipHorizontally;
        }
    }
    public virtual bool Flip_V 
    {
        get => (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
        set 
        {
            effects = value ? effects | SpriteEffects.FlipVertically : effects & ~SpriteEffects.FlipVertically;
        }
    }
    public virtual void ResetAlignment() => effects = SpriteEffects.None;
    protected TextureDependencies() {}
}