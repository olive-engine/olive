namespace Olive.Components;

/// <summary>
///     Represents a component which is attached to all gameobjects by default, providing them with transform data.
/// </summary>
public class Transform : Component
{
    private Transform? _parent;

    /// <summary>
    ///     Gets or sets the parent to this transform.
    /// </summary>
    /// <value>The parent to this transform.</value>
    public Transform? Parent
    {
        get
        {
            AssertNonDisposed();
            return _parent;
        }
        set
        {
            AssertNonDisposed();
            _parent = value;
        }
    }
}
