using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
namespace GameComponents.Logic;

public class TileMapLogic 
{
    public readonly Collider[] Colliders;
    public bool IsLogicActive { get; set; }
    public readonly byte[,] Layout;
    public readonly LayoutDirection LayoutDirection;
    public readonly Vector2 Origin;
    
    public int Rows => Layout.GetLength(0);
    public int Columns => Layout.GetLength(1);
    
    public delegate void Logic(int currentIndex, ref Collider collider);
    
    public TileMapLogic(LayoutDirection direction, Vector2 origin, int collisionSize, byte[,] layout, bool isLogicActive = true) 
    {
        // readonly initialization.
        IsLogicActive = isLogicActive;
        LayoutDirection = direction;
        Layout = layout;
        Origin = origin;
        // array initialization.
        Colliders = new Collider[Layout.Length];
        
        for(int r = 0; r < Rows; r++) 
        {
            for (int c = 0; c < Columns; c++) 
            {
                var x = (int)Origin.X + (LayoutDirection == LayoutDirection.Horizontal ? c : r) * collisionSize;
                var y = (int)Origin.Y + (LayoutDirection == LayoutDirection.Horizontal ? r : c) * collisionSize;
                var index = r * Columns + c;
                
                Colliders[index] = new Collider(x, y, collisionSize, collisionSize, Layout[r, c], isLogicActive);
            }
        }
    }
    
    public TileMapLogic(LayoutDirection direction, Vector2 origin, int width, int height, byte[,] layout, bool isLogicActive) 
    {
        IsLogicActive = isLogicActive;
        LayoutDirection = direction;
        Origin = origin;
        Layout = layout;
        // array initialzation.
        Colliders = new Collider[Layout.Length];
        
        for(int r = 0; r < Rows; r++) 
        {
            for(int c = 0; c < Columns; c++) 
            {
                var x = (int)Origin.X + (LayoutDirection == LayoutDirection.Horizontal ? c : r) * width;
                var y = (int)Origin.Y + (LayoutDirection == LayoutDirection.Horizontal ? r : c) * height;
                var index = r * Columns + c;
                
                Colliders[index] = new Collider(x, y, width, height, Layout[r, c], isLogicActive);
            }
        }
    }
    
    // methods
    
    // boolean checks
    
    public bool HasCollider(int row, int col) 
    {
        int amount = row * col;
        if (int.IsNegative(amount) || amount >= Colliders.Length) return false;
        return true;
    }
    
    // reference methods for locating and changing colliders.
    
    public ref Collider GetNeighbouringTopCollider(int index) 
    {
        var top = index - Columns;
        if (int.IsNegative(top) || top >= Colliders.Length) return ref Colliders[index];
        return ref Colliders[top];
    }
    
    public ref Collider GetNeighbouringTopCollider(int row, int col) 
    {
        var index = row * Columns + col;
        return ref GetNeighbouringTopCollider(index);
    }
    
    public ref Collider GetNeighbouringBottomCollider(int index) 
    {
        var bottom = index + Columns;
        if (int.IsNegative(bottom) || bottom >= Colliders.Length) return ref Colliders[index];
        return ref Colliders[bottom];
    }
    
    public ref Collider GetNeighbouringBottomCollider(int row, int col) 
    {
        var index = row * Columns + col;
        return ref GetNeighbouringBottomCollider(index);
    }
    
    public ref Collider GetNeighbouringLeftCollider(int index) 
    {
        int left = index - 1;
        if (int.IsNegative(left) || left >= Colliders.Length || (Colliders[left].Bounds.Y != Colliders[index].Bounds.Y)) return ref Colliders[index];
        return ref Colliders[left];
    }
    
    public ref Collider GetNeighbouringLeftCollider(int row, int col) 
    {
        var index = row * Columns + col;
        return ref GetNeighbouringLeftCollider(index);
    }
    
