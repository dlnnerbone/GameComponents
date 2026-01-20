using System.Diagnostics;
namespace GameComponents;
public static partial class Diagnostics 
{
    public static void WriteLine(string str) 
    {
        Debug.WriteLine(str);
    }
}