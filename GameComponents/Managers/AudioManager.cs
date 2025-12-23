using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameComponents.Managers;

public class AudioManager
{
    public readonly Dictionary<string, SoundEffect> Sounds;
    public readonly Dictionary<string, Song> Songs;
    public int TotalSoundEffectCount => Sounds.Count;
    public int TotalSongCount => Songs.Count;
    
    public AudioManager() 
    {
        Sounds = new Dictionary<string, SoundEffect>();
        Songs = new Dictionary<string, Song>();
    }
    // adding songs
    public void AddSoundEffect(string name, SoundEffect effect) 
    {
        if (Sounds.TryAdd(name, effect)) return;
        
        Sounds.Add(name, effect);
    }
    
    public void AddSong(string name, Song song) 
    {
        if (Songs.TryAdd(name, song)) return;
        
        Songs.Add(name, song);
    }
    
    // removing
    
    public void RemoveSound(string name, bool dispose = true) 
    {
        if (!Sounds.ContainsKey(name)) return;
        else if (dispose) Sounds[name].Dispose();
        Sounds.Remove(name);
    }
    
    
}