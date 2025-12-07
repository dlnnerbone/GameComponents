using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Interfaces;
using System.Runtime.InteropServices;
namespace GameComponents;

public struct Tile : IBodyComponent
{
    // private fields
    private byte sourceId;
    private float layerDepth;
    private Rectangle bounds;
    // public properties
    public Rectangle Bounds { get => bounds; set => bounds = value; }
    public byte SourceID { get => sourceId; set => sourceId = (byte)Math.Abs(value); }
    public float LayerDepth { get => layerDepth; set => layerDepth = MathHelper.Clamp(value, 0f, 1f); }
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public Color Color { get; set; }
    public bool IsDrawable { get; set; } = true;
    // extra properties
    
    public Vector2 Position
    {
        get => new Vector2(Bounds.X, Bounds.Y);
        set => bounds.Location = new Point((int)value.X, (int)value.Y);
    }
    
    public Vector2 Size 
    {
        get => new Vector2(Bounds.Width, Bounds.Height);
        set => bounds.Size = new Point((int)value.X, (int)value.Y);
    }
    
    public float Left => Bounds.Left;
    public float Right => Bounds.Right;
    public float Bottom => Bounds.Bottom;
    public float Top => Bounds.Top;
    
    public Vector2 TopLeft => new Vector2(Left, Top);
    public Vector2 TopRight => new Vector2(Right, Top);
    public Vector2 BottomLeft => new Vector2(Left, Bottom);
    public Vector2 BottomRight => new Vector2(Right, Bottom);
    
    public int GetByteSize() => Marshal.SizeOf<Tile>();
    
    public Tile(int x, int y, int width, int height, byte id, float layerDepth, Color color) 
    {
        bounds = new(x, y, width, height);
        sourceId = (byte)(id < 0 ? 0 : id);
        this.layerDepth = layerDepth;
        Color = color;
    }
    
    public Tile(Point location, Point size, byte id, float layerDepth, Color color) : this(location.X, location.Y, size.X, size.Y, id, layerDepth, color) {}
    
    public Tile(Rectangle bounds, byte id, float layerDepth, Color color) : this(bounds.Location, bounds.Size, id, layerDepth, color) {}
}