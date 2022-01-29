using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

/// <summary>
///     Represents a class which provides methods and properties related to scene management.
/// </summary>
public abstract class SceneManager
{
    /// <summary>
    ///     Loads the specified scene with the current scene manager's strategy.
    /// </summary>
    /// <param name="scene">The scene to load.</param>
    public virtual void LoadScene(Scene scene)
    {
        scene.SceneManager = this;
    }

    /// <summary>
    ///     Unloads the specified scene with the current scene manager's strategy.
    /// </summary>
    /// <param name="scene">The scene to unload.</param>
    public abstract void UnloadScene(Scene scene);

    protected internal abstract void Draw(GameTime gameTime);

    protected internal virtual void Initialize()
    {
        if (OliveEngine.CurrentGame is not null)
        {
            GL.Initialize(OliveEngine.CurrentGame.GraphicsDevice);
        }
    }

    internal abstract void LoadContent();

    protected internal virtual void OnPostRender()
    {
    }

    protected internal abstract void Update(FrameContext context);
}
