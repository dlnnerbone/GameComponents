using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameComponents.Interfaces;
namespace GameComponents.Rendering;
public class SpriteText : ISpriteText 
{
    private Vector2 textPosition = Vector2.Zero;
    private Color textColor = Color.White;
    private string text;
    // private fields
    public Vector2 TextPosition { get => textPosition; set => textPosition = value; }
    public Color TextColor { get => textColor; set => textColor = value; }
    public SpriteFont SpriteFont { get; set; }
    public string Text { get => text; set => text = value; }
    public SpriteText(ContentManager content, string pathToFont, string text, Vector2 textPosition) 
    {
        if (content == null) throw new ArgumentNullException($"{nameof(content)} could not be found or is null.");
        if (pathToFont == string.Empty || pathToFont == null) throw new ArgumentNullException($"{pathToFont} is either not found or does not exist.");
        if (text == null) throw new ArgumentNullException($"text can not be null, if you want no text then use 'string.Empty' instead.");

        SpriteFont = content.Load<SpriteFont>(pathToFont);
        this.text = text;
        TextPosition = textPosition;
    }
    
    public void ChangeText(string newText) => Text = newText;
    
    public void DrawText(SpriteBatch batch) 
    {
        batch.DrawString(SpriteFont, Text, TextPosition, TextColor);
    }
}