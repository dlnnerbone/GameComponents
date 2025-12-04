using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Rendering;
namespace GameComponents.Logic;

/// <summary>
/// TileMap class.
/// 
/// use a 2D Array of Tiles, each Tile is different for the TileGrid, a TileGrid is as a source region, tiles are the location.
/// an Origin point value is used to put the TileMap's top-left point as a position for the TileMap.
/// 
/// </summary>

public class TileMap 
{
    public Vector2 Origin { get; set; } = Vector2.Zero;
    public readonly Tile[,] Tiles;
    public readonly TileGrid SourceTileGrid;
    public List<Rectangle> Colliders { get; set; } = new List<Rectangle>();
    public bool IsActive { get; set; } = true;
    
    public readonly int TileWidth;
    public readonly int TileHeight;
    
    public int Rows => Tiles.GetLength(0);
    public int Columns => Tiles.GetLength(1);
    
    public TileMap(Vector2 origin, TileGrid sourceGrid, int initialTileWidth, int initialTileHeight, int[,] layout) 
    {
        Origin = origin;
        TileWidth = initialTileWidth;
        TileHeight = initialTileHeight;
        SourceTileGrid = sourceGrid;
        
        Tiles = new Tile[layout.GetLength(0), layout.GetLength(1)];
        
        for(int i = 0; i < layout.Length; i++) 
        {
            int r = i / layout.GetLength(1);
            int c = i % layout.GetLength(1);
            
            int x = (int)Origin.X + r * TileWidth;
            int y = (int) Origin.Y + c * TileHeight;
            
            Tiles[r, c] = new Tile(x, y, TileWidth, TileHeight, layout[r, c], TileFlags.None);
        }
    }
    
    public void UpdateTiles() {}
    
    public void Draw(SpriteBatch batch, Texture2D textureAtlas) 
    {
        for(int i = 0; i < Tiles.Length; i++) 
        {
            var r = i / Columns;
            var c = i % Columns;
            
            batch.Draw(textureAtlas, Tiles[r, c].Region, SourceTileGrid.Regions[Tiles[r, c].ID], Color.White);
        }
    }
}
