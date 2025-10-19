using Microsoft.Xna.Framework;
using GameComponents;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public abstract class Projectile : BodyComponent, IDirection
{
    private Vector2 direction = Vector2.UnitX;
    private Actions actionStates;
    private float radians = 0;
    // private fields
    public Vector2 Origin => Position;
    public Vector2 Direction 
    {
        get => direction;
        set 
        {
            direction = value != Vector2.Zero ? Vector2.Normalize(value) : Vector2.UnitX;
            radians = (float)Math.Atan2(direction.Y, direction.X);
        }
    }
    public Actions ActionStates { get => actionStates; protected set => actionStates = value; }
    public float Radians 
    {
        get => radians;
        set 
        {
            radians = value;
            direction = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
    // bool helpers
    public bool IsDead => actionStates == Actions.Disabled;
    public bool IsReady => (actionStates & Actions.Ready) == Actions.Ready;
    public bool IsCharging => (actionStates & Actions.Charging) == Actions.Charging;
    public bool IsActive => (actionStates & Actions.Active) == Actions.Active;
    public bool IsInterrupted => (actionStates & Actions.Interrupted) == Actions.Interrupted;
    public bool InRecovery => (actionStates & Actions.Cooldown) == Actions.Cooldown;
    public bool HasCompleted => (actionStates & Actions.Completed) == Actions.Completed;
    // flag adding or removing, overriding or disabling methods.
    public void AddFlag(Actions newAction) => actionStates |= newAction;
    public void RemoveFlag(Actions flag) => actionStates &= ~flag;
    public void OverrideFlags(Actions flagGroup) => actionStates = flagGroup;
    public void PurgeFlags() => actionStates = Actions.Disabled;
    // methods
    public void AimAt(Vector2 target) => Direction = target - Origin;
    public void AimAt(Point target) => Direction = new Vector2(target.X, target.Y) - Origin;

    public void SetDirection(Vector2 direction) => Direction = direction;
    public void SetDirection(Point direction) => Direction = new Vector2(direction.X, direction.Y);
    // main update methods
    public virtual void ShootingTime() 
    {
        if (IsDead) return;
    }
    // main constructors
    protected Projectile(int x, int y, int width, int height, Vector2 dir) : base(x, y, width, height) 
    {
        validateDirection(dir);
        direction = dir;
    }
    protected Projectile(Point location, Point size, Vector2 dir) : base(location, size) 
    {
        validateDirection(dir);
        direction = dir;
    }
    protected Projectile(Vector2 location, Vector2 size, Vector2 dir) : base(location, size) 
    {
        validateDirection(dir);
        direction = dir;
    }
    protected Projectile(Vector4 VectorModel, Vector2 dir) : base(VectorModel) 
    {
        validateDirection(dir);
        direction = dir;
    }
    protected Projectile(Rectangle bounds, Vector2 dir) : base(bounds) 
    {
        validateDirection(dir);
        direction = dir;
    }
    // private methods
    private void validateDirection(Vector2 dir) 
    {
        if (dir == Vector2.Zero) throw new ArgumentException($"direction argument in {this} is not valid (can not be set as zero). ");
    }
}