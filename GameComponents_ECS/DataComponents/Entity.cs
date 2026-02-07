namespace GameComponents;

public record struct Entity 
{
    public readonly int ID;
    public readonly ushort Version;
    public readonly ushort GroupID;
    
    public Entity(uint id) : this(id, 0, 1) {}
    public Entity(uint id, ushort groupID) : this(id, groupID, 1) {}
    public Entity(uint id, ushort groupID, ushort version) 
    {
        ID = (int)id;
        GroupID = groupID;
        Version = version;
    }
    
    internal Entity(int id = -1, ushort groupID = 0, ushort version = 0) 
    {
        ID = id;
        GroupID = groupID;
        Version = version;
    }
    
    public static Entity Null => new Entity(-1);
    public static Entity Construct(uint id) => new(id);
    public static Entity Construct(uint id, ushort groupID) => new(id, groupID);
    public static Entity Construct(uint id, ushort groupID, ushort version) => new(id, groupID, version);
    
    public override string ToString() => $"Entity ID: {ID}, Group ID: {GroupID}, Entity Version: {Version}";
}