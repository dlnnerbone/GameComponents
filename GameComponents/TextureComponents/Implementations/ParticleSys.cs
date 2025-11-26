using Microsoft.Xna.Framework;
using GameComponents;
namespace GameComponents.Rendering;

[Flags]
public enum ParticleStates 
{
    None = 0,
    All = 1,
    Vertical = 2,
    Horizontal = 4,
    Diagonal = 8
}

public class ParticleManager 
{
    private Vector2 location;
    private float minimumRange, maxRange;
}