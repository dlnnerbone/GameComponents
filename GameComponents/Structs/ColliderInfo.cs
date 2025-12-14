using GameComponents.Interfaces;
using Microsoft.Xna.Framework;
namespace GameComponents;

public struct ColliderInfo
{
    private byte layerID;

    public float OverlapX { get; set; }
    public float OverlapY { get; set; }
    public int LayerID { get => layerID; set => layerID = (byte)Math.Abs(layerID); }
    
}