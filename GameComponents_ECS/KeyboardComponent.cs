using Microsoft.Xna.Framework.Input;

namespace GameComponents;

public struct KeyboardStateComponent 
{
    public KeyboardState CurrentKeyboardState;
    public KeyboardState PreviousKeyBoardState;
}

public interface IKeyboardComponent 
{
    public bool IsKeyDown(Keys key);
    public bool IsKeyPressed(Keys key);
    public bool IsKeyReleased(Keys key);
}