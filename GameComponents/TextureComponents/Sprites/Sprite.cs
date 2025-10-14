using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Interfaces;
using System;
namespace GameComponents.Rendering;
public class Sprite : TextureDependencies 
{
    public Texture2D Texture { get; set; }
    public Rectangle Bounds => Texture.Bounds;
    public Sprite(Texture2D texture)
    {
        Texture = texture;
    }
    public Sprite(Texture2D texture, Color color) 
    {
        Texture = texture;
        Color = color;
    }
    // draw calls
    public void Draw(SpriteBatch batch, Rectangle destinationBounds, Rectangle sourceRegion) 
    {
        batch.Draw(Texture, destinationBounds, sourceRegion, Color, Radians, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Rectangle destinationBounds) 
    {
        batch.Draw(Texture, destinationBounds, null, Color, Radians, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 destination, Rectangle sourceRegion) 
    {
        batch.Draw(Texture, destination, sourceRegion, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 destination) 
    {
        batch.Draw(Texture, destination, null, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    // extra overloads to allow other arguments from other classes.
    public void Draw(SpriteBatch batch, Rectangle destinationBounds, Rectangle sourceRegion, float angle) 
    {
        batch.Draw(Texture, destinationBounds, sourceRegion, Color, angle, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Rectangle destinationBounds, float angle) 
    {
        batch.Draw(Texture, destinationBounds, null, Color, angle, Origin, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 destination, Rectangle sourceRegion, float angle) 
    {
        batch.Draw(Texture, destination, sourceRegion, Color, angle, Origin, Scale, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch batch, Vector2 destination, float angle) 
    {
        batch.Draw(Texture, destination, null, Color, angle, Origin, Scale, Effects, LayerDepth);
    }
}