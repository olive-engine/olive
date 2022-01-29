namespace Olive;

/// <summary>
///     An enumeration of color foramts.
/// </summary>
public enum ColorFormat
{
    /// <summary>
    ///     Denotes the format RRGGBBAA.
    /// </summary>
    RGBA32,

    /// <summary>
    ///     Denotes the format AARRGGBB.
    /// </summary>
    ARGB32,

    /// <summary>
    ///     Denotes the format AABBGGRR.
    /// </summary>
    ABGR32, // primarily for MonoGame interop

    /// <summary>
    ///     Denotes the format RRGGBB.
    /// </summary>
    RGB24
}
