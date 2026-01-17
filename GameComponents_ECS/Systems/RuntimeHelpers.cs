using System.Reflection;

namespace GameComponents.Helpers;

public static class RuntimeHelpers 
{
    public static bool IsReference<T>()
    {
        if (!typeof(T).IsValueType) return true;
        return false;
    }
    
    public static bool IsReferenceOrContainsReferences<T>() 
    {
        Type type = typeof(T);
        
        if (type.IsClass) return true;
        
        foreach(ref readonly var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic).AsSpan()) 
        {
            if (field.FieldType.IsClass) return true;
        }
        
        foreach(ref readonly var prop in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic).AsSpan()) 
        {
            if (prop.PropertyType.IsClass) return true;
        }
        
        return false;
    }
}