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
    public readonly Vector2 Origin = Vector2.Zero;
    public readonly Tile[,] Tiles;
    public readonly TileGrid SourceTileGrid;
    public readonly List<Rectangle> Colliders = new(); 
    public bool IsActive { get; set; } = true;
    
    public readonly int TileWidth;
    public readonly int TileHeight;
    
    public int Rows => Tiles.GetLength(0);
    public int Columns => Tiles.GetLength(1);
    
    // delegates
    
    public delegate void actionUpdater(int index, bool isCollided, Rectangle collider);
    
    public TileMap(Vector2 origin, TileGrid sourceGrid, int initialTileWidth, int initialTileHeight, LayoutDirection dir, int[,] layout) 
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
            
            int x = (int)Origin.X + (dir == LayoutDirection.Horizontal ? c : r) * TileWidth;
            int y = (int) Origin.Y + (dir == LayoutDirection.Horizontal ? r : c) * TileHeight;
            
            Tiles[r, c] = new Tile(x, y, TileWidth, TileHeight, (byte)layout[r, c], TileFlags.None);
        }
    }
    
    public void UpdateTileMap(actionUpdater action, BodyComponent body)
    {
        if (!IsActive) return;
        for(int i = 0; i < Colliders.Count; i++)
        {
            action(i, Colliders[i].Intersects(body.Bounds), Colliders[i]);
        }
    }
    
    // collision, addition and removal.
    
    public void AddColliders(int[] selectedIDs, Rectangle bounds) 
    {
        var idSet = new HashSet<int>(selectedIDs);
        AddColliders(idSet, bounds);
    }
    
    public void AddColliders(HashSet<int> idSet, Rectangle bounds) 
    {
        if (idSet.Count == 0) return;
        
        for(int i = 0; i < Tiles.Length; i++)
        {
            int r = i / Columns;
            int c = i % Columns;
            
            if (idSet.Contains(Tiles[r, c].ID)) 
            {
                Colliders.Add(new Rectangle(Tiles[r, c].Bounds.X + bounds.X, Tiles[r, c].Bounds.Y + bounds.Y, bounds.Width, bounds.Height));
            }
        }
    }
    
    public void AddColliders(int row, int column, Rectangle bounds) 
    {
        if (row < 0 || column < 0 || row > Rows - 1 || column > Columns - 1) 
        {
            throw new IndexOutOfRangeException("index of row or column is negative or invalid.");
        }
        
        Colliders.Add(new Rectangle(bounds.X + Tiles[row, column].Bounds.X, bounds.Y + Tiles[row, column].Bounds.Y, bounds.Width, bounds.Height));
    }
    
    public void Draw(SpriteBatch batch, Texture2D textureAtlas) 
    {
        for(int i = 0; i < Tiles.Length; i++) 
        {
            var r = i / Columns;
            var c = i % Columns;
            
            batch.Draw(textureAtlas, Tiles[r, c].Bounds, SourceTileGrid.Regions[Tiles[r, c].ID], Color.White);
        }
    }
}
