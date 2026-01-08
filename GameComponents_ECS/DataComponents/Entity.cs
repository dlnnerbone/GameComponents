using Microsoft.Xna.Framework;
namespace GameComponents;

public record struct Entity 
{
    public readonly int Id;
    public Entity(int id) => Id = id;
    
    public static implicit operator Entity(int id) 
    {
        return new Entity(id);
    }
    
    public override string ToString() => $"Entity: {Id}";
    
    public static Entity Null => new Entity(-1);
}