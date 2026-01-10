using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using System.Linq;
namespace GameComponents;

//a class that holds a assortment of Types (not objects, only classification)

public record Archetype
{
    public readonly HashSet<Type> Types;
    public readonly int Count;
    
    public Archetype(HashSet<Type> selectedTypes) 
    {
        Types = selectedTypes;
        Count = Types.Count;
    }
    
    public Type? Get<T>() 
    {
        if (Types.Contains(typeof(T))) return typeof(T);
        return null;
    }
    
    public Type[] GetRange(IEnumerable<Type> selectedTypes)
    {
        if (selectedTypes == null) return Array.Empty<Type>();
        var newList = selectedTypes.Distinct().Where(Types.Contains).ToArray();
        return newList;
    }
    
    public bool Has<TComponent>() 
    {
        if (Types.Contains(typeof(TComponent))) return true;
        return false;
    }
    
    public bool HasAllOf(IEnumerable<Type> selectedTypes) 
    {
        if (selectedTypes == null) return false;
        
        var hasAll = selectedTypes.All(element => Types.Contains(element));
        return hasAll;
    }
    
    public bool HasAnyOf(IEnumerable<Type> selectedTypes) 
    {
        if (selectedTypes == null) return false;
        
        var hasAny = selectedTypes.Any(element => Types.Contains(element));
        return hasAny;
    }
    
    // methods for arrays
    public ImmutableHashSet<Type> ToImmutable() 
    {
        return Types.ToImmutableHashSet();
    }
    
    public Type[] AsArray() => Types.ToArray();
    
    public static Archetype Empty => new Archetype(Array.Empty<Type>().ToHashSet());
}