using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Olive;

internal sealed class OliveGame : Game
{
    private readonly Size _resolution;
    private readonly string _title;
    private readonly DisplayMode _displayMode;

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
        GraphicsDeviceManager = new GraphicsDeviceManager(this);
    }

    public SpriteBatch SpriteBatch { get; private set; }

    internal GraphicsDeviceManager GraphicsDeviceManager { get; }

    protected override void Initialize()
    {
        GraphicsDeviceManager.PreferredBackBufferWidth = _resolution.Width;
        GraphicsDeviceManager.PreferredBackBufferHeight = _resolution.Height;
        GraphicsDeviceManager.IsFullScreen = _displayMode is DisplayMode.Fullscreen or DisplayMode.FullscreenBorderless;
        GraphicsDeviceManager.HardwareModeSwitch = _displayMode is not DisplayMode.FullscreenBorderless;

        GraphicsDeviceManager.ApplyChanges();

        var state = new DepthStencilState();
        state.DepthBufferEnable = true;
        GraphicsDevice.DepthStencilState = state;

        Window.Title = _title;
        base.Initialize();

        OliveEngine.SceneManager.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        SpriteBatch = new SpriteBatch(GraphicsDevice);
        OliveEngine.SceneManager.LoadContent();
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        OliveEngine.SceneManager.Draw(gameTime);

        SpriteBatch.Begin(SpriteSortMode.Immediate, depthStencilState: GraphicsDevice.DepthStencilState);
        OliveEngine.SceneManager.OnPostRender();
        SpriteBatch.End();
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var frameContext = new FrameContext
        {
            DeltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds,
            TotalTime = (float) gameTime.TotalGameTime.TotalSeconds
        };
        OliveEngine.SceneManager.Update(frameContext);
    }
}
