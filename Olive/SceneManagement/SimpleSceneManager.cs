using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

public sealed class SimpleSceneManager : SceneManager
{
    /// <summary>
    ///     Gets the currently active scene.
    /// </summary>
    /// <value>The currently active scene.</value>
    public override Scene PrimaryScene { get; protected internal set; } = new EmptyScene();

    internal override void Draw(GameTime gameTime)
    {
        PrimaryScene?.Draw(gameTime);
    }
}
