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
    public readonly int TileRows, TileColumns, ColliderRows, ColliderColumns;
    public TileGrid SourceGrid { get; private set; }
    
    public bool IsActive { get; set; } = true;
    
    public TileMap(byte[,] tileLayout, byte[,] colliderLayout, Vector2 origin, int tileWidth, int tileHeight, LayoutDirection layoutDirection) 
    {
        TileRows = tileLayout.GetLength(0);
        TileColumns = tileLayout.GetLength(1);
        
        ColliderRows = colliderLayout.GetLength(0);
        ColliderColumns = colliderLayout.GetLength(1);
        
        Origin = origin;
        
        Tiles = new Tile[TileRows * TileColumns];
        Colliders = new Collider[ColliderRows * ColliderColumns];
        
        for(int i = 0; i < tileLayout.Length; i++) 
        {
            int r = i / TileColumns;
            int c = i % TileColumns;
            
            int x = (int)Origin.X + (layoutDirection == LayoutDirection.Horizontal ? c : r) * tileWidth;
            int y = (int)Origin.Y + (layoutDirection == LayoutDirection.Horizontal ? r : c) * tileHeight;
            
            Tiles[i] = new Tile(x, y, tileWidth, tileHeight, tileLayout[r, c], 0);
        }
        
        for(int i = 0; i < colliderLayout.Length; i++) 
        {
            int r = i / ColliderColumns;
            int c = i % ColliderColumns;
            
            int x = (int)Origin.X + (layoutDirection == LayoutDirection.Horizontal ? c : r) * tileWidth;
            int y = (int)Origin.Y + (layoutDirection == LayoutDirection.Horizontal ? r : c) * tileHeight;
            
            Colliders[i] = new Collider(x, y, tileWidth, tileHeight, colliderLayout[r, c], false);
        }
        
    }
    
    public void SetSourceGrid(TileGrid sourceGrid) 
    {
        SourceGrid = sourceGrid;
    }
    
    // changing IDs or making Tiles not drawable.
    
    public virtual void SetTileID(byte[] selectedIDs, byte newID) 
    {
        if (selectedIDs.Length == 0) return;
        var idSet = new HashSet<byte>(selectedIDs);
        SetTileID(idSet, newID);
    }
    
    public virtual void SetTileID(HashSet<byte> idSet, byte newID) 
    {
        if (idSet.Count == 0) return;
        
        for(int i = 0; i < Tiles.Length; i++) 
        {
            if (idSet.Contains(Tiles[i].SourceID)) SetTileID((byte)i, newID);
        }
    }
    
    public virtual void SetTileID(byte index, byte newID) 
    {
        if (index < 0 || index >= Tiles.Length) return;
        Tiles[index].SourceID = newID;
    }
    // visibility
    public virtual void SetTileVisibility(byte index, bool isVisible) 
    {
        if (index < 0 || index >= Tiles.Length) return;
        Tiles[index].IsDrawable = isVisible;
    }
    
    public virtual void SetTileVisibility(HashSet<byte> selectedIndices, bool isVisible) 
    {
        if (selectedIndices.Count == 0) return;
        for(int i = 0; i < Tiles.Length; i++) 
        {
            if (selectedIndices.Contains(Tiles[i].SourceID)) SetTileVisibility((byte)i, isVisible);
        }
    }
    
    public virtual void SetTileVisibility(byte[] selectedIndices, bool isVisible) 
    {
        if (selectedIndices.Length == 0) return;
        var set = new HashSet<byte>(selectedIndices);
        SetTileVisibility(set, isVisible);
    }
    
    // Colliders, Activation, Etc.
    
    public virtual void SetColliderLayer(byte index, byte newID) 
    {
        if (index < 0 || index >= Colliders.Length) return;
        Colliders[index].LayerID = newID;
    }
    
    public virtual void SetColliderLayer(HashSet<byte> selectedIndices, byte newID) 
    {
        if (selectedIndices.Count == 0) return;
        for(int i = 0; i < Colliders.Length; i++) 
        {
            if (selectedIndices.Contains(Colliders[i].LayerID)) Colliders[i].LayerID = newID;
        }
    }
    
    public virtual void SetColliderLayer(byte[] selectedIndices, byte newID) 
    {
        if (selectedIndices.Length == 0) return;
        var idSet = new HashSet<byte>(selectedIndices);
        SetColliderLayer(idSet, newID);
    }
    
    // Activation.
    
    public virtual void ToggleCollider(byte index, bool isActive) 
    {
        if (index < 0 || index > Colliders.Length) return;
        Colliders[index].IsActive = isActive;
    }
    
    public virtual void ToggleCollider(HashSet<byte> selectedIndices, bool isActive) 
    {
        if (selectedIndices.Count == 0) return;
        for(int i = 0; i < Colliders.Length; i++) 
        {
            if (selectedIndices.Contains(Colliders[i].LayerID)) Colliders[i].IsActive = isActive;
        }
    }
    
    public virtual void ToggleCollider(byte[] selectedIndices, bool isActive) 
    {
        if (selectedIndices.Length == 0) return;
        var idSet = new HashSet<byte>(selectedIndices);
        ToggleCollider(idSet, isActive);
    }
    
    public virtual void ToggleCollidersFromLayout(HashSet<byte> selectedIndices, bool isActive) 
    {
        if (selectedIndices.Count == 0) return;
        
        for(int i = 0; i < Tiles.Length; i++) 
        {
            if (i >= Colliders.Length) return;
            else if (selectedIndices.Contains(Tiles[i].SourceID)) Colliders[i].IsActive = isActive;
        }
    } 
    
    // drawing Tiles.
    
    public virtual void Draw(SpriteBatch batch, Texture2D textureAtlas) 
    {
        foreach(ref Tile tile in Tiles.AsSpan()) 
        {
            if (!tile.IsDrawable) continue;
            batch.Draw(textureAtlas, tile.Bounds, SourceGrid.Regions[tile.SourceID], Color.White, 0, Vector2.Zero, SpriteEffects.None, tile.LayerDepth);
        }
    }
    
    public virtual void Draw(SpriteBatch batch, Texture2D textureAtlas, float rotation) 
    {
        foreach(ref Tile t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(textureAtlas, t.Bounds, SourceGrid.Regions[t.SourceID], Color.White, rotation, Vector2.Zero, SpriteEffects.None, t.LayerDepth);
        }
    }
    
    public virtual void Draw(SpriteBatch batch, Texture2D textureAtlas, float rotation, Vector2 origin) 
    {
        foreach(ref var t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(textureAtlas, t.Bounds, SourceGrid.Regions[t.SourceID], Color.White, rotation, origin, SpriteEffects.None, t.LayerDepth);
        }
    }
    
    public virtual void Draw(SpriteBatch batch, Texture2D textureAtlas, float rotation, Vector2 origin, SpriteEffects effects) 
    {
        foreach(ref var t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(textureAtlas, t.Bounds, SourceGrid.Regions[t.SourceID], Color.White, rotation, origin, effects, t.LayerDepth);
        }
    }
    
    public virtual void Draw(SpriteBatch batch, Texture2D textureAtlas, float rotation, Vector2 origin, SpriteEffects effects, Color color) 
    {
        foreach(ref var t in Tiles.AsSpan()) 
        {
            if (!t.IsDrawable) continue;
            batch.Draw(textureAtlas, t.Bounds, SourceGrid.Regions[t.SourceID], color, rotation, origin, effects, t.LayerDepth);
        }
    }
    
}
