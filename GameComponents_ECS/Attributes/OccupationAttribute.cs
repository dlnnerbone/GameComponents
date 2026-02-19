namespace GameComponents.Attributes;

// an attribute that helps filter components that are set to be occupied to a specific class/struct during compile time.

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
public class OccupationAttribute : Attribute 
{
    public readonly Type OccupiedType;
    
    public OccupationAttribute(Type type) 
    {
        OccupiedType = type;
    }
    
    public Type GetUnderlyingType() => OccupiedType;
}