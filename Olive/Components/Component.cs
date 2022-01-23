using System.Diagnostics.CodeAnalysis;

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
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <param name="componentFactory">The factory to invoke in order to build the component.</param>
    /// <returns>The newly-added component.</returns>
    public T AddComponent<T>(Func<T> componentFactory) where T : Component
    {
        return GameObject.AddComponent(componentFactory);
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>The newly-added component.</returns>
    public T AddComponent<T>() where T : Component, new()
    {
        return GameObject.AddComponent<T>();
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <param name="componentType">The type of the component to add.</param>
    /// <returns>The newly-added component.</returns>
    public Component AddComponent(Type componentType)
    {
        return GameObject.AddComponent(componentType);
    }

    /// <summary>
    ///     Disposes all resources allocated by this component, and removes it from the owning object. 
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed) throw new ObjectDisposedException(GetType().Name);

        GameObject.RemoveComponent(this);
        _isDisposed = true;
    }

    /// <summary>
    ///     Gets a component of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The component whose type matches <typeparamref name="T" />, or <see langword="null" /> if no match was found.</returns>
    public T? GetComponent<T>()
    {
        return GameObject.GetComponent<T>();
    }

    /// <summary>
    ///     Gets all components of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The components whose type matches <typeparamref name="T" />.</returns>
    public T[] GetComponents<T>()
    {
        return GameObject.GetComponents<T>();
    }

    /// <summary>
    ///     Attempts to get a component, and returns the result of that attempt.
    /// </summary>
    /// <param name="component">The destination of the component instance.</param>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns><see langword="true" /> if the component was found, otherwise <see langword="false" />.</returns>
    public bool TryGetComponent<T>([NotNullWhen(true)] out T? component)
    {
        return GameObject.TryGetComponent(out component);
    }

    private protected void AssertNonDisposed()
    {
        if (_isDisposed)
            throw new ObjectDisposedException(GetType().Name);
    }
}
