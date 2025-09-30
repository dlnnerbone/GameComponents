using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents.Interfaces;
public interface ICore 
{
    // checkers
    public ContentManager Content { get; set; }
    public GraphicsDeviceManager Device { get; set; }
    public GraphicsDevice GraphicsDevice { get; set; }
    public SpriteBatch SpriteBatch { get; set; }
}