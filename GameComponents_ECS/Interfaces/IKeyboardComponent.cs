using Microsoft.Xna.Framework.Input;
namespace GameComponents;

public interface IKeyboardComponent 
{
    public bool IsKeyDown(Keys key);
    public bool IsKeyPressed(Keys key);
    public bool IsKeyReleased(Keys key);
}