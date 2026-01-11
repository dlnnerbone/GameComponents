using System.Linq;
namespace GameComponents.Systems;

/// <summary>
/// a FlagHelper class to help with finding flags
/// WARNING: THIS CLASS ONLY WORKS WITH BITMASKS WITH THE BACKING FIELD OF INT.
/// </summary>
public static class FlagHelper 
{
    // boolean checks to check if flags have a flag.
    
    public static bool Has<TFlag>(TFlag flags, TFlag flag) where TFlag : struct
    {
        int flagsValue = (int)(object)flags;
        int flagValue = (int)(object)flag;
        
        return (flagsValue & flagValue) != 0;
    }
    
    public static void Set<TFlag>(ref TFlag flags, TFlag flag) where TFlag : struct
    {
        var flagsValue = (int)(object)flags;
        var flagValue = (int)(object)flag;
        
        flags = (TFlag)(object)(flagsValue | flagValue);
    }
    
    // removing
    
    public static void Remove<TFlag>(ref TFlag flags, TFlag flag) where TFlag : struct 
    {
        int flagsValue = (int)(object)flags;
        int flagValue = (int)(object)flag;
        
        flags = (TFlag)(object)(flagsValue & ~flagValue);
    }
    
    public static void OverrideSet<TFlag>(ref TFlag flags, TFlag flag) where TFlag : struct 
    {
        int flagValue = (int)(object)flag;
        
        flags = (TFlag)(object)flagValue;
    }
    
    public static void Purge<TFlag>(ref TFlag flags) where TFlag : struct 
    {
        flags = (TFlag)(object)0;
    }
    
    // checking
    
    public static bool HasAllOf<TFlag>(TFlag flags, IEnumerable<TFlag> selectedFlags) where TFlag : struct 
    {
        int flagsValue = (int)(object)flags;
        
        return selectedFlags.All(flag => (flagsValue & (int)(object)flag) != 0);
    }
}