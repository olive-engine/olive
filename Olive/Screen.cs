using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaDisplayMode = Microsoft.Xna.Framework.Graphics.DisplayMode;

namespace Olive;

/// <summary>
///     Provides methods and properties related to display management.
/// </summary>
public static class Screen
{
    private static GraphicsDeviceManager? s_graphicsDeviceManager;

    /// <summary>
    ///     Lazy-loads the graphics device.
    /// </summary>
    private static GraphicsDeviceManager GraphicsDeviceManager =>
        s_graphicsDeviceManager ??= OliveEngine.CurrentGame.GraphicsDeviceManager;

    /// <summary>
    ///     Gets the current screen resolution height.
    /// </summary>
    /// <value>The screen resolution height.</value>
    public static int Height => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

    /// <summary>
    ///     Gets the current screen resolution.
    /// </summary>
    /// <value>The screen resolution.</value>
    public static Size Resolution => new(Width, Height);

    /// <summary>
    ///     Gets the current screen resolution width.
    /// </summary>
    /// <value>The screen resolution width.</value>
    public static int Width => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

    /// <summary>
    ///     Gets the supported screen resolutions.
    /// </summary>
    /// <returns>An array of <see cref="Size" /> values representing the supported resolutions.</returns>
    public static Size[] GetSupportedResolutions()
    {
        var resolutions = new List<Size>();

        DisplayModeCollection displayModes = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;

        // IDE wants to optimize this and use Enumerable.Select, but to that I say: fuck you.
        // I refuse to allocate a predicate for something so mundanely trivial.
        foreach (XnaDisplayMode displayMode in displayModes)
        {
            resolutions.Add(new Size(displayMode.Width, displayMode.Height));
        }

        return resolutions.ToArray();
    }

    /// <summary>
    ///     Switches the screen resolution.
    /// </summary>
    /// <param name="resolution">The resolution.</param>
    /// <param name="displayMode">The display mode.</param>
    public static void SetResolution(Size resolution, DisplayMode displayMode)
    {
        SetResolution(resolution.Width, resolution.Height, displayMode);
    }

    /// <summary>
    ///     Switches the screen resolution.
    /// </summary>
    /// <param name="width">The resolution width.</param>
    /// <param name="height">The resolution height.</param>
    /// <param name="displayMode">The display mode.</param>
    public static void SetResolution(int width, int height, DisplayMode displayMode)
    {
        GraphicsDeviceManager graphicsDeviceManager = GraphicsDeviceManager;
        graphicsDeviceManager.PreferredBackBufferWidth = width;
        graphicsDeviceManager.PreferredBackBufferHeight = height;
        graphicsDeviceManager.IsFullScreen = displayMode is not DisplayMode.Windowed;
        graphicsDeviceManager.HardwareModeSwitch = displayMode is not DisplayMode.FullscreenBorderless;
        graphicsDeviceManager.ApplyChanges();
    }
}
