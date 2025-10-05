using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Interfaces;

public interface ISpriteText
{
    public Vector2 Position { get; set; }
    public Color Color { get; set; }
    public SpriteFont SpriteFont { get; set; }
    public string Text { get; set; }
}