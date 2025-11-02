using Microsoft.Xna.Framework;
namespace GameComponents;
public class TilePool 
{
    public readonly Stack<Tile> InactiveTiles = new Stack<Tile>();
    private readonly Tile _defaultTile;
    
    public TilePool(Tile defaultTile) 
    {
        _defaultTile = defaultTile;
    }
    public Tile FetchTile() 
    {
        if (InactiveTiles.Count > 0) 
        {
            return InactiveTiles.Pop();
        }
        else 
        {
            return _defaultTile;
        }
    }
    public void GiveTile(Tile tile) 
    {
        InactiveTiles.Push(tile);
    }
}