    public ref Collider GetNeighbouringRightCollider(int index) 
    {
        var right = index + 1;
        if (int.IsNegative(right) || right >= Colliders.Length || (Colliders[right].Bounds.Y != Colliders[index].Bounds.Y)) return ref Colliders[index];
        return ref Colliders[right];
    }
    
    public ref Collider GetNeighbouringRightCollider(int row, int col) 
    {
        var index = row * Columns + col;
        return ref GetNeighbouringRightCollider(index);
    }
    
    // setting up layer ID
    
    public void SetLayerID(int index, byte newID) 
    {
        if (index < 0 || index > Colliders.Length || newID < 0) return;
        Colliders[index].LayerID = newID;
    }
    
    public void SetLayerID(HashSet<int> selectedIndices, byte newID) 
    {
        if (selectedIndices.Count == 0) return;
        
         for(int i = 0; i < Colliders.Length; i++)  
         {
             if (selectedIndices.Contains(Colliders[i].LayerID)) SetLayerID(i, newID);
         }
    }
    
    public void SetWildLayerID(byte newID) 
    {
        for(int i = 0; i < Colliders.Length; i++) 
        {
            SetLayerID(i, newID);
        }
    }
    
    // Toggling collision for Colliders
    
    public void ToggleCollision(int index, bool isActive) 
    {
        if (index < 0 || index > Colliders.Length) return;
        Colliders[index].IsActive = isActive;
    }
    
    public void ToggleCollision(HashSet<int> selectedIndices, bool isActive) 
    {
        if (selectedIndices.Count == 0) return;
        
         for(int i = 0; i < Colliders.Length; i++)  
         {
             if (selectedIndices.Contains(Colliders[i].LayerID)) ToggleCollision(i, isActive);
         }
    }
    
    public void ToggleWildCollision(bool isActive) 
    {
        for(int i = 0; i < Colliders.Length; i++) 
        {
            ToggleCollision(i, isActive);
        }
    }
    
    // changing and setting new bounding boxes for Colliders.
    
    public void SetBoundingBox(int index, Rectangle newBounds, bool addLocationToOriginal = true) 
    {
        if (index < 0 || index >= Colliders.Length) return;
        
        var newRect = new Collider(addLocationToOriginal ? Colliders[index].Bounds.X + newBounds.X : newBounds.X,
                                    addLocationToOriginal ? Colliders[index].Bounds.Y + newBounds.Y : newBounds.Y, newBounds.Width, newBounds.Height,
                                    Colliders[index].LayerID, Colliders[index].IsActive);
        
        Colliders[index] = newRect;
    }
    
    public void SetBoundingBox(HashSet<int> selectedIndices, Rectangle bounds, bool addLocationToOriginal = true) 
    {
        if (selectedIndices.Count == 0) return;
        
        for(int i = 0; i < Colliders.Length; i++) 
        {
            if (selectedIndices.Contains(Colliders[i].LayerID)) SetBoundingBox(i, bounds, addLocationToOriginal);
        }
    }
    
    public void SetAllBoundingBoxes(Rectangle bounds, bool addLocationToOriginal = true) 
    {
        for (int i = 0; i < Colliders.Length; i++) 
        {
            SetBoundingBox(i, bounds, addLocationToOriginal);
        }
    }
    
    // main update loop.
    
    public virtual void Update(Logic logic) 
    {
        if (!IsLogicActive) return;
        
        for(int index = 0; index < Colliders.Length; index++) 
        {
            ref var collider = ref Colliders[index];
            
            if (!collider.IsActive) continue;
            
            logic(index, ref collider);
        }
    }
    
    // this is a simple update method that allows manual usage of for-loop statements, it can be used for optmizing collisions.
    
    public virtual void Update(Logic logic, int manualIndex) 
    {
        if (!IsLogicActive) return;
        
        ref var collider = ref Colliders[manualIndex];
        
        if (!collider.IsActive) return;
        
        logic(manualIndex, ref collider);
    }
}