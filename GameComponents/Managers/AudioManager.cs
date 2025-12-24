using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameComponents.Managers;

public class AudioManager
{
    public readonly Dictionary<string, SoundEffect> Sounds;
    public static readonly Dictionary<string, Song> Songs = new();
    public int TotalSoundEffectCount => Sounds.Count;
    public int TotalSongCount => Songs.Count;
    
    // statics (GameHasControl, IsRepeating)
    public static bool IsMuted { get => MediaPlayer.IsMuted; set => MediaPlayer.IsMuted = value; }
    public static bool IsRepeating { get => MediaPlayer.IsRepeating; set => MediaPlayer.IsRepeating = value; }
    public static bool IsShuffled { get => MediaPlayer.IsShuffled; set => MediaPlayer.IsShuffled = value; }
    public static bool IsVisualizationEnabled { get => MediaPlayer.IsVisualizationEnabled; }
    public static bool GameHasControl { get => MediaPlayer.GameHasControl; }
    
    public AudioManager() 
    {
        Sounds = new Dictionary<string, SoundEffect>();
    }
    // adding songs
    public void AddSoundEffect(string name, SoundEffect effect) 
    {
        if (Sounds.TryAdd(name, effect)) return;
    }
    
    public static void AddSong(string name, Song song) 
    {
        if (Songs.TryAdd(name, song)) return;
    }
    
    // removing
    
    public void RemoveSound(string name, bool dispose = true)
    {
        if (Sounds.TryGetValue(name, out var sound)) 
        {
            if (dispose) sound.Dispose();
            Sounds.Remove(name);
        }
    }
    
    public static void RemoveSong(string name, bool dispose = true)
    {
        if (Songs.TryGetValue(name, out var song)) 
        {
            if (dispose) song.Dispose();
            Songs.Remove(name);
        }
    }
    
    // playing
    
    public void PlaySound(string name) 
    {
        if (!Sounds.ContainsKey(name)) throw new ArgumentOutOfRangeException($"{name} does not exist as a key in the dictionary.");
        Sounds[name].Play();
    }
    
    public void PlaySound(string name, float pan, float pitch, float volume, bool isLooping = false) 
    {
        if (!Sounds.ContainsKey(name)) throw new ArgumentOutOfRangeException($"{name} does not exist as a key in the dictionary.");
        
        SoundEffectInstance effect = Sounds[name].CreateInstance();
        effect.Pan = pan;
        effect.Pitch = pitch;
        effect.Volume = volume;
        effect.IsLooped = isLooping;
        
        effect.Play();
    }
    
    public static void PlaySong(string name, float volume = 0.5f) 
    {
        MediaPlayer.Volume = volume;
        
        if (MediaPlayer.State == MediaState.Playing) MediaPlayer.Stop();
        MediaPlayer.Play(Songs[name]);
    }
    
    
}