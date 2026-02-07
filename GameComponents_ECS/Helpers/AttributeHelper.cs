using GameComponents.Storages;
using GameComponents.Systems;

namespace GameComponents.Helpers;
public static class AttributeHelper 
{
    internal static readonly FastStack<Attribute[]> _cachedEntries = new FastStack<Attribute[]>(16);
    
    public static bool Contains<TAttr>(Type type) where TAttr : Attribute 
    {
        int attrID = AttributeID<TAttr>.ID;
        
        return false;
    }
}

internal static class AttributeMetadata { internal static int HighestID = 0; }

internal static class AttributeID<TAttr> where TAttr : Attribute 
{
    internal static readonly int ID = AttributeMetadata.HighestID++;
}