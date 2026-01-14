using System.Linq;
namespace GameComponents.Systems;

/// <summary>
/// a FlagHelper class to help with finding flags
/// WARNING: THIS CLASS ONLY WORKS WITH BITMASKS WITH THE BACKING FIELD OF INT.
/// </summary>
public static class FlagHelper 
{
    public static void FlagTo<TFlag, T>(TFlag flags, TFlag flag, out T flagsValue, out T flagValue) where T : struct where TFlag : struct, Enum 
    {
        flagsValue = (T)(object)flags;
        flagValue = (T)(object)flag;
    }
    
    public static bool Has<TFlag>(TFlag flags, TFlag flag) where TFlag : struct, Enum
    {
        FlagTo<TFlag, int>(flags, flag, out var flagsValue, out var flagValue);
        
        return (flagsValue & flagValue) != 0;
    }
    
    public static void Set<TFlag>(ref TFlag flags, TFlag flag) where TFlag : struct, Enum
    {
        FlagTo<TFlag, int>(flags, flag, out var flagsValue, out var flagValue);
        
        flags = (TFlag)(object)(flagsValue | flagValue);
    }
    
    // removing
    
    public static void Remove<TFlag>(ref TFlag flags, TFlag flag) where TFlag : struct, Enum 
    {
        FlagTo<TFlag, int>(flags, flag, out var flagsValue, out var flagValue);
        
        flags = (TFlag)(object)(flagsValue & ~flagValue);
    }
    
    public static void OverrideSet<TFlag>(ref TFlag flags, TFlag flag) where TFlag : struct, Enum 
    {
        int flagValue = (int)(object)flag;
        
        flags = (TFlag)(object)flagValue;
    }
    
    public static void Purge<TFlag>(ref TFlag flags) where TFlag : struct, Enum 
    {
        flags = (TFlag)(object)0;
    }
    
    // checking
    
    public static bool HasAllOf<TFlag>(TFlag flags, IEnumerable<TFlag> selectedFlags) where TFlag : struct, Enum 
    {
        int flagsValue = (int)(object)flags;
        
        return selectedFlags.All(flag => (flagsValue & (int)(object)flag) != 0);
    }
}