namespace GameComponents.Storages;

public static class FastStackHelper<T> 
{
    public delegate void Loop(ref T item);
    
    internal static void ForEach(Span<T> span, Loop loop) 
    {
        foreach(ref T item in span) 
        {
            loop(ref item);
        }
    }
}