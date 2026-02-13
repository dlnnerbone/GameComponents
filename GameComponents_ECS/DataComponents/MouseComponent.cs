using Microsoft.Xna.Framework.Input;
namespace GameComponents;

public class CurrentMouseStateComponent 
{
    public MouseState CurrentMouseState;
}

public class PreviousMouseStateComponent 
{
    public MouseState PreviousMouseState;
}

public struct ScrollWheelComponent 
{
    public int ScrollWheelValue;
    public int ScrollWheelDelta;
}

// flag enums for Mouse clicks
[Flags]
public enum MouseStates : ushort 
{
    NoInput = 0,
    LeftClick = 1,
    RightClick = 2,
    MiddleClick = 4,
    BackClick = 8,
    FrontClick = 16,
    
    LeftHold = 32,
    RightHold = 64,
    MiddleHold = 128,
    BackHold = 256,
    FrontHold = 512
}

