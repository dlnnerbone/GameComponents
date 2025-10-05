using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Interfaces;
using System;
namespace GameComponents.Rendering;
public class Sprite : ITexture 
{
    private Texture2D texture;
    private Color[] colors;
    private Vector2 scale = Vector2.One;
    private Vector2 origin = Vector2.Zero;
    private float depth = 0;
    private Vector2 direction = Vector2.UnitX;
    private float radians = 0;
    private SpriteEffects effects = SpriteEffects.None;
    
    // private fields.
    /// <summary>
    /// The Main Texture of the class.
    /// </summary>
    public Texture2D Texture { get { return texture; } set { texture = value; } }
    /// <summary>
    /// the Bounds of the Texture in px
    /// </summary>
    public Rectangle Bounds => texture.Bounds;
    /// <summary>
    /// the Scale of the Texture.
    /// </summary>
    public Vector2 Scale { get { return scale; } set { scale = new Vector2((float)Math.Abs(value.X), (float)Math.Abs(value.Y)); } }
    /// <summary>
    /// the Origin of the texture
    /// </summary>
    public Vector2 Origin { get { return origin; } set { origin = value; } }
    /// <summary>
    /// the direction of which the texture faces
    /// </summary>
    public Vector2 Direction 
    {
        get => direction;
        set 
        {
            direction = Vector2.Normalize(value);
            radians = (float)Math.Atan2(direction.Y, direction.X);
        }
    }
    /// <summary>
    /// the radians of the Texture, uses Atan2 to translate Direction into radians, setter value uses Cos and Sin to set the DIrection.
    /// </summary>
    public float Radians 
    {
        get => radians;
        set 
        {
            radians = value;
            direction = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
    /// <summary>
    /// the Effects of the texture, handles the flips.
    /// </summary>
    public SpriteEffects Effects => effects;
    /// <summary>
    /// the Color Array for the texture.
    /// </summary>
    public Color[] Colors => colors;
    /// <summary>
    /// the Color for the Texture.
    /// </summary>
    public Color Color { get { return colors[0]; } set { colors[0] = value; } }
    /// <summary>
    /// the Red value from the Color property. (R)
    /// </summary>
    public float R { get { return Color.R; } set { Color = new(value, Color.G, Color.B, Color.A); } }
    /// <summary>
    /// the Green Value from the Color Property (G).
    /// </summary>
    public float G { get { return Color.G; } set { Color = new(Color.R, value, Color.B, Color.A); } }
    /// <summary>
    /// the Blue value from the Color property (B).
    /// </summary>
    public float B { get { return Color.B; } set { Color = new(Color.R, Color.B, value, Color.A); } }
    /// <summary>
    /// the transparency and the Opacity of the Texture and Color. (A)
    /// </summary>
    public float Opacity { get { return Color.A; } set { Color = new(Color.R, Color.G, Color.B, value); } }
    /// <summary>
    /// the Depth from a value of zero to one to envision 3D depth.
    /// </summary>
    public float LayerDepth { get { return depth; } set { depth = MathHelper.Clamp(value, 0f, 1f); } }
    /// <summary>
    /// the Main constructor for the Sprite class.
    /// </summary>
    /// <param name="texture">the pathh to the Texture or image you want to add.</param>
    /// <param name="selectedColor">the color you want to set for the Texture.</param>
    /// <exception cref="ArgumentException">if the Texture is null (or path is incorrect)</exception>
    public Sprite(Texture2D texture, Color selectedColor) 
    {
        if (texture == null) throw new ArgumentException("texture can not be null.");
        this.texture = texture;
        colors = new Color[texture.Width * texture.Height];
        Array.Fill(colors, selectedColor);
    }
    /// <summary>
    /// Flips the Texture horizontally, uses flag enum and has no return, but ca be set only.
    /// </summary>
    public bool Flip_H { set { effects = value ? effects |= SpriteEffects.FlipHorizontally : effects &= ~SpriteEffects.FlipHorizontally; } }
    /// <summary>
    /// flips the Texture vertically, uses flag enums and has no return, but can be set only.
    /// </summary>
    public bool Flip_V { set { effects = value ? effects |= SpriteEffects.FlipVertically : effects &= ~SpriteEffects.FlipVertically; } }
    /// <summary>
    /// a purge method to set the Texture back to normal direction.
    /// </summary>
    public void FlipBackToNormal() => effects = SpriteEffects.None;
    /// <summary>
    /// please be aware that this method should only be used if the Color Array is the length of 1! Color Arrays in this class are typically for debugging.
    /// </summary>
    public void SetToData() => texture.SetData<Color>(Colors);
    
    // sprite batch calls.
    public void Draw(SpriteBatch batch, Rectangle Destination, Rectangle Source) 
    {
        batch.Draw(Texture, Destination, Source, Color, Radians, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Rectangle Destination) 
    {
        batch.Draw(Texture, Destination, null, Color, Radians, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 Destination, Rectangle Source) 
    {
        batch.Draw(Texture, Destination, Source, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 Destination) 
    {
        batch.Draw(Texture, Destination, null, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    // extra overloads for convienance
}