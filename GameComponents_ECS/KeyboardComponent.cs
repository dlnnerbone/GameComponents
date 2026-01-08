using Microsoft.Xna.Framework.Input;

namespace GameComponents;

public class KeyboardStateComponent 
{
    public KeyboardState CurrentKeyboardState;
}

public class PreviousKeyboardStateComponent 
{
    public KeyboardState PreviousKeyboardState;
}

public interface IKeyboardComponent 
{
    public bool IsKeyDown(Keys key);
    public bool IsKeyPressed(Keys key);
    public bool IsKeyReleased(Keys key);
}