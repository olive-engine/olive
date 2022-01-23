using System.Drawing;
using Microsoft.Xna.Framework;

namespace Olive;

internal sealed class OliveGame : Game
{
    private readonly Size _resolution;
    private readonly string _title;
    private readonly GraphicsDeviceManager _graphics;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OliveGame" /> class.
    /// </summary>
    /// <param name="resolution">The game's screen resolution.</param>
    /// <param name="title">The window title.</param>
    public OliveGame(Size resolution, string title)
    {
        _resolution = resolution;
        _title = title;
        _graphics = new GraphicsDeviceManager(this);
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _resolution.Width;
        _graphics.PreferredBackBufferHeight = _resolution.Height;
        _graphics.ApplyChanges();

        Window.Title = _title;
        base.Initialize();
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
