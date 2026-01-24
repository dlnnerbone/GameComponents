using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using System.Linq;
namespace GameComponents.Systems;

//a class that holds a assortment of Types (not objects, only classification)

public partial record Archetype 
{
    private readonly string collectedTypes;

    // a hashset for better lookups.
    public readonly HashSet<Type> ComponentTypes;
    // data
    public readonly int TypeCount;
    public override string ToString() => collectedTypes;
    
    public Type? Get<T>() 
    {
        if (ComponentTypes.Contains(typeof(T))) return typeof(T);
        return default!;
    }
    
    public bool Has<T>() => ComponentTypes.Contains(typeof(T));
    
    public Type[] GetRange(IEnumerable<Type> selectedTypes) 
    {
        if (selectedTypes == null) return Array.Empty<Type>();
        
        return selectedTypes.Distinct().Where(ComponentTypes.Contains).ToArray();
    }
    
    public bool HasAnyOf(IEnumerable<Type> types) 
    {
        if (types == null) return false;
        
        return types.Any(ComponentTypes.Contains);
    }
    
    public bool HasAllOf(IEnumerable<Type> types) 
    {
        if (types == null) return false;
        
        return types.All(ComponentTypes.Contains);
    }
}