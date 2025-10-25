using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace GameComponents.Rendering;
public sealed class Animation
{
    private bool _isPlaying = true;
    private int currentFrameIndex;
    private float frameTime = 1;
    private int startingIndex = 0;
    private int endingIndex = 0;
    private float deltaTime;
    // public properties
    public readonly TextureAtlas SpriteSheet;
    public Rectangle[] FrameGallery => SpriteSheet.Regions;
    public bool IsLooping { get; set; } = true;
    public bool IsReversed { get; set; } = false;
    
    public int CurrentFrameIndex 
    {
        get => currentFrameIndex;
        set => currentFrameIndex = MathHelper.Clamp(value, startingIndex, endingIndex);
    }
    public int StartingIndex 
    {
        get => startingIndex;
        set => startingIndex = MathHelper.Clamp(value, 0, endingIndex);
    }
    public int EndingIndex 
    {
        get => endingIndex;
        set => endingIndex = MathHelper.Clamp(value, startingIndex, 10000);
    }
    public Frame CurrentFrame => FrameGallery[currentFrameIndex];
}