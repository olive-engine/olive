using Microsoft.Xna.Framework;

namespace Olive.Extensions;

internal static class Vector3Extensions
{
    /// <summary>
    ///     Returns a normalized vector.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    /// <remarks>
    ///     This method exists because <see cref="Vector3.Normalize()" /> modifies the instance, rather than returning a new
    ///     instance - which can be useful when inlining equations.
    /// </remarks>
    public static Vector3 Normalized(this Vector3 value)
    {
        value.Normalize();
        return value;
    }
}
