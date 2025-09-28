using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents.Managers;
public abstract class Scene : IDisposable
{
    public ContentManager? SceneContent { get; protected set; }
    public bool IsActive { get; set; } = true;
    public bool IsDisposed { get; private set; }
    public bool IsLoaded { get; private set; }
    public string Name { get; protected set; } = string.Empty;
    public virtual void Initialize(Game game) 
    {
        if (game == null) throw new ArgumentNullException(nameof(game));
        LoadSceneContent(game);
    }
    public virtual void LoadSceneContent(Game game, string contentDir = "Content") 
    {
        if (game == null) throw new ArgumentNullException("Scene's game argumenet can not be null.");
        if (IsDisposed) throw new ObjectDisposedException(Name);
        SceneContent = new ContentManager(game.Content.ServiceProvider, contentDir);
        IsLoaded = true;
    }
    //
    public virtual void UnloadContent() 
    {
        if (!IsDisposed) return;
        SceneContent?.Unload();
        IsLoaded = false;
    }
    //
    public virtual void UpdateScene(GameTime gt) 
    {
        if (!IsActive || IsDisposed) return;
    }
    public virtual void DrawScene(SpriteBatch batch) 
    {
        if (!IsActive || IsDisposed) return;
    }
    // disposing the scene
    public void Dispose() 
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    public virtual void Dispose(bool disposing) 
    {
        if (IsDisposed) return;
        if (disposing) 
        {
            UnloadContent();
            SceneContent?.Dispose();
        }
        IsDisposed = true;
    } 
}