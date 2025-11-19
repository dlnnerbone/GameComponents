using Microsoft.Xna.Framework;
namespace GameComponents.Interfaces;
public interface IMovementComponent 
{
    Vector2 Velocity { get; set; }
    bool IsMoving();
    bool IsMovingDown();
    bool IsMovingUp();
    bool IsMovingLeft();
    bool IsMovingRight();
}