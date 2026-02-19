namespace GameComponents.Storages;

public static class FastStackHelper<T> 
{
    public delegate void Loop(ref T item);
    
    internal static void ForEach(ref FastStack<T> collection, Loop loop) 
    {
        foreach(ref T item in collection.AsSpan()) loop(ref item);
    }
}