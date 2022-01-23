using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

public sealed class SimpleSceneManager : SceneManager
{
    /// <summary>
    ///     Gets the currently active scene.
    /// </summary>
    /// <value>The currently active scene.</value>
    public Scene CurrentScene { get; protected internal set; } = new EmptyScene();

    protected internal override void Draw(GameTime gameTime)
    {
        CurrentScene?.Draw(gameTime);
    }

    protected internal override void Initialize()
    {
        CurrentScene?.Initialize();
    }

    protected internal override void Update(GameTime gameTime)
    {
        CurrentScene?.Update(gameTime);
    }
}
