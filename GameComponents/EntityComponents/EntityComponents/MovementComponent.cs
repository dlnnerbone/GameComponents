using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class MovementComponent : IMovementComponent 
{
    private Vector2 velocity = Vector2.Zero;
    // properties
    public Vector2 Velocity { get => velocity; set => velocity = value; }
    public Vector2 MovingDirection => Vector2.Normalize(Velocity);
    public ref Vector2 ReferencedVelocity => ref velocity;
    
    public float VeloX { get => velocity.X; set => velocity.X = value; }
    public float VeloY { get => velocity.Y; set => velocity.Y = value; }
    
    // methods
    public bool IsMovingRight() => Velocity.X > 0f;
    public bool IsMovingLeft() => Velocity.X < 0f;
    public bool IsMovingUp() => Velocity.Y < 0f;
    public bool IsMovingDown() => Velocity.Y > 0f;
    public bool IsMoving() => IsMovingRight() || IsMovingLeft() || IsMovingUp() || IsMovingDown();
     
    public MovementComponent(Vector2 initialVelocity) => velocity = initialVelocity;
    public static MovementComponent Zero => new MovementComponent(Vector2.Zero);
}