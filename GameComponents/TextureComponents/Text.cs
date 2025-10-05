using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Interfaces;
namespace GameComponents.Rendering;
public class SpriteText : ISpriteText 
{
    // for Sprite/Texture handling, directly not afflicated with the interface.
    private Vector2 direction = Vector2.UnitX;
    private Vector2 scale = Vector2.One;
    private Vector2 origin = Vector2.Zero;
    private float depth = 0;
    private SpriteEffects effects = SpriteEffects.None;
    // fields afflialted with the interface.
    private Vector2 textPos = Vector2.Zero;
    private Color textColor = Color.White;
    private string textDisplay = string.Empty;
    // public properties for Texture-based spritebatches
    /// <summary>
    /// the DIrection of the Text.
    /// </summary>
    public Vector2 Direction { get => direction; set => direction = Vector2.Normalize(value); }
    /// <summary>
    /// the Scale of the Text.
    /// </summary>
    public Vector2 Scale { get => scale; set => scale = new Vector2((float)Math.Abs(value.X), (float)Math.Abs(value.Y)); }
    /// <summary>
    /// the Origin point of the Text.
    /// </summary>
    public Vector2 Origin { get => origin; set => origin = value; }
    /// <summary>
    /// the Depth (Layered) of the Text to envision 3D-like depth (Z-Axis)
    /// </summary>
    public float LayerDepth { get => depth; set => depth = MathHelper.Clamp(value, 0f, 1f); }
    /// <summary>
    /// the Effects of the Texture, handles the flipping.
    /// </summary>
    public SpriteEffects Effects => effects;
    // public properties with the iSprite Text nterface
    
    /// <summary>
    /// the Position of the Text.
    /// </summary>
    public Vector2 Position { get => textPos; set => textPos = value; }
    /// <summary>
    /// the Color of the Text.
    /// </summary>
    public Color Color { get => textColor; set => textColor = value; }
    /// <summary>
    /// the back-handler for setting up the Font for the Text.
    /// </summary>
    public SpriteFont SpriteFont { get; set; }
    /// <summary>
    /// the text to be displayed.
    /// </summary>
    public string Text { get => textDisplay; set => textDisplay = value; }
    /// <summary>
    /// the Bounds of the String when rendered.
    /// </summary>
    public Rectangle Bounds 
    {
        get 
        {
            Vector2 size = SpriteFont.MeasureString(Text);
            return new Rectangle((int)Position.X, (int)Position.Y, (int)size.X, (int)size.Y);
        }
    }
    public bool Flip_H { set => effects = value ? effects |= SpriteEffects.FlipHorizontally : effects &= ~SpriteEffects.FlipHorizontally; }
    public bool Flip_V { set => effects = value ? effects |= SpriteEffects.FlipVertically : effects &= ~SpriteEffects.FlipVertically; }
    public void FlipBackToNormal() => effects = SpriteEffects.None;
    public float Radians 
    {
        get => (float)Math.Atan2(Direction.Y, Direction.X);
        set => Direction = new Vector2((float)Math.Cos(value), (float)Math.Sin(value));
    }
    // main constructor(s)
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        if (contentManager == null) throw new ArgumentNullException($"{nameof(contentManager)} does not exist or can't be null.");
        if (string.IsNullOrEmpty(pathToFont)) throw new ArgumentNullException($"{pathToFont} can not be empty or null, or can't be found.");
        if (textDisplay == null) throw new ArgumentNullException($"{nameof(textDisplay)} can not be null.");
        
        SpriteFont = contentManager.Load<SpriteFont>(pathToFont);
        Text = textDisplay;
        LayerDepth = depth;
        this.effects = effects;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, 
                    float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        if (contentManager == null) throw new ArgumentNullException($"{nameof(contentManager)} does not exist or can't be null.");
        if (string.IsNullOrEmpty(pathToFont)) throw new ArgumentNullException($"{pathToFont} can not be empty or null, or can't be found.");
        if (textDisplay == null) throw new ArgumentNullException($"{nameof(textDisplay)} can not be null.");

        SpriteFont = contentManager.Load<SpriteFont>(pathToFont);
        Text = textDisplay;
        LayerDepth = depth;
        this.effects = effects;
        Position = position;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        if (contentManager == null) throw new ArgumentNullException($"{nameof(contentManager)} does not exist or can't be null.");
        if (string.IsNullOrEmpty(pathToFont)) throw new ArgumentNullException($"{pathToFont} can not be empty or null, or can't be found.");
        if (textDisplay == null) throw new ArgumentNullException($"{nameof(textDisplay)} can not be null.");
        if (direction == Vector2.Zero) throw new ArgumentException("both values of direction can not be zero.");
        
        SpriteFont = contentManager.Load<SpriteFont>(pathToFont);
        Text = textDisplay;
        LayerDepth = depth;
        this.effects = effects;
        Position = position;
        Direction = direction;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    Vector2 origin, float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        if (contentManager == null) throw new ArgumentNullException($"{nameof(contentManager)} does not exist or can't be null.");
        if (string.IsNullOrEmpty(pathToFont)) throw new ArgumentNullException($"{pathToFont} can not be empty or null, or can't be found.");
        if (textDisplay == null) throw new ArgumentNullException($"{nameof(textDisplay)} can not be null.");
        if (direction == Vector2.Zero) throw new ArgumentException("both values of direction can not be zero.");
        
        SpriteFont = contentManager.Load<SpriteFont>(pathToFont);
        Text = textDisplay;
        LayerDepth = depth;
        this.effects = effects;
        Position = position;
        Direction = direction;
        Origin = origin;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    Vector2 origin, Color color, float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        if (contentManager == null) throw new ArgumentNullException($"{nameof(contentManager)} does not exist or can't be null.");
        if (string.IsNullOrEmpty(pathToFont)) throw new ArgumentNullException($"{pathToFont} can not be empty or null, or can't be found.");
        if (textDisplay == null) throw new ArgumentNullException($"{nameof(textDisplay)} can not be null.");
        if (direction == Vector2.Zero) throw new ArgumentException("both values of direction can not be zero.");
        
        SpriteFont = contentManager.Load<SpriteFont>(pathToFont);
        Text = textDisplay;
        LayerDepth = depth;
        this.effects = effects;
        Position = position;
        Direction = direction;
        Origin = origin;
        Color = color;
    }
    // main draw
    public void DrawString(SpriteBatch batch) 
    {
        batch.DrawString(SpriteFont, Text, Position, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
}