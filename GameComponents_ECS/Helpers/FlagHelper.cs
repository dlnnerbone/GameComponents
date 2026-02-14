using System.Runtime.CompilerServices;
namespace GameComponents.Helpers;

public static class FlagChecker<TFlag> where TFlag : struct, Enum 
{
    public static TTo FlagTo<TTo>(TFlag source) where TTo : struct 
    {
        return Unsafe.BitCast<TFlag, TTo>(source);
    }
    
    public static bool Has(TFlag source, TFlag flag) => source.HasFlag(flag);
}

public static class FlagHelper<TFlag> where TFlag : struct, Enum 
{
    public static void OutFlags(TFlag source, TFlag other, out ushort sourceValue, out ushort flagValue) 
    {
        sourceValue = FlagChecker<TFlag>.FlagTo<ushort>(source);
        flagValue = FlagChecker<TFlag>.FlagTo<ushort>(other);
    }
    
    public static void Combine(ref TFlag sourceFlags, TFlag flag) 
    {
        OutFlags(sourceFlags, flag, out var source, out var flagVal);
        
        sourceFlags = (TFlag)(object)(source | flagVal);
    }
    
    public static void Remove(ref TFlag source, TFlag flagToRemove) 
    {
        OutFlags(source, flagToRemove, out var flagsVal, out var flagVal);
        
        source = (TFlag)(object)(flagsVal & ~flagVal);
    }
    
    public static void Override(ref TFlag source, TFlag set) 
    {
        var flagValue = FlagChecker<TFlag>.FlagTo<ushort>(set);
        
        source = (TFlag)(object)flagValue;
    }
}