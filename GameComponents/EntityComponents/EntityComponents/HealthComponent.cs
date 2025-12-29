using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class HealthComponent : IHealthComponent 
{
    protected float ProtectedHealth = 0;
    protected float ProtectedMinHealth = 0;
    protected float ProtectedMaxHealth = 0;
    // private fields
    public float Health { get => ProtectedHealth; set => ProtectedHealth = MathHelper.Clamp(value, ProtectedMinHealth, ProtectedMaxHealth); }
    public float MinHealth { get => ProtectedMinHealth; set => ProtectedMinHealth = MathHelper.Clamp(value, 0f, ProtectedMaxHealth); }
    public float MaxHealth { get => ProtectedMaxHealth; set => ProtectedMaxHealth = MathHelper.Clamp(value, MinHealth, float.PositiveInfinity); }
    
    public ref float ReferencedHealth => ref ProtectedHealth;
    public ref float ReferencedMinHealth => ref ProtectedMinHealth;
    public ref float ReferencedMaxHealth => ref ProtectedMaxHealth;
    
    // methods
    
    public float NormalizedHP => (Health - MinHealth) / (MaxHealth - MinHealth);
    
    public bool IsWithinThreshold(float range) => Health < range;
    public virtual void Terminate() => Health = MinHealth;
    
    // constructors
    public HealthComponent(float health, float minHealth, float maxHealth) 
    {
        Health = health;
        MinHealth = minHealth;
        MaxHealth = maxHealth;
    }
}