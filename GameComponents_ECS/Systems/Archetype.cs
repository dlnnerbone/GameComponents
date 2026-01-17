using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using System.Linq;
namespace GameComponents.Systems;

//a class that holds a assortment of Types (not objects, only classification)

public record Archetype
{
    // an unordered, hashset to prevent dupes.
    public readonly HashSet<Type> Types;
    // the Count of how many Types are in the Archetype.
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
        
        // finds the distinct, filters the selected components, and converts it into an array.
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
        
        // checks if every element within the selected types is inside the Archetype, if any of them return false, the method returns false.
        var hasAll = selectedTypes.All(element => Types.Contains(element));
        return hasAll;
    }
    
    public bool HasAnyOf(IEnumerable<Type> selectedTypes) 
    {
        if (selectedTypes == null) return false;
        
        // checks if ANY of the elements inside the selected types is inside the ARchetype, returns true if any of them return true.
        var hasAny = selectedTypes.Any(element => Types.Contains(element));
        return hasAny;
    }
    
    public ImmutableHashSet<Type> ToImmutable() 
    {
        return Types.ToImmutableHashSet();
    }
    
    public Type[] AsArray() => Types.ToArray();
    
    public static Archetype Empty => new Archetype(Array.Empty<Type>().ToHashSet());
    
    // to string
    
    public override string ToString() 
    {
        var str = string.Empty;
        var arr = AsArray();
        
        for(int i = 0; i < Count; i++) 
        {
            str += $"{arr[i]}, ";
        }
        
        return str;
    }
}