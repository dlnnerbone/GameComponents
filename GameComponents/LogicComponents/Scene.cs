using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents.Managers;

public abstract class Scene : IDisposable
{
    public ContentManager? Content { get; private set; }
    public readonly string Name;
    public bool IsActive { get; set; } = true;
    public bool IsPaused { get; set; } = false;
    public bool IsDrawable { get; set; } = true;
    public bool IsLoaded { get; private set; } = false;
    public bool IsDisposed { get; private set; } = false;
    
    public Scene(string name) 
    {
        Name = name ?? throw new ArgumentNullException($"name is invalid or null.");
    }
    
    public virtual void Initialize(Game game) 
    {
        if (game == null) throw new ArgumentNullException($"{nameof(game)} is null or does not exist.");
        LoadContent(game);
    }
    
    protected virtual void LoadContent(Game game, string contentDir = "Content") 
    {
        if (IsDisposed) throw new ObjectDisposedException($"Can't load content because {this} is already disposed.");
        Content = new ContentManager(game.Content.ServiceProvider, contentDir);
        IsLoaded = true;
    }
    
    public virtual void UnloadContent() 
    {
        if (IsDisposed || !IsLoaded) return;
        Content?.Unload();
        IsLoaded = false;
    }
    
    public virtual void Update(GameTime gt) 
    {
        if (!IsActive || IsPaused || IsDisposed) return;
    }
    
    public virtual void DrawScene(SpriteBatch batch) 
    {
        if (!IsActive || !IsDrawable || IsDisposed) return;
    }
    
    // dispose method
    
    public void Dispose() 
    {
        if (IsDisposed) return;
        UnloadContent();
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }
}
