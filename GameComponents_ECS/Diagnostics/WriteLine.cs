using System.Diagnostics;
namespace GameComponents;
public static class Diagnostics 
{
    public static void WriteLine(string str) 
    {
        Debug.WriteLine(str);
    }
}