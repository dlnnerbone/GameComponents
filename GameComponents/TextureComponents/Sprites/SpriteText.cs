using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Rendering;
public class SpriteText : TextureDependencies
{
    public SpriteFont SpriteFont;
    public Vector2 Position { get; set; } = Vector2.Zero;
    public SpriteText(SpriteFont spriteFont) 
    {
        ArgumentNullException.ThrowIfNull(spriteFont, $"{nameof(spriteFont)} is not found or is null.");
        SpriteFont = spriteFont;
    }
    public SpriteText(SpriteFont spriteFont, Vector2 position) : this(spriteFont) => Position = position;
    public SpriteText(SpriteFont spriteFont, Vector2 position, Vector2 direction) : this(spriteFont, position) => Direction = direction;
    // draw calls
    public void DrawString(SpriteBatch batch, string textDisplay) 
    {
        batch.DrawString(SpriteFont, textDisplay, Position, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    public void DrawString(SpriteBatch batch, string textDisplay, float angle) 
    {
        batch.DrawString(SpriteFont, textDisplay, Position, Color, angle, Origin, Scale, Effects, LayerDepth);
    }
}