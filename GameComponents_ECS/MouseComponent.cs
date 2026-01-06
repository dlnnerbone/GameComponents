using Microsoft.Xna.Framework.Input;
namespace GameComponents.Input;

public struct MouseStateComponent
{
    public MouseState CurrentMouseState;
    public MouseState PreviousMouseState;
}

public struct MouseClickingComponent
{
    public bool IsLeftClicked;
    public bool IsRightClicked;
    public bool IsMiddleClicked;
    public bool IsBackClicked;
    public bool IsFrontClicked;
}

public struct MouseHoldingComponent 
{
    public bool IsLeftHeld;
    public bool IsRightHeld;
    public bool IsMiddleHeld;
    public bool IsBackHeld;
    public bool IsFrontHeld;
}

public struct MouseScrollWheelComponent
{
    public int ScrollWheelDelta;
    public int ScrollWheelValue;
}