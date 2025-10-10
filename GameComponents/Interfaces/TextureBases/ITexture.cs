using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Interfaces;
public interface ITexture
{
    Color Color { get; set; }
    Vector2 Scale { get; set; }
    Vector2 Origin { get; set; }
    Vector2 Direction { get; set; }
    SpriteEffects Effects { get; set; }
    float Radians { get; set; }
    float LayerDepth { get; set; }
    
}