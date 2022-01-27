using System.Drawing;
using Microsoft.Xna.Framework;

namespace Olive;

internal sealed class OliveGame : Game
{
    private readonly Size _resolution;
    private readonly string _title;
    private readonly DisplayMode _displayMode;
    private readonly GraphicsDeviceManager _graphics;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OliveGame" /> class.
    /// </summary>
    /// <param name="resolution">The game's screen resolution.</param>
    /// <param name="title">The window title.</param>
    /// <param name="displayMode">The game's display mode.</param>
    public OliveGame(Size resolution, string title, DisplayMode displayMode)
    {
        _resolution = resolution;
        _title = title;
        _displayMode = displayMode;
        _graphics = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _resolution.Width;
        _graphics.PreferredBackBufferHeight = _resolution.Height;
        _graphics.IsFullScreen = _displayMode is DisplayMode.Fullscreen or DisplayMode.FullscreenBorderless;
        _graphics.HardwareModeSwitch = _displayMode is not DisplayMode.FullscreenBorderless;

        _graphics.ApplyChanges();

        Window.Title = _title;
        base.Initialize();

        OliveEngine.SceneManager.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        OliveEngine.SceneManager.LoadContent();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        OliveEngine.SceneManager.Draw(gameTime);
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        OliveEngine.SceneManager.Update(gameTime);
    }
}
