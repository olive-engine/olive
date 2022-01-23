using System.Drawing;
using Olive.SceneManagement;

namespace Olive;

/// <summary>
///     Represents a class which provides global engine data.
/// </summary>
public static class OliveEngine
{
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
    /// <param name="isFullscreen"><see langword="true" /> to enable full screen rendering, otherwise <see langword="false" />.</param>
    public static void Initialize(string title, int width, int height, bool isFullscreen)
    {
        Initialize(title, new Size(width, height), isFullscreen);
    }

    /// <summary>
    ///     Initializes the game engine.
    /// </summary>
    /// <param name="title">The window title.</param>
    /// <param name="resolution">The screen resolution.</param>
    /// <param name="isFullscreen"><see langword="true" /> to enable full screen rendering, otherwise <see langword="false" />.</param>
    public static void Initialize(string title, Size resolution, bool isFullscreen)
    {
        CurrentGame = new OliveGame(resolution, title);

        _gameThread = new Thread(() => CurrentGame.Run());
        _gameThread.Start();

        s_sceneManager.PrimaryScene?.Initialize();
    }
}
