using Microsoft.Xna.Framework;
using System.IO;
using System.Text.Json;
namespace GameComponents.Helpers;

public static class FileManager 
{
    public static string Serialize<T>(T entity) 
    {
        if (entity == null) throw new ArgumentNullException($"{nameof(entity)}");
        return JsonSerializer.Serialize(entity);
    }
    
    public static T? Deserialize<T>(string json) 
    {
        if (string.IsNullOrWhiteSpace(json)) throw new ArgumentException($"{nameof(json)} can not be null or empty.");
        return JsonSerializer.Deserialize<T>(json);
    }
    
    public static string ReadFromFile(string filePath) 
    {
        if (File.Exists(filePath)) throw new FileNotFoundException($"File could not be found: {filePath}");
        return File.ReadAllText(filePath);
    }
    
    public static void WriteToFile(string filePath, string contents) 
    {
        if (File.Exists(filePath)) throw new FileNotFoundException($"{filePath} does not exist or is invalid.");
        File.WriteAllText(filePath, contents);
    }
    
    // saving states
    
    public static void SaveToFile<T>(string filePath, T entity) 
    {
        if (File.Exists(filePath)) throw new FileNotFoundException($"{filePath} could not be found and therefore can be saved to.");
        WriteToFile(filePath, Serialize(entity));
    }
    
    public static T? LoadFromFile<T>(string filePath) 
    {
        if (File.Exists(filePath)) throw new FileNotFoundException($"{filePath} is invalid or does not exist.");
        return Deserialize<T>(ReadFromFile(filePath));
    }
}