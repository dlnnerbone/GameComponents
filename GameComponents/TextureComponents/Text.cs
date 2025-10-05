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
    // main constructor(s)
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        SpriteFont = contentManager.Load<SpriteFont>(pathToFont);
        Text = textDisplay;
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, 
                    float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    Vector2 origin, float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        
    }
    public SpriteText(ContentManager contentManager, string pathToFont, string textDisplay, Vector2 position, Vector2 direction,
                    Vector2 origin, Color color, float depth = 0, SpriteEffects effects = SpriteEffects.None) 
    {
        
    }
}