using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameComponents.Managers;
public abstract class Core : Game 
{
    internal Core? s_instance;
    public SpriteBatch? SpriteBatch { get; private set; }
    public GraphicsDeviceManager? GraphicsDeviceManager { get; private set; }
    public bool ExitOnEscape { get; set; } = true;
    
    // private fields
    private readonly InputManager input;
    private Keys keyForExit = Keys.Escape;
    // methods
    protected void ChangeExitKey(Keys newKey) 
    {
        keyForExit = newKey;
    }
    // main methods
    protected override void Initialize() 
    {
        base.Initialize();
    }
    protected override void LoadContent() 
    {
        base.LoadContent();
        SpriteBatch = new(GraphicsDevice);
    }
    protected override void UnloadContent() 
    {
        UnloadContent();
    }
    protected override void Update(GameTime gt) 
    {
        base.Update(gt);
        input.UpdateInputs();
        if (ExitOnEscape && input.IsKeyPressed(keyForExit)) 
        {
            Exit();
        }
    }
    protected override void Draw(GameTime gt) 
    {
        base.Draw(gt);
    }
    // constructor
    public Core(string title, int width, int height, bool isFullScreen = false, bool isMouseVisible = true, string contentRootDir = "Content") 
    {
        if (s_instance != null) 
        {
            throw new InvalidOperationException("there can only be one Core class.");
        }
        
        Window.Title = title;
        
        GraphicsDeviceManager = new(this);
        GraphicsDeviceManager.PreferredBackBufferWidth = width;
        GraphicsDeviceManager.PreferredBackBufferHeight = height;
        GraphicsDeviceManager.ApplyChanges();

        input = new();

        IsMouseVisible = isMouseVisible;
        GraphicsDeviceManager.IsFullScreen = isFullScreen;

        Content.RootDirectory = contentRootDir;
    }
    
}