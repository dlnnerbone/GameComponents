using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameComponents.Rendering;
using GameComponents.Helpers;
using System.Runtime.CompilerServices;
namespace GameComponents.Logic;

public enum LayoutDirection
{
    Horizontal,
    Vertical
}

public class TileMapVisuals 
{
    public readonly Tile[] Tiles;
    public bool IsActive { get; set; } = true;
    public TileGrid SourceGrid { get; set; } = default;
    public readonly LayoutDirection LayoutDirection;
    public readonly Vector2 Origin;
    
    public readonly byte[,] TileLayout;
    
    public int Rows => TileLayout.GetLength(0);
    public int Columns => TileLayout.GetLength(1);
    
    // constructor(s).
    
    public TileMapVisuals(Vector2 origin, LayoutDirection direction, int tileSize, byte[,] layout, bool isActive = true, float initialDepth = 1f) 
    {
        // readonly and boolean initialization.
        Origin = origin;
        TileLayout = layout;
        IsActive = isActive;
        LayoutDirection = direction;
        
        // Tile Array initialzation.
        Tiles = new Tile[Rows * Columns];
        
        for (int r = 0; r < Rows; r++) 
        {
            for (int c = 0; c < Columns; c++) 
            {
                int x = (int)Origin.X + (LayoutDirection == LayoutDirection.Horizontal ? c : r) * tileSize;
                int y = (int)Origin.Y + (LayoutDirection == LayoutDirection.Horizontal ? r : c) * tileSize;
                int index = r * Columns + c;
                
                Tiles[index] = new Tile(x, y, tileSize, tileSize, layout[r, c], initialDepth);
            }
        }
        
    }
    
    public TileMapVisuals(Vector2 origin, LayoutDirection direction, int tileWidth, int tileHeight, byte[,] layout, bool isActive, float initialDepth = 1f) 
    {
        // readonly and variable initialization;
        Origin = origin;
        LayoutDirection = direction;
        IsActive = isActive;
        TileLayout = layout;
        
        // Tile Array initialization.
        Tiles = new Tile[Rows * Columns];
        
        for (int r = 0; r < Rows; r++) 
        {
            for (int c = 0; c < Columns; c++) 
            {
                int x = (int)Origin.X + (LayoutDirection == LayoutDirection.Horizontal ? c : r) * tileWidth;
                int y = (int)Origin.Y + (LayoutDirection == LayoutDirection.Horizontal ? r : c) * tileHeight;
                int index = r * Columns + c;
                
                Tiles[index] = new Tile(x, y, tileWidth, tileHeight, layout[r, c], initialDepth);
            }
        }
    }
    
    // general methods
    
    // setting up the SourceGrid or changing it:
    
    public void SetSourceGrid(TileGrid grid) 
    {
        SourceGrid = grid;
    }
    
    // finding Tiles
    
    public bool HasTile(int row, int col) 
    {
        int amount = row * col;
        if (int.IsNegative(amount) || amount >= Tiles.Length) return false;
        return true;
    }
    
    public ref Tile GetTile(int row, int column) 
    {
        var index = row * Columns + column;
        if (index < 0 || index >= Tiles.Length) throw new IndexOutOfRangeException($"{row} or {column} is too high or low (invalid) within the Array.");
        return ref Tiles[index];
    }
    
    public ref Tile GetNeighbouringTopTile(int row, int column) 
    {
        var index = row * Columns + column;
        return ref GetNeighbouringTopTile(index);
    }
    
    public ref Tile GetNeighbouringBottomTile(int row, int col) 
    {
        var index = row * Columns + col;
        return ref GetNeighbouringBottomTile(index);
    }
    
    public ref Tile GetNeighbouringLeftTile(int row, int col) 
    {
        var index = row * Columns + col;
        return ref GetNeighbouringLeftTile(index);
    }
    
    public ref Tile GetNeighbouringRightTile(int row, int col) 
    {
        var index = row * Columns + col;
        return ref GetNeighbouringRightTile(index);
    }
    
    public ref Tile GetNeighbouringTopTile(int selectedIndex) 
    {
        var index = selectedIndex - Columns;
        if (index < 0 || index >= Tiles.Length) return ref Tiles[selectedIndex];
        return ref Tiles[index];
    }
    
