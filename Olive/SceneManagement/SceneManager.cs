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
    public abstract void LoadScene(Scene scene);
    
    /// <summary>
    ///     Unloads the specified scene with the current scene manager's strategy.
    /// </summary>
    /// <param name="scene">The scene to unload.</param>
    public abstract void UnloadScene(Scene scene);
    
    protected internal abstract void Draw(GameTime gameTime);

    protected internal abstract void Initialize();

    protected internal abstract void Update(GameTime gameTime);
}
