namespace Olive.Components;

/// <summary>
///     Represents a component which can be attached to game objects.
/// </summary>
public abstract class Component : IDisposable
{
    private bool _isDisposed = false;

    /// <summary>
    ///     Gets the game object to which this component is attached.
    /// </summary>
    /// <value>The object to which this component is attached.</value>
    public GameObject GameObject { get; internal set; } = null!;

    /// <summary>
    ///     Disposes all resources allocated by this component, and removes it from the owning object. 
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed) throw new ObjectDisposedException(GetType().Name);

        GameObject.RemoveComponent(this);
        _isDisposed = true;
    }

    private protected void AssertNonDisposed()
    {
        if (_isDisposed)
            throw new ObjectDisposedException(GetType().Name);
    }
}
