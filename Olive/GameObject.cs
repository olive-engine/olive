using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Olive.Components;
using Olive.SceneManagement;

namespace Olive;

/// <summary>
///     Represents an object which exists in a scene.
/// </summary>
public sealed class GameObject : IDisposable
{
    private readonly List<Coroutine> _coroutines = new();
    private readonly List<Component> _components = new();
    private bool _isDisposed = false;
    private Transform? _transform; // lazy load of Transform component
    private string _name;
    private bool _activeSelf;

    public GameObject(Scene owningScene)
    {
        if (owningScene is null) throw new ArgumentNullException(nameof(owningScene));

        owningScene.AddGameObject(this);
        AddComponent<Transform3D>();
    }

    /// <summary>
    ///     Gets a value indicating whether this game object is active in the hierarchy.
    /// </summary>
    /// <value><see langword="true" /> if this game object is active in the hierarchy, otherwise <see langword="false" />.</value>
    /// <remarks>
    ///     A game object is considered "active in the hierarchy" if <see cref="ActiveSelf" /> on the object and all of its
    ///     parents are <see langword="true" />
    /// </remarks>
    public bool ActiveInHierarchy
    {
        get
        {
            Transform? parent = Transform.Parent;
            if (parent is null) return ActiveSelf;

            do
            {
                if (!parent.GameObject.ActiveSelf)
                    return false;

                parent = parent.Parent;
            } while (parent != null);

            return true;
        }
    }

    /// <summary>
    ///     Gets a value indicating whether this game object is active.
    /// </summary>
    /// <value><see langword="true" /> if this game object is active, otherwise <see langword="false" />.</value>
    public bool ActiveSelf
    {
        get
        {
            AssertNonDisposed();
            return _activeSelf;
        }
    }

    /// <summary>
    ///     Gets or sets the name of this game object.
    /// </summary>
    /// <value>The name of this game object.</value>
    public string Name
    {
        get
        {
            AssertNonDisposed();
            return _name;
        }
        set
        {
            AssertNonDisposed();
            _name = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }
    }

    /// <summary>
    ///     Gets the transform component attached to this game object.
    /// </summary>
    /// <value>The transform component.</value>
    public Transform Transform
    {
        get
        {
            AssertNonDisposed();

            _transform ??= GetComponent<Transform>();

            // we can safely assume that GetComponent<Transform> will not be null.
            // because if it is, something is tremendously fucked. all GOs should have a Transform
            Trace.Assert(_transform is not null);
            return _transform!;
        }
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <param name="componentFactory">The factory to invoke in order to build the component.</param>
    /// <returns>The newly-added component.</returns>
    public T AddComponent<T>(Func<T> componentFactory) where T : Component
    {
        AssertNonDisposed();

        if (typeof(T).IsSubclassOf(typeof(Transform)))
        {
            throw new ArgumentException("Cannot add multiple transforms to one game object.");
        }

        T component = componentFactory.Invoke();
        component.GameObject = this;

        _components.Add(component);
        if (component is Behavior behavior) behavior.Awake();
        return component;
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>The newly-added component.</returns>
    public T AddComponent<T>() where T : Component, new()
    {
        return (T) AddComponent(typeof(T));
    }

    /// <summary>
    ///     Adds the specified component type to the game object.
    /// </summary>
    /// <param name="componentType">The type of the component to add.</param>
    /// <returns>The newly-added component.</returns>
    public Component AddComponent(Type componentType)
    {
        AssertNonDisposed();

        if (!componentType.IsSubclassOf(typeof(Component)))
        {
            throw new ArgumentException($"Type does not inherit {typeof(Component)}");
        }

        if (componentType.IsAbstract)
        {
            throw new ArgumentException("Component is abstract");
        }

        if (componentType == typeof(Transform2D) && GetComponent<Transform2D>() is null)
        {
            // Transform2D overrides Transform3D
            foreach (Transform3D existingTransform in GetComponents<Transform3D>())
            {
                RemoveComponent(existingTransform, true);
                existingTransform.Dispose();
            }
        }

        if (componentType.IsSubclassOf(typeof(Transform)) && GetComponent<Transform>() is not null)
        {
            throw new ArgumentException("Cannot add multiple transforms to one game object.");
        }

        var component = (Component) Activator.CreateInstance(componentType)!;
        component.GameObject = this;

        _components.Add(component);
        if (component is Behavior behavior) behavior.Awake();

        return component;
    }

    /// <summary>
    ///     Gets a component of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The component whose type matches <typeparamref name="T" />, or <see langword="null" /> if no match was found.</returns>
    public T? GetComponent<T>()
    {
        AssertNonDisposed();
        return _components.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    ///     Gets all components of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component.</typeparam>
    /// <returns>The components whose type matches <typeparamref name="T" />.</returns>
    public T[] GetComponents<T>()
    {
        AssertNonDisposed();
        return _components.OfType<T>().ToArray();
    }

    /// <summary>
    ///     Sets whether or not this game object is active.
    /// </summary>
    /// <param name="active"><see langword="true" /> if this game object should be activated, otherwise <see langword="false" />.</param>
    public void SetActive(bool active)
    {
        AssertNonDisposed();
        _activeSelf = active;
    }

    /// <summary>
    ///     Attempts to get a component, and returns the result of that attempt.
    /// </summary>
    /// <param name="component">The destination of the component instance.</param>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns><see langword="true" /> if the component was found, otherwise <see langword="false" />.</returns>
    public bool TryGetComponent<T>([NotNullWhen(true)] out T? component)
    {
        AssertNonDisposed();
        return (component = GetComponent<T>()) is not null;
    }

    /// <summary>
    ///     Disposes all resources allocated by this game object, and removes the object from the scene. 
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed) return;

        // TODO destroy game object
        _isDisposed = true;
    }

    internal void RemoveComponent<T>(T component, bool force = false) where T : Component
    {
        if (component is Transform && !force)
            throw new InvalidOperationException("Cannot remove the Transform component from a game object.");

        _components.Remove(component);
    }

    internal Coroutine StartCoroutine(IEnumerator enumerator)
    {
        var coroutine = new Coroutine(enumerator);
        _coroutines.Add(coroutine);
        return coroutine;
    }

    internal void StopCoroutine(Coroutine coroutine)
    {
        _coroutines.Remove(coroutine);
    }

    internal void Update(GameTime gameTime)
    {
        foreach (Behavior behavior in _components.OfType<Behavior>())
        {
            behavior.Update();
        }

        for (var index = 0; index < _coroutines.Count; index++)
        {
            Coroutine coroutine = _coroutines[index];
            Stack<IEnumerator> instructions = coroutine.CallStack;

            if (instructions.Count == 0)
            {
                _coroutines.RemoveAt(index);
                index--;
                continue;
            }

            IEnumerator instruction = instructions.Peek();
            if (!instruction.MoveNext())
            {
                instructions.Pop();
                continue;
            }

            if (instruction.Current is IEnumerator next && instruction != next)
                instructions.Push(next);
        }
    }

    private void AssertNonDisposed()
    {
        if (_isDisposed)
            throw new ObjectDisposedException(Name);
    }
}
