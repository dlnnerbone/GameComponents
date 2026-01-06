using Microsoft.Xna.Framework;
namespace GameComponents;

public record struct Entity 
{
    public int Id;
    public Entity(int id) => Id = id;
}