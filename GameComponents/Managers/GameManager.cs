using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace GameComponents.Managers;
public abstract class Scene 
{
    public virtual ContentManager SceneContent { get; }
}