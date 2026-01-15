using System;

namespace GameComponents.Helpers;

public static class RuntimeHelpers 
{
    public static bool IsReference<T>() where T : notnull 
    {
        if (!typeof(T).IsValueType) return true;
        return false;
    }
}