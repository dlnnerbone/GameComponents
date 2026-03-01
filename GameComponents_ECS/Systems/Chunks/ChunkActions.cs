namespace GameComponents.Systems;

public static class ChunkActions
{
    public static Chunk Create(uint capacity, Type type) => new Chunk(capacity, type);
    public static void Expand(Chunk chunk) => chunk.Expand();
    public static void ExpandBy(Chunk chunk, int amount) => chunk.ExpandBy(amount);
    public static void CompactTo(Chunk chunk, int index) => chunk.CompactTo(index);
}