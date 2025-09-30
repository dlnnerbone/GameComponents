using Microsoft.Xna.Framework;
namespace GameComponents;
public static class Diagnostics 
{
    public static void Write(string debugMsg) 
    {
        System.Diagnostics.Debug.WriteLine(debugMsg);
    }
    
}