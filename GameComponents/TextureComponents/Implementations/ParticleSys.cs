using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents;
namespace GameComponents.Rendering;

public class ParticleManager 
{
    // public properties
    public readonly Particle[] Particles;
    public delegate void ParticleBehaviour(ref Particle p, int index);
    // helper methods
    public int TotalCount => Particles.Length;
    
    public ParticleManager(int amount) 
    {
        if (int.IsNegative(amount)) throw new ArgumentException($"value of {amount} is invalid or is negative.");
        Particles = new Particle[amount];
    }
    
    public void Initialize(Func<Particle> initializer) 
    {
        for(int i = 0; i < Particles.Length; i++) Particles[i] = initializer();
    }
    
    public virtual void UpdateParticles(ParticleBehaviour particleBehaviour) 
    {
        for(int i = 0; i < Particles.Length; i++) 
        {
            if (!Particles[i].IsActive) continue;
            particleBehaviour(ref Particles[i], i);
        }
    }
    // draw methods
    public virtual void Draw(SpriteBatch batch, Texture2D texture, Vector2 origin) 
    {
        foreach(ref readonly Particle p in Particles.AsSpan()) 
        {
            batch.Draw(texture, p.Position, null, p.Color, p.Radians, origin, p.Scale, p.Effects, p.LayerDepth);
        }
    }
    
    public virtual void Draw(SpriteBatch batch, Texture2D texture, Rectangle sourceBounds, Vector2 origin) 
    {
        foreach(ref readonly Particle p in Particles.AsSpan()) 
        {
            batch.Draw(texture, p.Position, sourceBounds, p.Color, p.Radians, origin, p.Scale, p.Effects, p.LayerDepth);
        }
    }
    
    public virtual void Draw(SpriteBatch batch, Texture2D texture, Rectangle[] sourceRectangles, Vector2 origin) 
    {
        foreach(ref readonly Particle p in Particles.AsSpan()) 
        {
            batch.Draw(texture, p.Position, sourceRectangles[p.ID], p.Color, p.Radians, origin, p.Scale, p.Effects, p.LayerDepth);
        }
    }
}