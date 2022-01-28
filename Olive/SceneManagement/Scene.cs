using System.Diagnostics;
using Microsoft.Xna.Framework;
using Olive.Rendering;

namespace Olive.SceneManagement;

/// <summary>
///     Represents a scene.
/// </summary>
public abstract class Scene
{
    private readonly List<GameObject> _gameObjects = new();
    private Camera _mainCamera;

    protected Scene()
    {
        if (this is EmptyScene)
        {
            return;
        }

        MainCamera = new GameObject(this).AddComponent<Camera>();
    }

    /// <summary>
    ///     Gets a read-only view of the game objects currently in the scene.
    /// </summary>
    /// <value>A read-only view of the game objects currently in the scene.</value>
    public IReadOnlyCollection<GameObject> GameObjects => _gameObjects.AsReadOnly();

    /// <summary>
    ///     Gets or sets the main camera of this scene.
    /// </summary>
    /// <value>The main camera.</value>
    public Camera MainCamera
    {
        get => _mainCamera;
        set => _mainCamera = Camera.Main = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    ///     Gets the current scene manager.
    /// </summary>
    /// <value>The scene manager.</value>
    public SceneManager SceneManager { get; internal set; } = null!;

    /// <summary>
    ///     Called when the scene is being initialized.
    /// </summary>
    protected internal virtual void Initialize()
    {
    }

    /// <summary>
    ///     Called once per frame.
    /// </summary>
    protected internal virtual void Update(GameTime gameTime)
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.IsDisposed || !gameObject.ActiveInHierarchy)
            {
                continue;
            }

            gameObject.Update(gameTime);
        }
    }

    /// <summary>
    ///     Loads content into the scene.
    /// </summary>
    /// <param name="assetName">The name of the asset to import.</param>
    /// <typeparam name="T">The asset type.</typeparam>
    /// <returns>The loaded asset.</returns>
    public T LoadContent<T>(string assetName)
    {
        if (OliveEngine.CurrentGame is { } game)
        {
            return game.Content.Load<T>(assetName);
        }

        Trace.Assert(false, "Impossible state. Game is null."); // if we're here, something is horrendously fucked.
        return default!;
    }

    protected internal virtual void LoadContent()
    {
    }

    protected internal virtual void OnPostRender()
    {
    }

    internal virtual void AddGameObject(GameObject gameObject)
    {
        if (_gameObjects.Contains(gameObject)) return;
        _gameObjects.Add(gameObject);
    }

    internal bool Draw(GameTime gameTime)
    {
        if (OliveEngine.CurrentGame?.GraphicsDevice is not { } graphicsDevice)
        {
            return false;
        }

        if (Camera.Main is not { } camera)
        {
            camera = _gameObjects.FirstOrDefault(g => !g.IsDisposed && g.ActiveInHierarchy && g.TryGetComponent(out Camera? _))
                ?.GetComponent<Camera>();
        }

        if (camera is null || camera.IsDisposed)
        {
            return false;
        }

        Matrix world = camera.GetWorldMatrix();
        Matrix view = camera.GetViewMatrix();
        Matrix projection = camera.GetProjectionMatrix(graphicsDevice);

        graphicsDevice.Clear(camera.ClearColor);
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.TryGetComponent(out Renderer? renderer))
            {
                renderer.Render(gameTime, world, view, projection);
            }
        }

        return true;
    }
}