    public ref Tile GetNeighbouringBottomTile(int selectedIndex) 
    {
        var index = selectedIndex + Columns;
        if (index < 0 || index >= Tiles.Length) return ref Tiles[selectedIndex];
        return ref Tiles[index];
    }
    
    public ref Tile GetNeighbouringLeftTile(int selectedIndex) 
    {
        var index = selectedIndex - 1;
        if (index < 0 || index >= Tiles.Length) throw new IndexOutOfRangeException($"{selectedIndex} is invalid.");
        else if (Tiles[index].Bounds.Y != Tiles[index + 1].Bounds.Y) return ref Tiles[selectedIndex];
        
        return ref Tiles[index];
    }
    
    public ref Tile GetNeighbouringRightTile(int selectedIndex) 
    {
        var index = selectedIndex + 1;
        if (index < 0 || index >= Tiles.Length) throw new IndexOutOfRangeException($"{selectedIndex} is invalid.");
        else if (Tiles[index].Bounds.Y != Tiles[index - 1].Bounds.Y) return ref Tiles[selectedIndex];
        
        return ref Tiles[index];
    }
    
    // Setting up or changing the ID of selected Tiles.
    
    public void SetSourceID(int index, byte newID) 
    {
        if (index < 0 || index >= Tiles.Length || newID < 0 || newID >= SourceGrid.Regions.Length) return;
        Tiles[index].SourceID = newID;
    }
    
    public void SetSourceID(HashSet<int> selectedIndices, byte newID) 
    {
        if (selectedIndices.Count == 0) return;
        
         for(int i = 0; i < Tiles.Length; i++)  
         {
             if (selectedIndices.Contains(Tiles[i].SourceID)) SetSourceID(i, newID);
         }
    }
    
    //  setting up or changing LayerDepth of selected tiles.
    
    public void SetLayerDepth(int index, float newDepth) 
    {
        if (index < 0 || index >= Tiles.Length) return;
        Tiles[index].LayerDepth = newDepth;
    }
    
    public void SetLayerDepth(HashSet<int> selectedIndices, float newDepth) 
    {
        if (selectedIndices.Count == 0) return;
        
        for(int i = 0; i < Tiles.Length; i++) 
        {
            if (selectedIndices.Contains(Tiles[i].SourceID)) SetLayerDepth(i, newDepth);
        }
    }
    
    // setting up or changing whether or not selected Tiles are drawable within the draw call.
    
    public void ToggleVisibility(int index, bool isVisible) 
    {
        if (index < 0 || index >= Tiles.Length) return;
        Tiles[index].IsDrawable = isVisible;
    }
    
    public void ToggleVisibility(HashSet<int> selectedIndices, bool isVisible) 
    {
        if (selectedIndices.Count == 0) return;
        
        for(int i = 0; i < Tiles.Length; i++) 
        {
            if (selectedIndices.Contains(Tiles[i].SourceID)) ToggleVisibility(i, isVisible);
        }
    }
    
    // drawing
    
    public void Draw(SpriteBatch batch, Texture2D texture, Color color) 
    {
        if (!IsActive) return;
        
        foreach(ref readonly var t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(texture, t.Bounds, SourceGrid.Regions[t.SourceID], color, 0, Vector2.Zero, SpriteEffects.None, t.LayerDepth);
        }
    }
    
    public void Draw(SpriteBatch batch, Texture2D texture, float radians, Color color) 
    {
    
        if (!IsActive) return;
        
        foreach(ref readonly var t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(texture, t.Bounds, SourceGrid.Regions[t.SourceID], color, radians, Vector2.Zero, SpriteEffects.None, t.LayerDepth);
        }
    }
    
    public void Draw(SpriteBatch batch, Texture2D texture, float radians, Vector2 origin, Color color) 
    {
        if (!IsActive) return;
        
        foreach(ref readonly var t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(texture, t.Bounds, SourceGrid.Regions[t.SourceID], color, radians, origin, SpriteEffects.None, t.LayerDepth);
        }
    }
    
    public void Draw(SpriteBatch batch, Texture2D texture, float radians, Vector2 origin, SpriteEffects effects, Color color) 
    {
        if (!IsActive) return;
        
        foreach(ref readonly var t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(texture, t.Bounds, SourceGrid.Regions[t.SourceID], color, radians, origin, effects, t.LayerDepth);
        }
    }
    
}
