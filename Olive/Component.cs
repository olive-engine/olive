using System.Diagnostics.CodeAnalysis;
using Olive.Components;

namespace Olive;

/// <summary>
///     Represents the base class for all components that can be attached to a game object.
/// </summary>
public abstract class Component : ICloneable, IDisposable
{
    private GameObject _gameObject = null!;

    protected Component()
    {
        OliveEngine.CreateComponent(this);
    }

    /// <summary>
    ///     Gets the game object to which this component is attached.
    /// </summary>
    /// <value>The game object to which this component is attached.</value>
    public GameObject GameObject
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _gameObject;
        }
        internal set => _gameObject = value;
    }

    /// <summary>
    ///     Gets the transform component attached to this game object.
    /// </summary>
    /// <value>The transform component.</value>
    public Transform Transform
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return GameObject.Transform;
        }
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <param name="componentFactory">The factory to invoke in order to build the component.</param>
    /// <returns>The newly-added component.</returns>
    public T AddComponent<T>(Func<T> componentFactory) where T : Component
    {
        OliveEngine.AssertNonDisposed(this);
        return GameObject.AddComponent(componentFactory);
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>The newly-added component.</returns>
    public T AddComponent<T>() where T : Component, new()
    {
        OliveEngine.AssertNonDisposed(this);
        return GameObject.AddComponent<T>();
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <param name="componentType">The type of the component to add.</param>
    /// <returns>The newly-added component.</returns>
    public Component AddComponent(Type componentType)
    {
        OliveEngine.AssertNonDisposed(this);
        return GameObject.AddComponent(componentType);
    }

    /// <summary>
    ///     Disposes all resources allocated by this game object, and removes the object from the scene. 
    /// </summary>
    public void Dispose()
    {
        OliveEngine.AssertNonDisposed(this);
        GameObject.RemoveComponent(this);
        OliveEngine.DisposeComponent(this);
    }

    /// <summary>
    ///     Gets a component of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The component whose type matches <typeparamref name="T" />, or <see langword="null" /> if no match was found.</returns>
    public T? GetComponent<T>()
    {
        OliveEngine.AssertNonDisposed(this);
        return GameObject.GetComponent<T>();
    }

    /// <summary>
    ///     Gets all components of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The components whose type matches <typeparamref name="T" />.</returns>
    public T[] GetComponents<T>()
    {
        OliveEngine.AssertNonDisposed(this);
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
        OliveEngine.AssertNonDisposed(this);
        return GameObject.TryGetComponent(out component);
    }

    public virtual object Clone()
    {
        OliveEngine.AssertNonDisposed(this);
        return Activator.CreateInstance(GetType())!;
    }
}
