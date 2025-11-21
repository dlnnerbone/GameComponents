using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class MovementComponent : IMovementComponent, IDirection 
{
    protected Vector2 velocity = Vector2.Zero;
    protected Vector2 direction;
    // private fields
    public virtual Vector2 Velocity 
    {
        get => velocity;
        set 
        {
            velocity = value;
            direction = Vector2.Normalize(velocity);
        }
    }
    public virtual Vector2 Direction 
    {
        get => direction;
        set 
        {
            direction = Vector2.Normalize(value);
            var prevVelo = velocity.Length();
            velocity = direction * prevVelo;
        }
    }
    public float Velocity_X { get { return velocity.X; } set { velocity.X = value; } }
    public float Velocity_Y { get { return velocity.Y; } set { velocity.Y = value; } }
    public bool IsMovingLeft() => Velocity.X < 0;
    public bool IsMovingRight() => Velocity.X > 0;
    public bool IsMovingDown() => Velocity.Y > 0;
    public bool IsMovingUp() => Velocity.Y < 0;
    public bool IsMoving() => IsMovingLeft() || IsMovingRight() || IsMovingDown() || IsMovingUp();
    public MovementComponent() {}
    public MovementComponent(Vector2 vel) 
    {
        Velocity = vel;
    }
}