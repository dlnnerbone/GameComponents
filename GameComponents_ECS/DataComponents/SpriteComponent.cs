using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents;

public class SpriteComponent 
{
    public Texture2D Sprite;
    
    public SpriteComponent(Texture2D texture) => Sprite = texture;
}

public struct ColorComponent 
{
    public Color Color;
}

public struct OriginComponent 
{
    public Vector2 Origin;
}

public struct RotationComponent 
{
    public float Radians;
}

public struct LayerDepthComponent 
{
    public float LayerDepth;
}

public struct ScaleComponent 
{
    public Vector2 Scale;
}

public struct SpriteEffectComponent 
{
    public SpriteEffects SpriteEffects;
}

public struct SourcePositionCOmponent 
{
    public Vector2 SourcePosition;
}