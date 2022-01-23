using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

/// <summary>
///     Represents a class which provides methods and properties related to scene management.
/// </summary>
public abstract class SceneManager
{
    /// <summary>
    ///     Gets the primary scene.
    /// </summary>
    /// <value>The primary scene.</value>
    public abstract Scene? PrimaryScene { get; protected internal set; }

    internal abstract void Draw(GameTime gameTime);

    internal abstract void Update(GameTime gameTime);
}
