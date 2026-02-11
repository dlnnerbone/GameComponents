using GameComponents.Storages;

namespace GameComponents.Helpers;
public static class AttributeHelper 
{
    internal static readonly FastStack<object[]> _cachedEntries = new FastStack<object[]>(16);
    
    public static bool Contains<TAttr>(Type type) where TAttr : Attribute 
    {
        int attrID = AttributeID<TAttr>.ID;
        
        if (!AttributeID<TAttr>._justChecked) 
        {
            _cachedEntries.Push(type.GetCustomAttributes(false));
            AttributeID<TAttr>._justChecked = true;
        }
        
        if (_cachedEntries[attrID] == null || _cachedEntries[attrID].Length == 0) return false;
        
        return _cachedEntries[attrID].Any(attribute => attribute.GetType() == typeof(TAttr));
    }
    
    public static bool Contains<TAttr, TSource>() where TAttr : Attribute
    {
        return Contains<TAttr>(typeof(TSource));
    }
}

internal static class AttributeMetadata { internal static int HighestID = 0; }

internal static class AttributeID<TAttr> where TAttr : Attribute 
{
    internal static readonly int ID = AttributeMetadata.HighestID++;
    internal static bool _justChecked = false;
}