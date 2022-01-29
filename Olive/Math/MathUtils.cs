namespace Olive.Math;

/// <summary>
///     A collection of useful mathematical functions and constants.
/// </summary>
public static class MathUtils
{
    /// <summary>
    ///     ARM-supported epsilon.
    /// </summary>
    // static readonly instead of const to prevent compiler optimisation
    // ReSharper disable once HeuristicUnreachableCode
    private static readonly float Epsilon = float.Epsilon == 0 ? 1.175494351E-38f : float.Epsilon;

    /// <summary>
    ///     Determines if two single-precision floating-point values are considered approximately equal.
    /// </summary>
    /// <param name="left">The first value.</param>
    /// <param name="right">The second value.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="left" /> is considered approximately equal to <paramref name="right" />;
    ///     otherwise, <see langword="false" />.
    /// </returns>
    public static bool Approximately(float left, float right)
    {
        return MathF.Abs(left - right) < Epsilon;
    }
}
