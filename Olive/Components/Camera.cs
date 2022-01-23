using Microsoft.Xna.Framework;

namespace Olive.Components;

/// <summary>
///     Represents a camera.
/// </summary>
public sealed class Camera : Component
{
    /// <summary>
    ///     Gets or sets the camera's clear color.
    /// </summary>
    public Color ClearColor { get; set; } = Color.Black;
}
