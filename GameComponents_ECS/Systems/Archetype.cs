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
    
    /// <summary>
    /// a method that takes a generic and attempts to find it within the Archetype.
    /// </summary>
    /// <typeparam name="T">the component to search and return within the HashSet, returns null if it doesn't exist.</typeparam>
    /// <returns></returns>
    public Type? Get<T>() 
    {
        if (Types.Contains(typeof(T))) return typeof(T);
        return null;
    }
    
    /// <summary>
    /// a method to search for a range of Types into a one-dimensional array.
    /// </summary>
    /// <param name="selectedTypes">the types to filter for.</param>
    /// <returns></returns>
    public Type[] GetRange(IEnumerable<Type> selectedTypes)
    {
        if (selectedTypes == null) return Array.Empty<Type>();
        
        // finds the distinct, filters the selected components, and converts it into an array.
        var newList = selectedTypes.Distinct().Where(Types.Contains).ToArray();
        return newList;
    }
    
    /// <summary>
    /// a method that uses a Generic to check if the HashSet contains that type.
    /// </summary>
    /// <typeparam name="TComponent">The Component Type</typeparam>
    /// <returns>returns a boolean returning true if it exists inside the ARchetype, false if otherwise.</returns>
    public bool Has<TComponent>() 
    {
        if (Types.Contains(typeof(TComponent))) return true;
        return false;
    }
    
    /// <summary>
    /// a method that checks if all selected elements exist within the ARchetype.
    /// </summary>
    /// <param name="selectedTypes">the selected components.</param>
    /// <returns>returns true if EVERY chosen type exists inside the archetype, otherwise if any of them are false, method returns as false.</returns>
    public bool HasAllOf(IEnumerable<Type> selectedTypes) 
    {
        if (selectedTypes == null) return false;
        
        // checks if every element within the selected types is inside the Archetype, if any of them return false, the method returns false.
        var hasAll = selectedTypes.All(element => Types.Contains(element));
        return hasAll;
    }
    
    /// <summary>
    /// a method that checks if ANY of the types inside selected exist inside the Archetype.
    /// </summary>
    /// <param name="selectedTypes">the selected component types.</param>
    /// <returns>returns true if any of the selected types exist inside the Archetype.</returns>
    public bool HasAnyOf(IEnumerable<Type> selectedTypes) 
    {
        if (selectedTypes == null) return false;
        
        // checks if ANY of the elements inside the selected types is inside the ARchetype, returns true if any of them return true.
        var hasAny = selectedTypes.Any(element => Types.Contains(element));
        return hasAny;
    }
    
    // methods for arrays
    
    /// <summary>
    /// converts the HashSet into an immutable HashSet.
    /// </summary>
    /// <returns>returns a immutable HashSet.</returns>
    public ImmutableHashSet<Type> ToImmutable() 
    {
        return Types.ToImmutableHashSet();
    }
    
    /// <summary>
    /// converts the HashSet into an array.
    /// </summary>
    /// <returns>returns a one-dimensional array of Types.</returns>
    public Type[] AsArray() => Types.ToArray();
    
    /// <summary>
    /// a static Archetype that returns an archetype without any Types, empty.
    /// </summary>
    public static Archetype Empty => new Archetype(Array.Empty<Type>().ToHashSet());
}