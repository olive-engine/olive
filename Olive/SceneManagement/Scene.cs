using Microsoft.Xna.Framework;
using Olive.Components;

namespace Olive.SceneManagement;

/// <summary>
///     Represents a scene.
/// </summary>
public abstract class Scene
{
    private readonly List<GameObject> _gameObjects = new();

    /// <summary>
    ///     Gets a read-only view of the game objects currently in the scene.
    /// </summary>
    /// <value>A read-only view of the game objects currently in the scene.</value>
    public IReadOnlyCollection<GameObject> GameObjects => _gameObjects.AsReadOnly();

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
            if (!gameObject.ActiveInHierarchy) continue;
            gameObject.Update(gameTime);
        }
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

    internal void Draw(GameTime gameTime)
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.ActiveInHierarchy && gameObject.TryGetComponent(out Camera? camera))
            {
                OliveEngine.CurrentGame?.GraphicsDevice.Clear(camera.ClearColor);
                break;
            }
        }
    }
}
