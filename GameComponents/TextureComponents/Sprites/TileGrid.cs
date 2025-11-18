using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameComponents.Rendering;
public readonly struct TileGrid 
{
    public readonly Rectangle[] Regions; // int is basically the key, and using that key returns the value (The Rectangle)
    public readonly int Rows;
    public readonly int Columns;
    public readonly int TileWidth;
    public readonly int TileHeight;
    // readonly properties
    public int GetEndingTileIndex() => Regions.Length - 1;
    public Rectangle GetEndingTile() => Regions[^1];
    public Vector2 TileDimensions => new Vector2(TileWidth, TileHeight);
    public int TileAmount => Columns * Rows;
    public Vector2 TotalSize => new Vector2(TileWidth * Columns, TileHeight * Rows);
    
    public TileGrid(int columns, int rows, int width, int height) 
    {
        if (columns <= 0 || rows <= 0) throw new ArgumentException("columns and/or rows can not be a value lower than one.");
        Regions = new Rectangle[columns * rows];
        Columns = columns;
        Rows = rows;

        TileWidth = width / columns;
        TileHeight = height / rows;
        
        for(int i = 0; i < columns * rows; i++) 
        {
            int x = i % columns * TileWidth;
            int y = i / columns * TileHeight;
            Regions[i] = new Rectangle(x, y, TileWidth, TileHeight);
        }
    }
    public TileGrid(int columns, int rows, Rectangle bounds) : this(columns, rows, bounds.Width, bounds.Height) {}
    public TileGrid(int columns, int rows, Texture2D texture) : this(columns, rows, texture.Bounds) {}
    
}