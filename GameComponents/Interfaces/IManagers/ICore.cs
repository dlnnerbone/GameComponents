using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents.Interfaces;
public interface ICore 
{
    // checkers
    public ContentManager Content { get; private set; }
    public GraphicsDeviceManager Device { get; private set; }
    public GraphicsDevice GraphicsDevice { get; private set; }
    public SpriteBatch SpriteBatch { get; private set; }
    protected override void Initialize();
    protected override void LoadContent();
    protected override void UnloadContent();
    protected override void Update(GameTime gt);
    protected override void Draw(GameTime gt);
}