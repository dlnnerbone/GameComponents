using System;
using Microsoft.Xna.Framework;
using GameComponents.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Entity;
namespace GameComponents.Logic;
public sealed class Raycast : IDirection
{
    private Vector2 direction = Vector2.UnitX;
    private float radians = 0;
    private Vector2 origin = Vector2.Zero;
    private float maxDistance = 0;
    private float t = 0;
    private readonly Vector2[] Vertices = new Vector2[4];
    // private fields
    public Vector2 Direction 
    {
        get => direction;
        set 
        {
            direction = value != Vector2.Zero ? Vector2.Normalize(value) : Vector2.UnitX;
            radians = (float)Math.Atan2(direction.Y, direction.X);
        }
    }
    public Vector2 Origin { get => origin; set => origin = value; }
    public float HitDistance => t;
    public float MaxDistance { get => maxDistance; set => maxDistance = Math.Abs(value); }
    public float Radians 
    {
        get => radians;
        set 
        {
            radians = value;
            direction = new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
        }
    }
    public Vector2 GetEndPoint() => origin + (direction * maxDistance);
    // methods
    public void AimAt(Vector2 location) => Direction = location - origin;
    public void AimAt(Point location) => Direction = new Vector2(location.X, location.Y) - origin;

    public void SetDirection(Vector2 direction) => Direction = direction;
    public void SetDIrection(Point direction) => Direction = new Vector2(direction.X, direction.Y);
    
    public float Dot(Vector2 line) 
    {
        return Vector2.Dot(line, Direction);
    }
    // collision
    public bool RayToSegment(Vector2 start, Vector2 end) 
    {
        Vector2 segDir = end - start;
        Vector2 perp = new Vector2(-segDir.Y, segDir.X);
        float dotProduct = Dot(perp);
        float u = Vector2.Dot(new Vector2(-Direction.Y, Direction.X), start - Origin) / dotProduct;
        float calculatedT = Vector2.Dot(perp, start - Origin) / dotProduct;
        bool isValid = u >= 0 && u <= 1;
        if (Math.Abs(dotProduct) < float.Epsilon) return false;
        if (isValid && calculatedT >= 0 && calculatedT <= maxDistance)
        {
            t = calculatedT;
            return true;
        }
        else { t = maxDistance; return false; }
    }
    public bool RayToRectangle(Rectangle bounds) 
    {
        Vertices[0] = new Vector2(bounds.Left, bounds.Top);
        Vertices[1] = new Vector2(bounds.Right, bounds.Top);
        Vertices[2] = new Vector2(bounds.Right, bounds.Bottom);
        Vertices[3] = new Vector2(bounds.Left, bounds.Bottom);
        for(int i = 0; i < 4; i++) 
        {
            Vector2 start = Vertices[i];
            Vector2 end = Vertices[(i + 1) % 4];
            Vector2 segDir = end - start;
            Vector2 perp = new Vector2(-segDir.Y, segDir.X);
            float dot = Dot(perp);
            float calculatedT = Vector2.Dot(perp, start - Origin) / dot;
            float u = Vector2.Dot(new Vector2(-Direction.Y, Direction.X), start - Origin) / dot;
            bool isValid = u >= 0 && u <= 1;
            if (Math.Abs(dot) < float.Epsilon) continue;
            if (isValid && calculatedT >= 0 && calculatedT <= maxDistance)
            {
                t = calculatedT;
                return true;
            }
            else { t = maxDistance; }
        }
        t = maxDistance;
        return false;
    }
    public bool RayToRectangle(BodyComponent other) => RayToRectangle(other.Bounds);
    public void DrawLine(SpriteBatch batch, Texture2D texture, Color color, float thickness = 1) 
    {
        if (texture == null) throw new ArgumentException("texture should not be null.");
        batch.Draw(texture, Origin, null, color, Radians, new Vector2(0f, 0.5f), new Vector2(maxDistance, thickness), SpriteEffects.None, 1);
    }
    // constructor(s)
    public Raycast(Vector2 origin, Vector2 direction, float distance = 100f) 
    {
        if (direction == Vector2.Zero) throw new ArgumentException($"Direction argument can not be null or have both values as zero.");
        Origin = origin;
        MaxDistance = distance;
        Direction = direction;
    }
    public Raycast(Point origin, Vector2 direction, float distance = 100f) : this(new Vector2(origin.X, origin.Y), direction, distance) {}
}