using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
namespace GameComponents.Entity;
public class BodyComponent : IBodyComponent 
{
    protected Rectangle ProtectedBounds;
    //
    public Rectangle Bounds { get => ProtectedBounds; set => ProtectedBounds = value; }
    
    public Vector2 Position 
    {
        get => new Vector2(X, Y);
        set => ProtectedBounds.Location = new Point((int)value.X, (int)value.Y);
    }
    
    public Vector2 Size 
    {
        get => new Vector2(X, Y);
        set => ProtectedBounds.Size = new Point((int)value.X, (int)value.Y);
    }
    
    public int X { get => Bounds.X; set => ProtectedBounds.X = value; }
    public int Y { get => Bounds.Y; set => ProtectedBounds.Y = value; }
    public int Width { get => Bounds.Width; set => ProtectedBounds.Width = value; }
    public int Height { get => Bounds.Height; set => ProtectedBounds.Height = value; }
    
    public float Left => Bounds.Left;
    public float Right => Bounds.Right;
    public float Top => Bounds.Top;
    public float Bottom => Bounds.Bottom;
    
    public float HalfWidth => Width / 2f;
    public float HalfHeight => Height / 2f;
    
    public float QuarterWidth => Width / 4;
    public float QuarterHeight => Height / 4;
    
    public float GetWidthProportion(float denominator) => Width / denominator;
    public float GetHeightProportion(float denominator) => Height / denominator; 
    
    public Vector2 GetProportion(float deno) => GetProportion(deno, deno);
    public Vector2 GetProportion(float deno1, float deno2) 
    {
        return new Vector2(GetWidthProportion(deno1), GetHeightProportion(deno2));
    }
    public Vector2 QuarterSize => new Vector2(QuarterWidth, QuarterHeight);
    public Vector2 HalfSize => new Vector2(HalfWidth, HalfHeight);
    
    public Vector2 TopLeft => new Vector2(Left, Top);
    public Vector2 TopRight => new Vector2(Right, Top);
    public Vector2 BottomLeft => new Vector2(Left, Bottom);
    public Vector2 BottomRight => new Vector2(Right, Bottom);
    public Vector2 Center => new Vector2(X + HalfWidth, Y + HalfHeight);
    
    // methods
    
    public bool Contains(Vector2 location) => Bounds.Contains(location);
    public bool Contains(Point location) => Bounds.Contains(location);
    public bool Contains(Rectangle otherBounds) => Bounds.Contains(otherBounds);
    public bool Contains(IBodyComponent otherComponent) => Bounds.Contains(otherComponent.Bounds);
    
    public void Contains(ref Point point, out bool result) => Bounds.Contains(ref point, out result);
    public void Contains(ref Rectangle rect, out bool result) => Bounds.Contains(ref rect, out result);
    public void Contains(ref Vector2 location, out bool result) => Bounds.Contains(ref location, out result);
    
    public bool Intersects(Rectangle otherBounds) => Bounds.Intersects(otherBounds);
    public bool Intersects(IBodyComponent otherComponent) => Intersects(otherComponent.Bounds);
    
    public void Deconstruct(out int x, out int y, out int width, out int height) => Bounds.Deconstruct(out x, out y, out width, out height);
    
    public bool Equals(Rectangle bounds) => Bounds.Equals(bounds);
    
    public void Inflate(int horizontalAmount, int verticalAmount) => Bounds.Inflate(horizontalAmount, verticalAmount);
    public void Inflate(float horizontalAmount, float verticalAmount) => Bounds.Inflate(horizontalAmount, verticalAmount);
    
    public bool IsEmpty => Bounds.IsEmpty;
    
    public BodyComponent(int x, int y, int width, int height) 
    {
        ProtectedBounds = new Rectangle(x, y, width, height);
    }
    
    public BodyComponent(Point location, Point dimensions) 
    {
        ProtectedBounds = new Rectangle(location, dimensions);
    }
}