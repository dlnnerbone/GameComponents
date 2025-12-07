using GameComponents.Interfaces;
using Microsoft.Xna.Framework;
namespace GameComponents;

public struct Collider : IBodyComponent 
{
    private byte layerId;
    private Rectangle bounds;
    // public properties
    
    /// <summary>
    /// collision bounds of the Collider.
    /// </summary>
    public Rectangle Bounds { get => bounds; set => bounds = value; }
    /// <summary>
    /// the ID corrseponding to what "Layer" the collider is in.
    /// </summary>
    public byte LayerID { get => layerId; set => layerId = (byte)Math.Abs(value); }
    /// <summary>
    /// if the Collider is Active or not.
    /// </summary>
    public bool IsActive { get; set; }
    
    public Vector2 Position 
    {
        get => new Vector2(bounds.X, bounds.Y);
        set => bounds.Location = new((int)value.X, (int)value.Y);
    }
    
    public Vector2 Size
    {
        get => new Vector2(bounds.Width, bounds.Height);
        set => bounds.Size = new((int)value.X, (int)value.Y);
    }
    
    public float Top => Bounds.Top;
    public float Bottom => Bounds.Bottom;
    public float Right => Bounds.Right;
    public float Left => Bounds.Left;
    
    public Vector2 TopLeft => new Vector2(Left, Top);
    public Vector2 TopRight => new Vector2(Right, Top);
    public Vector2 BottomLeft => new Vector2(Left, Bottom);
    public Vector2 BottomRight => new Vector2(Right, Bottom);
    
    public Collider(int x, int y, int width, int height, byte id, bool isActive = true) 
    {
        bounds = new(x, y, width, height);
        layerId = (byte)(id < 0 ? 0 : id);
        IsActive = isActive;
    }
    
    public Collider(Point location, Point size, byte id, bool isActive) : this(location.X, location.Y, size.X, size.Y, id, isActive) {}
    
    public Collider(Rectangle bounds, byte id, bool isActive) : this(bounds.Location, bounds.Size, id, isActive) {}
    
}