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
    public IReadOnlyCollection<GameObject> GameObjects => _gameObjects.Where(g => !g.IsDisposed).ToArray();

    /// <summary>
    ///     Gets or sets the main camera of this scene.
    /// </summary>
    /// <value>The main camera.</value>
    public Camera MainCamera
    {
        get => _mainCamera;
        set => _mainCamera = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    ///     Gets the current scene manager.
    /// </summary>
    /// <value>The scene manager.</value>
    public SceneManager SceneManager { get; internal set; } = null!;

    /// <summary>
    ///     Gets the scene transform data.
    /// </summary>
    /// <value>The scene transform data.</value>
    public SceneTransform Transform { get; } = new();

    internal bool IsInitialized { get; private set; }

    /// <summary>
    ///     Called when the scene is being initialized.
    /// </summary>
    protected internal virtual void Initialize()
    {
        IsInitialized = true;
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
        Camera? FindMainCamera()
        {
            Camera? camera = Camera.Main;
            if (camera is not null && !camera.IsDisposed)
            {
                return camera;
            }

            for (var index = 0; index < _gameObjects.Count; index++)
            {
                GameObject gameObject = _gameObjects[index];
                if (gameObject.IsDisposed || !gameObject.ActiveInHierarchy)
                {
                    continue;
                }

                if (gameObject.TryGetComponent(out camera))
                {
                    return camera;
                }
            }

            return null;
        }

        if (OliveEngine.CurrentGame?.GraphicsDevice is not { } graphicsDevice)
        {
            return false;
        }

        Camera? camera = FindMainCamera();
        if (camera is null or {IsDisposed: true})
        {
            return false;
        }

        Matrix world = camera.GetWorldMatrix();
        Matrix view = camera.GetViewMatrix();
        Matrix projection = camera.GetProjectionMatrix(graphicsDevice);

        foreach (GameObject gameObject in _gameObjects)
        {
            if (!gameObject.IsDisposed && gameObject.TryGetComponent(out Renderer? renderer))
            {
                renderer.Render(gameTime, world, view, projection);
            }
        }

        return true;
    }
}
