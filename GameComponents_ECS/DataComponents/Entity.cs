using Microsoft.Xna.Framework;
namespace GameComponents;

public record struct Entity 
{
    public readonly int Id;
    public Entity(int id) => Id = id;
    
    public static implicit operator Entity(int id) => new Entity(id);
    public static implicit operator byte(Entity entity) => (byte)entity.Id;
    public static implicit operator int(Entity entity) => entity.Id;
    
    public override string ToString() => $"Entity: {Id}";
    
    public static Entity Null => new Entity(-1);
    public static Entity Create(int id) => new Entity(id);
}