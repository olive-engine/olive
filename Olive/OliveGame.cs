using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Olive.Rendering;
using Olive.SceneManagement;

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
        Screen.SetResolution(_resolution, _displayMode);

        var state = new DepthStencilState();
        state.DepthBufferEnable = true;
        GraphicsDevice.DepthStencilState = state;

        Window.Title = _title;
        base.Initialize();

        for (var index = 0; index < SceneManager.LoadedScenes.Count; index++)
        {
            Scene scene = SceneManager.LoadedScenes[index];
            scene.Initialize();
        }
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        SpriteBatch = new SpriteBatch(GraphicsDevice);
        // OliveEngine.SceneManager.LoadContent();
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

        for (var index = 0; index < SceneManager.LoadedScenes.Count; index++)
        {
            Scene scene = SceneManager.LoadedScenes[index];
            scene.Update(frameContext);
        }
    }
}
