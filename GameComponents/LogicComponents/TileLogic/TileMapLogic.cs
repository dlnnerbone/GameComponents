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
        Colliders = new Collider[Rows * Columns];
        
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
        Colliders = new Collider[Rows * Columns];
        
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
    
    // main update loop.
    
    public void Update(Logic logic) 
    {
        if (!IsLogicActive) return;
        
        for(int index = 0; index < Colliders.Length; index++) 
        {
            ref var collider = ref Colliders[index];
            
            if (!collider.IsActive) return;
            
            logic(index, ref collider);
        }
    }
    
}