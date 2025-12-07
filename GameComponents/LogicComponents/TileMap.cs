using Microsoft.Xna.Framework;
using GameComponents.Entity;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Rendering;
namespace GameComponents.Logic;

public enum LayoutDirection
{
    Horizontal,
    Vertical
}

public class TileMap 
{
    public readonly Tile[] Tiles;
    public readonly Collider[] Colliders;
    public readonly Vector2 Origin;
    public bool IsActive { get; set; } = true;
    
    public TileMap(byte[,] tileLayout, byte[,] colliderLayout, Vector2 origin, int tileWidth, int tileHeight, LayoutDirection layoutDirection) 
    {
        var tileRows = tileLayout.GetLength(0);
        var tileCols = tileLayout.GetLength(1);
        
        var colliderRows = colliderLayout.GetLength(0);
        var colliderCols = colliderLayout.GetLength(1);
        
        Origin = origin;
        
        Tiles = new Tile[tileRows * tileCols];
        Colliders = new Collider[colliderRows * colliderCols];
        
        for(int i = 0; i < tileLayout.Length; i++) 
        {
            int r = i / tileCols;
            int c = i % tileCols;
            
            int x = (int)Origin.X + (layoutDirection == LayoutDirection.Horizontal ? c : r) * tileWidth;
            int y = (int)Origin.Y + (layoutDirection == LayoutDirection.Horizontal ? r : c) * tileHeight;
            
            Tiles[i] = new Tile(x, y, tileWidth, tileHeight, tileLayout[r, c], 0, Color.White);
        }
        
        for(int i = 0; i < colliderLayout.Length; i++) 
        {
            int r = i / colliderCols;
            int c = i % colliderCols;
            
            int x = (int)Origin.X + (layoutDirection == LayoutDirection.Horizontal ? c : r) * tileWidth;
            int y = (int)Origin.Y + (layoutDirection == LayoutDirection.Horizontal ? r : c) * tileHeight;
            
            Colliders[i] = new Collider(x, y, tileWidth, tileHeight, colliderLayout[r, c], true);
        }
        
    }
    
}
