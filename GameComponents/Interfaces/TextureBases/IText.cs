using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Interfaces;

public interface ISpriteText
{
    public Vector2 TextPosition { get; set; }
    public Color TextColor { get; set; }
    public SpriteFont SpriteFont { get; set; }
    public string Text { get; set; }
}