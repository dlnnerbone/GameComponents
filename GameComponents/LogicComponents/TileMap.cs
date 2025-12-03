using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameComponents;
namespace GameComponents.Logic;

public class TileMap 
{
    public readonly Tile[,] Tiles;
    public bool IsActive { get; set; } = true;
    public readonly int MapID;
    public int TileCount => Tiles.Length;
    
    public readonly Vector2 MapSize;
    
    public readonly Texture2D TextureAtlas;
}
