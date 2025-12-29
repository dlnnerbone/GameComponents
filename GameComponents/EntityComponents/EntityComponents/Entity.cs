using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public abstract class Entity : BodyComponent, IHealthComponent, IMovementComponent, IBodyComponent
{
    protected HealthComponent ProtectedHealth;
    protected MovementComponent ProtectedMovement;
    
    protected abstract void MoveAndSlide(GameTime gameTime);
    // main stuff
    // main health properties
    public float Health { get => ProtectedHealth.Health; set => ProtectedHealth.Health = value; }
    public float MinHealth { get => ProtectedHealth.MinHealth; set => ProtectedHealth.MinHealth = value; }
    public float MaxHealth { get => ProtectedHealth.MaxHealth; set => ProtectedHealth.MaxHealth = value; }
    
    public ref float ReferencedHealth => ref ProtectedHealth.ReferencedHealth;
    public ref float ReferencedMinHealth => ref ProtectedHealth.ReferencedMinHealth;
    public ref float ReferencedMaxHealth => ref ProtectedHealth.ReferencedMaxHealth;
    
    public float NormalizedHP => ProtectedHealth.NormalizedHP;
    public bool IsHealthWithinThreshold(float range) => ProtectedHealth.IsWithinThreshold(range);
    public void Terminate() => ProtectedHealth.Terminate();
    // main Movement properties
    public Vector2 Velocity { get => ProtectedMovement.Velocity; set => ProtectedMovement.Velocity = value; }
    public Vector2 MovingDirection => ProtectedMovement.MovingDirection;
    public ref Vector2 ReferencedVelocity => ref ProtectedMovement.ReferencedVelocity;
    
    public float VeloX { get => ProtectedMovement.VeloX; set => ProtectedMovement.VeloX = value; }
    public float VeloY { get => ProtectedMovement.VeloY; set => ProtectedMovement.VeloY = value; }
    
    public bool IsMoving() => ProtectedMovement.IsMoving();
    public bool IsMovingLeft() => ProtectedMovement.IsMovingLeft();
    public bool IsMovingRight() => ProtectedMovement.IsMovingRight();
    public bool IsMovingUp() => ProtectedMovement.IsMovingUp();
    public bool IsMovingDown() => ProtectedMovement.IsMovingDown();
    
    // constructors
    public Entity(int x, int y, int width, int height, float health, float maxHealth, float minHealth = 0f) : base(x, y, width, height) 
    {
        ProtectedHealth = new(health, minHealth, maxHealth);
        ProtectedMovement = MovementComponent.Zero;
    }
    
    public Entity(int x, int y, int width, int height, Vector2 initialVelocity, float health, float maxHealth, float minHealth = 0f) : base(x, y, width, height)
    {
        ProtectedHealth = new(health, minHealth, maxHealth);
        ProtectedMovement = new(initialVelocity);
    }
    
    public Entity(Point location, Point size, float health, float maxHealth, float minHealth = 0f) : base(location, size) 
    {
        ProtectedHealth = new(health, minHealth, maxHealth);
        ProtectedMovement = MovementComponent.Zero;
    }
    
    public Entity(Point location, Point size, Vector2 initialVelocity, float health, float maxHealth, float minHealth = 0f) : base(location, size) 
    {
        ProtectedHealth = new(health, minHealth, maxHealth);
        ProtectedMovement = new(initialVelocity);
    }
    
}