using System.Globalization;
using System.Text;
using Olive.Math;

namespace Olive;

/// <summary>
///     Represents a color whose values are stored as single-precision floating-point values.
/// </summary>
/// <seealso cref="Color" />
public readonly struct ColorF
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Color" /> structure by assigning the R, G, B, and A components to
    ///     specified values.
    /// </summary>
    /// <param name="r">The value to assign to the <see cref="R" /> component.</param>
    /// <param name="g">The value to assign to the <see cref="G" /> component.</param>
    /// <param name="b">The value to assign to the <see cref="B" /> component.</param>
    /// <param name="a">The value to assign to the <see cref="A" /> component.</param>
    public ColorF(float r, float g, float b, float a = 1) : this()
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>
    ///     Gets the alpha component of this color.
    /// </summary>
    /// <value>The alpha component.</value>
    public float A { get; init; }

    /// <summary>
    ///     Gets the blue component of this color.
    /// </summary>
    /// <value>The blue component.</value>
    public float B { get; init; }

    /// <summary>
    ///     Gets the green component of this color.
    /// </summary>
    /// <value>The green component.</value>
    public float G { get; init; }

    /// <summary>
    ///     Gets the red component of this color.
    /// </summary>
    /// <value>The red component.</value>
    public float R { get; init; }

    /// <summary>
    ///     Adds two colors together.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>The summed color.</returns>
    public static ColorF operator +(in ColorF left, in ColorF right)
    {
        return new ColorF(
            left.R + right.R,
            left.G + right.G,
            left.B + right.B,
            left.A + right.A
        );
    }

    /// <summary>
    ///     Subtracts the second color from the first.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>The difference color.</returns>
    public static ColorF operator -(in ColorF left, in ColorF right)
    {
        return new ColorF(
            left.R - right.R,
            left.G - right.G,
            left.B - right.B,
            left.A - right.A
        );
    }

    /// <summary>
    ///     Multiples two colors together.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>The product color.</returns>
    public static ColorF operator *(in ColorF left, in ColorF right)
    {
        return new ColorF(
            left.R * right.R,
            left.G * right.G,
            left.B * right.B,
            left.A * right.A
        );
    }

    /// <summary>
    ///     Multiples a color by the given scalar.
    /// </summary>
    /// <param name="left">The color value.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled color.</returns>
    public static ColorF operator *(in ColorF left, float right)
    {
        return new ColorF(
            left.R * right,
            left.G * right,
            left.B * right,
            left.A * right
        );
    }

    /// <summary>
    ///     Multiples a color by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The color value.</param>
    /// <returns>The scaled color.</returns>
    public static ColorF operator *(float left, in ColorF right)
    {
        return new ColorF(
            left * right.R,
            left * right.G,
            left * right.B,
            left * right.A
        );
    }

    /// <summary>
    ///     Returns a value indicating whether the two given colors are equal.
    /// </summary>
    /// <param name="left">The first color to compare.</param>
    /// <param name="right">The second color to compare.</param>
    /// <returns><see langword="true" /> if the two colors are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(in ColorF left, in ColorF right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Returns a value indicating whether the two given colors are not equal.
    /// </summary>
    /// <param name="left">The first color to compare.</param>
    /// <param name="right">The second color to compare.</param>
    /// <returns><see langword="true" /> if the two colors are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(in ColorF left, in ColorF right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Implicitly converts a <see cref="Color" /> to a <see cref="ColorF" />.
    /// </summary>
    /// <param name="value">The color to convert.</param>
    /// <returns>The converted color.</returns>
    public static implicit operator ColorF(in Color value)
    {
        return new ColorF(value.R / 255f, value.G / 255f, value.B / 255f, value.A / 255f);
    }

    /// <summary>
    ///     Inverts a given color.
    /// </summary>
    /// <param name="value">The color to invert.</param>
    /// <returns>The inverted vector.</returns>
    public static ColorF Invert(in ColorF value)
    {
        return new ColorF(1 - value.R, 1 - value.G, 1 - value.B, 1 - value.A);
    }

    /// <summary>
    ///     Performs a linear interpolation between two colors based on a value that specifies the weighting of the second
    ///     color.
    /// </summary>
    /// <param name="left">The first color.</param>
    /// <param name="right">The second color.</param>
    /// <param name="alpha">The relative weight of <paramref name="right" /> in the interpolation.</param>
    /// <returns>The interpolated color.</returns>
    public static ColorF Lerp(in ColorF left, in ColorF right, float alpha)
    {
        ColorF firstInfluence = left * (1.0f - alpha);
        ColorF secondInfluence = right * alpha;
        return firstInfluence + secondInfluence;
    }

    /// <summary>
    ///     Returns a color whose elements are the maximum of each of the pairs of elements in the two source colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>The maximized color.</returns>
    public static ColorF Max(in ColorF left, in ColorF right)
    {
        return new ColorF(
            MathF.Max(left.R, right.R),
            MathF.Max(left.G, right.G),
            MathF.Max(left.B, right.B),
            MathF.Max(left.A, right.A)
        );
    }

    /// <summary>
    ///     Returns a color whose elements are the minimum of each of the pairs of elements in the two source colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>The minimized color.</returns>
    public static ColorF Min(in ColorF left, in ColorF right)
    {
        return new ColorF(
            MathF.Min(left.R, right.R),
            MathF.Min(left.G, right.G),
            MathF.Min(left.B, right.B),
            MathF.Min(left.A, right.A)
        );
    }

    /// <summary>
    ///     Returns a value indicating whether this color and another color are equal.
    /// </summary>
    /// <param name="other">The color to compare with this instance.</param>
    /// <returns><see langword="true" /> if the two colors are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(ColorF other)
    {
        return MathUtils.Approximately(R, other.R) &&
               MathUtils.Approximately(G, other.G) &&
               MathUtils.Approximately(B, other.B) &&
               MathUtils.Approximately(A, other.A);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is ColorF other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(R, G, B, A);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Vector3" /> instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Vector3" /> instance, using the specified format to
    ///     format individual elements.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format)
    {
        return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Vector3" /> instance, using the specified format to
    ///     format individual elements and the given <see cref="IFormatProvider" />.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <param name="formatProvider">The format provider to use when formatting elements.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var builder = new StringBuilder();
        string separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
        builder.Append('<');
        builder.Append(R.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(G.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(B.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(A.ToString(format, formatProvider));
        builder.Append('>');
        return builder.ToString();
    }
}
