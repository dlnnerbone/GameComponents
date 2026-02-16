using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using System.Linq;
namespace GameComponents.Systems;

//a class that holds a assortment of Types (not objects, only classification)

public partial record Archetype 
{
    
    public bool Has<T>() 
    {
        int compID = ComponentID<T>.ID;
        
        if (compID > _bits.Length - 1) return false;
        return _bits[compID];
    }
    
    public Type? Get<T>() 
    {
        Type type = typeof(T);
        if (FoundTypes.Contains(type)) return type;
        return default!;
    }
    
    public bool ContainsAll(IEnumerable<Type> types) 
    {
        if (types.ToArray().Length == 0) return false;
        
        return types.All(FoundTypes.Contains);
    }
    
    public bool ContainsAny(IEnumerable<Type> types) 
    {
        var toArr = types.ToArray();
        if (toArr.Length == 0) return false;
        
        for(int i = 0; i < toArr.Length; i++) 
        {
            int compID = ComponentDictionary.GetID(toArr[i]);
           
            if (compID > _bits.Length - 1) return false; 
            else if (_bits[compID] == true) return true;
        }
        
        return false;
    }
}