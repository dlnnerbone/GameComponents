using System;

public interface IDrawable 
{
    void Draw();
}

public interface IDrawable<TRenderer> where TRenderer : class 
{
    void Draw(TRenderer renderer);
}