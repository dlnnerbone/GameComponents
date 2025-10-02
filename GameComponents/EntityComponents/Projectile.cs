using Microsoft.Xna.Framework;
using GameComponents;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public abstract class Projectile : BodyComponent, IDirection
{
    private Vector2 direction = Vector2.UnitX;
    private Actions actionStates;
    // private fields
    public Vector2 Origin => Position;
    public Vector2 Direction { get => direction; set => direction = Vector2.Normalize(value); }
    public Actions ActionStates { get => actionStates; protected set => actionStates = value; }
    public float Radians 
    {
        get => (float)Math.Atan2(Direction.Y, Direction.X);
        set => Direction = new Vector2((float)Math.Cos(value), (float)Math.Sin(value));
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
}