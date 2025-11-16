using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace GameComponents.Rendering;
public sealed class Animation : TextureDependencies
{
    // private fields
    private bool _isPlaying = true;
    private float displayTime = 0, deltaTime = 0, deltaMulti = 1f;
    private int startingInt = 0, endingInt = 1, currentFrameIndex = 0;
    // public properties
    public readonly TextureAtlas RegionAtlas;
    public readonly Texture2D SpriteSheet;

    public bool IsLooping { get; set; } = true;
    public bool IsReversed { get; set; } = false;
    public float DisplayTime { get => displayTime; set => displayTime = MathHelper.Clamp(value, 0f, 1f); }
    public float DeltaTime => deltaTime;
    public float DeltaMultiplier { get => deltaMulti; set => deltaMulti = Math.Abs(value); }
    public float FPS { get => 1 / DisplayTime; set => DisplayTime = value <= 0 ? 1 : 1 / value; }
    public readonly Dictionary<string, Point> Presets = new Dictionary<string, Point>();
    
    public Rectangle CurrentFrame => RegionAtlas.Regions[CurrentFrameIndex];
    
    public int StartingIndex 
    {
        get => startingInt;
        set => startingInt = MathHelper.Clamp(value, 0, EndingIndex);
    }
    public int EndingIndex 
    {
        get => endingInt;
        set => endingInt = value <= StartingIndex ? StartingIndex : value;
    }
    public int CurrentFrameIndex 
    {
        get => currentFrameIndex;
        set => currentFrameIndex = MathHelper.Clamp(value, StartingIndex, EndingIndex);
    }
    // general methods
    public void Play() => _isPlaying = true;
    public void Stop() => _isPlaying = false;
    public void GoTo(int frame) => CurrentFrameIndex = frame;
    
    public void SetRange(int startingIndex, int endingIndex) 
    {
        StartingIndex = startingIndex;
        EndingIndex = endingIndex;
    }
    
    public void AddPreset(string presetName, int presetStartingIndex, int presetEndingIndex) 
    {
        if (string.IsNullOrEmpty(presetName)) throw new ArgumentNullException($"{presetName} does not exist or is null (Invalid)");
        else if (int.IsNegative(presetStartingIndex)) throw new ArgumentException($"{presetStartingIndex} is negative.");
        else if (presetEndingIndex <= presetStartingIndex) throw new ArgumentException($"{presetEndingIndex} is less than startingInt.");
        
        Presets.Add(presetName, new Point(presetStartingIndex, presetEndingIndex));
    }
    
    public void SetToPreset(string presetName) 
    {
        if (!Presets.ContainsKey(presetName)) throw new ArgumentOutOfRangeException($"{presetName} does not exist in the dictionary.");
        
        StartingIndex = Presets[presetName].X;
        EndingIndex = Presets[presetName].Y;
    }
    
    public void Restart(bool restartToEndingIndex = false) 
    {
        if (restartToEndingIndex) CurrentFrameIndex = EndingIndex;
        else CurrentFrameIndex = StartingIndex;
        deltaTime = 0;
    }
    // helper methods
    public float NormalizedAnimationProgress 
    {
        get 
        {
            if (EndingIndex <= 0) return 1f;
            return (CurrentFrameIndex - StartingIndex) / (EndingIndex - StartingIndex);
        }
    }
    public float NormalizedProgress => deltaTime / DisplayTime;
    public int AmountOfPresets => Presets.Count;
    public bool IsAnimationDone => !IsReversed ? CurrentFrameIndex >= EndingIndex : CurrentFrameIndex <= StartingIndex;
    // main Update Method.
    private void _reversed(GameTime gt) 
    {
        deltaTime -= (float)gt.ElapsedGameTime.TotalSeconds * DeltaMultiplier;
        
        if (deltaTime <= 0) 
        {
            CurrentFrameIndex--;
            deltaTime = DisplayTime;
        }
        
        if (IsLooping && CurrentFrameIndex <= StartingIndex) 
        {
            CurrentFrameIndex = EndingIndex;
        }
        else if (!IsLooping && CurrentFrameIndex <= StartingIndex) 
        {
            CurrentFrameIndex = StartingIndex;
        }
    }
    private void _forward(GameTime gt) 
    {
        deltaTime += (float)gt.ElapsedGameTime.TotalSeconds * DeltaMultiplier;
        
        if (DeltaTime >= DisplayTime) 
        {
            CurrentFrameIndex++;
            deltaTime = 0;
        }
        
        if (IsLooping && CurrentFrameIndex >= EndingIndex) 
        {
            CurrentFrameIndex = StartingIndex;
        }
        else if (!IsLooping && CurrentFrameIndex >= EndingIndex) 
        {
            CurrentFrameIndex = EndingIndex;
        }
    }
    public void Advance(GameTime gt) 
    {
        if (!_isPlaying) return;
        
        if (IsReversed) _reversed(gt);
        else _forward(gt);
    }
    // main Draw Method(s)
    public void Animate(SpriteBatch batch, Rectangle destinationRectangle) 
    {
        batch.Draw(SpriteSheet, destinationRectangle, CurrentFrame, Color, Radians, Origin, Effects, LayerDepth);
    }
    public void Animate(SpriteBatch batch, Vector2 position) 
    {
        batch.Draw(SpriteSheet, position, CurrentFrame, Color, Radians, Origin, Scale, Effects, LayerDepth);
    }
    // Constructors
    public Animation(Texture2D texture, TextureAtlas atlas, int starting, int ending, float fps) 
    {
        SpriteSheet = texture;
        RegionAtlas = atlas;
        FPS = fps;
        StartingIndex = starting;
        EndingIndex = ending;
    }
}