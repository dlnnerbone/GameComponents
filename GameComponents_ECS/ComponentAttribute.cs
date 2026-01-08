using System;

namespace GameComponents;

// attribute for selecting what Components should be included for a system.
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public class ProcessForAttribute : Attribute 
{
    public Type[] Types;
    
    public ProcessForAttribute(Type[] types) 
    {
        Types = types;
    }
    
    public Type[] GetTypes() => Types;
}