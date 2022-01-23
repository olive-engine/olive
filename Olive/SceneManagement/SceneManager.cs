using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

/// <summary>
///     Represents a class which provides methods and properties related to scene management.
/// </summary>
public abstract class SceneManager
{
    protected internal abstract void Draw(GameTime gameTime);

    protected internal abstract void Initialize();

    protected internal abstract void Update(GameTime gameTime);
}
