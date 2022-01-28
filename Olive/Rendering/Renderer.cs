using Microsoft.Xna.Framework;

namespace Olive.Rendering;

/// <summary>
///     Represents the base class for all renders.
/// </summary>
public abstract class Renderer : Component
{
    protected internal virtual void Render(GameTime gameTime, Matrix world, Matrix view, Matrix projection)
    {
    }
}
