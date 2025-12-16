using Microsoft.Xna.Framework;
namespace GameComponents;

public struct Collider
{
    private byte layerID;
    
    public Rectangle Bounds { get; set; }
    public bool IsActive { get; set; }
    public int LayerID { get => layerID; set => layerID = (byte)Math.Abs(layerID); }
    
    public Collider(int x, int y, int width, int height, int id, bool isActive) 
    {
        layerID = (byte)id;
        IsActive = isActive;
        Bounds = new Rectangle(x, y, width, height);
    }
    
    public Collider(Point location, Point size, int id, bool isActive) 
    {
        layerID = (byte)id;
        IsActive = isActive;
        Bounds = new(location, size);
    }
}