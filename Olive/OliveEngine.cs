using System.Drawing;
using Olive.Components;
using Olive.SceneManagement;

namespace Olive;

/// <summary>
///     Represents a class which provides global engine data.
/// </summary>
public static class OliveEngine
{
    private static readonly List<Component> _liveComponents = new();
    private static bool s_isInitialized;
    private static Thread? _gameThread;
    private static SceneManager s_sceneManager = new SimpleSceneManager();

    /// <summary>
    ///     Gets or sets the current scene manager.
    /// </summary>
    /// <value>The current scene manager.</value>
    /// <exception cref="ArgumentNullException"><paramref name="value" /> is <see langword="null" />.</exception>
    public static SceneManager SceneManager
    {
        get => s_sceneManager;
        set => s_sceneManager = value ?? throw new ArgumentNullException(nameof(value));
    }

    internal static OliveGame? CurrentGame { get; private set; }

    /// <summary>
    ///     Initializes the game engine.
    /// </summary>
    /// <param name="title">The window title.</param>
    /// <param name="width">The resolution width.</param>
    /// <param name="height">The resolution height.</param>
    /// <param name="displayMode">The game's display mode.</param>
    public static void Initialize(string title, int width, int height, DisplayMode displayMode)
    {
        Initialize(title, new Size(width, height), displayMode);
    }

    /// <summary>
    ///     Initializes the game engine.
    /// </summary>
    /// <param name="title">The window title.</param>
    /// <param name="resolution">The screen resolution.</param>
    /// <param name="displayMode">The game's display mode.</param>
    public static void Initialize(string title, Size resolution, DisplayMode displayMode)
    {
        CurrentGame = new OliveGame(resolution, title, displayMode);
        s_sceneManager.Initialize();
        s_isInitialized = true;
    }

    /// <summary>
    ///     Runs the game.
    /// </summary>
    public static void Run()
    {
        if (!s_isInitialized)
            throw new InvalidOperationException("Cannot run game before engine is initialized.");

        _gameThread = new Thread(() => CurrentGame.Run());
        _gameThread.Start();
    }

    internal static void AssertNonDisposed<T>(T instance) where T : Component
    {
        if (!_liveComponents.Contains(instance))
            throw new ObjectDisposedException(instance.GetType().Name);
    }

    internal static void CreateComponent<T>(T instance) where T : Component
    {
        if (!_liveComponents.Contains(instance))
            _liveComponents.Add(instance);
    }

    internal static void DisposeComponent<T>(T instance) where T : Component
    {
        if (!_liveComponents.Contains(instance)) return;
        _liveComponents.Remove(instance);
    }
}
