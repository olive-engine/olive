using Microsoft.Xna.Framework;

namespace Olive.Components;

/// <summary>
///     Represents a component which provides a game object with 2-dimensional transform data.
/// </summary>
public sealed class Transform2D : Transform
{
    private Vector2 _localPosition;

    /// <summary>
    ///     Gets or sets the local-space position of this transform.
    /// </summary>
    /// <value>The local-space position of this transform.</value>
    public Vector2 LocalPosition
    {
        get
        {
            AssertNonDisposed();
            return _localPosition;
        }
        set
        {
            AssertNonDisposed();
            _localPosition = value;
        }
    }
}
