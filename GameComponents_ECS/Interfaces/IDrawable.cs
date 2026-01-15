namespace GameComponents;
using Microsoft.Xna.Framework.Graphics;

public interface IDrawable 
{
    void Draw();
}

public interface IDrawable<TRenderer> where TRenderer : SpriteBatch 
{
    void Draw(TRenderer renderer);
}