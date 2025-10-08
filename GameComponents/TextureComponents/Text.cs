using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents.Rendering;
public class SpriteText 
{
    // for Sprite/Texture handling, directly not afflicated with the interface.
    private Vector2 direction = Vector2.UnitX;
    private Vector2 scale = Vector2.One;
    private Vector2 origin = Vector2.Zero;
    private float depth = 0;
    private float radians = 0;
    private SpriteEffects effects = SpriteEffects.None;
    private SpriteFont spriteFont;
    // fields afflialted with the interface.
    private Vector2 textPos = Vector2.Zero;
    private Color textColor = Color.White;
    private string pathToFont = string.Empty;
    // public properties
    public Vector2 Direction 
    {
        get => direction;
        set 
        {
            direction = Vector2.Normalize(value);
            radians = (float)Math.Atan2(direction.Y, direction.X);
        }
    }
    public Vector2 Scale { get => scale; set => scale = new Vector2(Math.Abs(value.X), Math.Abs(value.Y)); }
    public Vector2 Origin { get => origin; set => origin = value; }
    public SpriteFont SpriteFont => spriteFont;
    public SpriteEffects Effects { get => effects; set => effects = value; }
    public Color Color { get => textColor; set => textColor = value; }
    public float LayerDepth { get => depth; set => depth = MathHelper.Clamp(value, 0f, 1f); }
    public float Radians 
    {
        get => radians;
        set 
        {
            radians = value;
            direction = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
    // text properties
    public Vector2 Position { get => textPos; set => textPos = value; }
    public string Path => pathToFont;
    // constructors
    public SpriteText(ContentManager content, string pathToFont, float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        if (content == null) throw new ArgumentNullException("content manager can not be null.");
        if (string.IsNullOrEmpty(pathToFont)) throw new ArgumentNullException("path to font can not be empty or null.");

        spriteFont = content.Load<SpriteFont>(pathToFont);
        this.pathToFont = pathToFont;
        LayerDepth = depth;
        Effects = effects;
    }
    public SpriteText(ContentManager content, string pathToFont, Vector2 direction, float depth = 0, SpriteEffects effects = SpriteEffects.None) : 
                        this(content, pathToFont, depth, effects) 
    {
        if (direction == Vector2.Zero) throw new ArgumentException($"{nameof(direction)} can not have both x-y values as zero.");
        Direction = direction;
    }
    public SpriteText(ContentManager content, string pathToFont, Vector2 direction, Vector2 scale, float depth = 0, SpriteEffects effects = SpriteEffects.None) : 
                        this(content, pathToFont, direction, depth, effects) 
    {
        Scale = scale;
    }
    public SpriteText(ContentManager content, string pathToFont, Vector2 direction, Vector2 scale, Vector2 origin, float depth = 0, SpriteEffects effects = SpriteEffects.None) : 
                        this(content, pathToFont, direction, scale, depth, effects) 
    {
        Origin = origin;
    }
    public SpriteText(ContentManager content, string pathToFont, Vector2 direction, Vector2 scale, Vector2 origin, 
                        Vector2 position, float depth = 0, SpriteEffects effects = SpriteEffects.None) : this(content, pathToFont, direction, scale, 
                        origin, depth, effects) 
    {
        Position = position;
    }
    public SpriteText(ContentManager content, string pathToFont, Vector2 direction, Vector2 scale, Vector2 origin, 
                        Vector2 position, Color color, float depth = 0, SpriteEffects effects = SpriteEffects.None) : this(content, pathToFont, direction, scale, 
                        origin, position, depth, effects) 
    {
        Color = color;
    }
    // main draw method
    public void DrawString(SpriteBatch batch, string textToDisplay) 
    {
        batch.DrawString(SpriteFont, textToDisplay, Position, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    
}