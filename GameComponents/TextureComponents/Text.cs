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
    private float radians = 0;
    private SpriteEffects effects = SpriteEffects.None;
    // fields afflialted with the interface.
    private Vector2 textPos = Vector2.Zero;
    private Color textColor = Color.White;
    private string textDisplay = string.Empty;
    // public properties for Texture-based spritebatches
    /// <summary>
    /// the DIrection of the Text.
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
    /// gets the Radians of the direction, setter changes direction.
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
    public string Text { get => textDisplay; set => textDisplay = value ?? string.Empty; }
    /// <summary>
    /// the Bounds of the String when rendered.
    /// </summary>
    public Rectangle Bounds 
    {
        get 
        {
            Vector2 size = SpriteFont.MeasureString(Text) * Scale;
            return new Rectangle((int)Position.X, (int)Position.Y, (int)size.X, (int)size.Y);
        }
    }
    /// <summary>
    /// flips the text horizontally if true, otherwise stays the same.
    /// </summary>
    public bool Flip_H { set => effects = value ? effects |= SpriteEffects.FlipHorizontally : effects &= ~SpriteEffects.FlipHorizontally; }
    /// <summary>
    /// flips the text vertically if true, otherwise it stays the same.
    /// </summary>
    public bool Flip_V { set => effects = value ? effects |= SpriteEffects.FlipVertically : effects &= ~SpriteEffects.FlipVertically; }
    /// <summary>
    /// a purge flag to flip all effects of the Texture to normal.
    /// </summary>
    public void FlipBackToNormal() => effects = SpriteEffects.None;
    // main constructor(s)
    
    /// <summary>
    /// the constructor to set up the class.
    /// </summary>
    /// <param name="contentManager">the path to set up the font of the text.</param>
    /// <param name="pathToFont">the path to the font file, can not be Empty or Null.</param>
    /// <param name="textDisplay">the text to be displayed and rendered on screen.</param>
    /// <param name="depth">the Layerepth from a value of 0 to 1 for how 'close' the text is to the screen.</param>
    /// <param name="effects">the SpriteEffects for flipping, default as None.</param>
    /// <exception cref="ArgumentNullException"></exception>
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
                    float depth = 0, SpriteEffects effects = SpriteEffects.None) : this(contentManager, pathToFont, textDisplay, depth, effects)
    {
        Position = position;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    float depth = 0, SpriteEffects effects = SpriteEffects.None) : this(contentManager, pathToFont, textDisplay, position, depth, effects)
    {
        if (direction == Vector2.Zero) throw new ArgumentException("direction can not have both values as zero.");
        Direction = direction;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    Vector2 origin, float depth = 0, SpriteEffects effects = SpriteEffects.None) : this(contentManager, pathToFont, textDisplay, position,
                    direction, depth, effects)
    {
        Origin = origin;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    Vector2 origin, Color color, float depth = 0, SpriteEffects effects = SpriteEffects.None) : this(contentManager, pathToFont, textDisplay,
                    position, direction, origin, depth, effects)
    {
        Color = color;
    }
    // main draw
    public void DrawString(SpriteBatch batch) 
    {
        batch.DrawString(SpriteFont, Text, Position, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
}