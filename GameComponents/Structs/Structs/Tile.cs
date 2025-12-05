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

public struct Tile 
{
    private byte id;
    // fields
    public Rectangle Bounds { get; set; }
    public TileFlags Flags { get; set; }
    public byte ID { get => id; set => id = (byte)Math.Abs(value); }
    // constructors
    public Tile(int x, int y, int width, int height, byte id, TileFlags flag = TileFlags.None) 
    {
        Bounds = new(x, y, width, height);
        this.id = (byte)(id < 0 ? 0 : id);
        Flags = flag;
    }
    
    public Tile(Point location, Point Size, byte id, TileFlags flag = TileFlags.None) 
    {
        Bounds = new(location, Size);
        ID = (byte)(id < 0 ? 0 : id);
        Flags = flag;
    }
    
    public Tile(Vector2 location, Vector2 Size, byte id, TileFlags flag = TileFlags.None) 
    {
        Bounds = new((int)location.X, (int)location.Y, (int)Size.X, (int)Size.Y);
        ID = (byte)(id < 0 ? 0 : id);
        Flags = flag;
    }

    // utility properties
    public Vector2 TopLeft => new Vector2(Bounds.Left, Bounds.Top);
    public Vector2 TopRight => new Vector2(Bounds.Right, Bounds.Top);
    public Vector2 BottomLeft => new Vector2(Bounds.Left, Bounds.Bottom);
    public Vector2 BottomRight => new Vector2(Bounds.Right, Bounds.Bottom);
    public Vector2 Center => new Vector2(Bounds.X + Bounds.Width / 2, Bounds.Y + Bounds.Height / 2);
    public Vector2 HalfSize => new Vector2(Bounds.Width / 2, Bounds.Height / 2);
    public Vector2 QuarterSize => new Vector2(Bounds.Width / 4, Bounds.Height / 4);
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
    public void AddFlag(TileFlags flags) => Flags |= flags;
    public void RemoveFlag(TileFlags flags) => Flags &= ~flags;
    public void OverrideFlags(TileFlags flags) => Flags = flags;
    public void PurgeFlags() => Flags = TileFlags.None;

    // intersection methods
    public bool IntersectsWithTile(Rectangle other) => Bounds.Intersects(other);
    
}