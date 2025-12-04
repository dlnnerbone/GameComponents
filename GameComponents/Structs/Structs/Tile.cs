using Microsoft.Xna.Framework;
using System;
using GameComponents.Entity;
namespace GameComponents;
[Flags] public enum TileFlags : byte
{
    None = 0,
    Collidable = 1,
    Walkable = 2,
    Dangerous = 4,
    Movable = 8
}

public readonly struct Tile 
{
    // fields
    public readonly Rectangle Region;
    public readonly TileFlags Flags;
    public readonly int ID;
    
    // constructors
    public Tile(int x, int y, int width, int height, int id, TileFlags flag = TileFlags.None) 
    {
        Region = new(x, y, width, height);
        ID = id;
        Flags = flag;
    }
    public Tile(Point location, Point Size, int id, TileFlags flag = TileFlags.None) 
    {
        Region = new(location, Size);
        ID = id;
        Flags = flag;
    }
    public Tile(Vector2 location, Vector2 Size, int id, TileFlags flag = TileFlags.None) 
    {
        Region = new((int)location.X, (int)location.Y, (int)Size.X, (int)Size.Y);
        ID = id;
        Flags = flag;
    }
    private Tile(Rectangle Bounds, int id, TileFlags flag) 
    {
        Region = Bounds;
        ID = id;
        Flags = flag;
    }

    // utility properties
    public Vector2 TopLeft => new Vector2(Region.Left, Region.Top);
    public Vector2 TopRight => new Vector2(Region.Right, Region.Top);
    public Vector2 BottomLeft => new Vector2(Region.Left, Region.Bottom);
    public Vector2 BottomRight => new Vector2(Region.Right, Region.Bottom);
    public Vector2 Center => new Vector2(Region.X + Region.Width / 2, Region.Y + Region.Height / 2);
    public Vector2 HalfSize => new Vector2(Region.Width / 2, Region.Height / 2);
    public Vector2 QuarterSize => new Vector2(Region.Width / 4, Region.Height / 4);
    // -- flag checks --
    public bool IsMovable => (Flags & TileFlags.Movable) == TileFlags.Movable;
    public bool IsCollidable => (Flags & TileFlags.Collidable) == TileFlags.Collidable;
    public bool IsDangerous => (Flags & TileFlags.Dangerous) == TileFlags.Dangerous;
    public bool IsWalkable => (Flags & TileFlags.Walkable) == TileFlags.Walkable;
    public bool HasNoAttributes => Flags == TileFlags.None;
    // extra bool utilities
    public bool IsSolid => IsCollidable && !IsWalkable;
    public bool IsPlatform => IsCollidable && IsWalkable;
    public bool IsPosionous => IsWalkable && IsDangerous;
    public bool IsPushable => IsMovable && IsCollidable && IsWalkable;
    // safe modifiers
    public Tile AddFlag(TileFlags flag) => new Tile(this.Region, this.ID, this.Flags & flag);
    public Tile RemoveFlag(TileFlags flag) => new Tile(this.Region, this.ID, this.Flags & ~flag);
    public Tile OverrideFlags(TileFlags flags) => new Tile(this.Region, this.ID, flags);
    public Tile PurgeFlags() => new Tile(this.Region, this.ID, TileFlags.None);

    // intersection methods
    public bool IntersectsWithTile(Rectangle other) => Region.Intersects(other);
    
}