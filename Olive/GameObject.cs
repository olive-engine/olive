using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Olive.SceneManagement;

namespace Olive;

/// <summary>
///     Represents an object which exists in a scene.
/// </summary>
public sealed class GameObject : IDisposable
{
    private readonly List<Coroutine> _coroutines = new();
    private readonly List<Component> _components = new();
    private Transform? _transform; // lazy load of Transform component
    private string _name;
    private bool _activeSelf = true;

    /// <summary>
    ///     Initializes a new instance of the <see cref="GameObject" /> class. 
    /// </summary>
    /// <param name="owningScene">The owning scene.</param>
    /// <exception cref="ArgumentNullException"><paramref name="owningScene" /> is <see langword="null" />.</exception>
    public GameObject(Scene owningScene)
    {
        OwningScene = owningScene ?? throw new ArgumentNullException(nameof(owningScene));
        owningScene.AddGameObject(this);
        AddComponent<Transform>();
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
            AssertNonDisposed();

            if (Transform.Parent is { } parent)
            {
                return ActiveSelf && parent.GameObject.ActiveInHierarchy;
            }

            return ActiveSelf;
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
    ///     Gets a value indicating whether this component has been disposed by calling <see cref="Dispose" />.
    /// </summary>
    /// <value><see langword="true" /> if the component has been disposed; otherwise, <see langword="false" />.</value>
    public bool IsDisposed { get; private set; }

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
    ///     Gets the scene which owns this game object.
    /// </summary>
    /// <value>The scene which owns this game object.</value>
    public Scene OwningScene { get; }

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
    ///     Gets a read-only view of the components in this handler.
    /// </summary>
    /// <value>A read-only view of the components.</value>
    public IReadOnlyCollection<Component> Components => _components.AsReadOnly();

    /// <summary>
    ///     Adds a new component to this handler.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The newly-added component.</returns>
    public T AddComponent<T>() where T : Component
    {
        return (T) AddComponent(typeof(T));
    }

    /// <summary>
    ///     Adds a new component to this handler.
    /// </summary>
    /// <param name="factory">The factory to invoke in order to fetch the necessary component.</param>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The newly-added component.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="factory" /> is <see langword="null" />.</exception>
    /// <exception cref="InvalidCastException"><paramref name="factory" /> returned invalid component (possibly <see langword="null" />).</exception>
    public T AddComponent<T>(Func<T> factory) where T : Component
    {
        AssertNonDisposed();

        if (factory is null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        if (factory.Invoke() is not { } component)
        {
            throw new InvalidCastException("Factory did not return valid behavior. (Perhaps it returned null?)");
        }

        _components.Add(component);
        component.GameObject = this;
        if (component is Behavior behavior) behavior.Initialize();
        return component;
    }

    /// <summary>
    ///     Adds a new component to this handler.
    /// </summary>
    /// <param name="type">The component type.</param>
    /// <returns>The newly-added component.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="type" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException">
    ///     <para><paramref name="type" /> does is abstract.</para>
    ///     -or-
    ///     <para><paramref name="type" /> does not inherit <see cref="Component" />.</para>
    /// </exception>
    public Component AddComponent(Type type)
    {
        AssertNonDisposed();

        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (type.IsAbstract)
        {
            throw new ArgumentException("Type cannot be abstract", nameof(type));
        }

        if (!type.IsSubclassOf(typeof(Component)))
        {
            throw new ArgumentException($"Type does not inherit {typeof(Component)}");
        }

        var component = (Component) Activator.CreateInstance(type)!;
        _components.Add(component);
        component.GameObject = this;
        if (component is Behavior behavior) behavior.Initialize();
        return component;
    }

    /// <summary>
    ///     Gets the specified component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>All components whose type matches <typeparamref name="T" />.</returns>
    public IEnumerable<T> EnumerateComponents<T>()
    {
        AssertNonDisposed();
        return _components.OfType<T>();
    }

    /// <summary>
    ///     Gets the specified component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The component whose type matches <typeparamref name="T" />, or <see langword="null" /> on failure.</returns>
    public T? GetComponent<T>()
    {
        AssertNonDisposed();
        return _components.OfType<T>().FirstOrDefault();
    }

    /// <summary>
    ///     Gets the specified component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>All components whose type matches <typeparamref name="T" />.</returns>
    public T[] GetComponents<T>()
    {
        return EnumerateComponents<T>().ToArray();
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
        AssertNonDisposed();

        foreach (Component component in _components.ToArray())
        {
            component.Dispose(true);
        }

        IsDisposed = true;
    }

    internal void RemoveComponent<T>(T component, bool force = false) where T : Component
    {
        if (component is Transform && !force)
        {
            throw new InvalidOperationException("Cannot remove the Transform component from a game object.");
        }

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

    internal void Update(FrameContext context)
    {
        foreach (Behavior behavior in _components.OfType<Behavior>())
        {
            behavior.Update(context);
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
            {
                instructions.Push(next);
            }
        }
    }

    private void AssertNonDisposed()
    {
        if (IsDisposed)
        {
            throw new ObjectDisposedException(_name);
        }
    }
}
