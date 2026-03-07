using GameComponents.Systems;
namespace GameComponents;

// this is a class designed to have more control over the movement of entities, though its not recommended.
public static class EntityControl
{
    public static void SetPosition(ref Entity entity, ushort newPos) => entity.SetPosition(newPos);
    public static void SetArchetype(ref Entity entity, ushort occupation) => entity.SetArchetype(occupation);
    public static void SetArchetype(ref Entity entity, Archetype archetype) => entity.SetArchetype(archetype);
}