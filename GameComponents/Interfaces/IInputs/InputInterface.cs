
using Microsoft.Xna.Framework.Input;
namespace GameComponents.Interfaces;
public interface IInputs 
{
    KeyboardState CurrentKeyboardState { get; protected set; }
    KeyboardState PreviousKeyboardState { get; protected set; }
    bool IsKeyDown(Keys key);
    bool IsKeyPressed(Keys key);
    bool IsKeyReleased(Keys key);
}