using System.Globalization;
using System.Text;

namespace Olive;

/// <summary>
///     Represents a 32-bit color.
/// </summary>
/// <seealso cref="ColorF" />
public readonly struct Color : IEquatable<Color>, IFormattable
{
    public static readonly Color Transparent = new(0, ColorFormat.ABGR32);
    public static readonly Color AliceBlue = new(0xfffff8f0, ColorFormat.ABGR32);
    public static readonly Color AntiqueWhite = new(0xffd7ebfa, ColorFormat.ABGR32);
    public static readonly Color Aqua = new(0xffffff00, ColorFormat.ABGR32);
    public static readonly Color Aquamarine = new(0xffd4ff7f, ColorFormat.ABGR32);
    public static readonly Color Azure = new(0xfffffff0, ColorFormat.ABGR32);
    public static readonly Color Beige = new(0xffdcf5f5, ColorFormat.ABGR32);
    public static readonly Color Bisque = new(0xffc4e4ff, ColorFormat.ABGR32);
    public static readonly Color Black = new(0xff000000, ColorFormat.ABGR32);
    public static readonly Color BlanchedAlmond = new(0xffcdebff, ColorFormat.ABGR32);
    public static readonly Color Blue = new(0xffff0000, ColorFormat.ABGR32);
    public static readonly Color BlueViolet = new(0xffe22b8a, ColorFormat.ABGR32);
    public static readonly Color Brown = new(0xff2a2aa5, ColorFormat.ABGR32);
    public static readonly Color BurlyWood = new(0xff87b8de, ColorFormat.ABGR32);
    public static readonly Color CadetBlue = new(0xffa09e5f, ColorFormat.ABGR32);
    public static readonly Color Chartreuse = new(0xff00ff7f, ColorFormat.ABGR32);
    public static readonly Color Chocolate = new(0xff1e69d2, ColorFormat.ABGR32);
    public static readonly Color Coral = new(0xff507fff, ColorFormat.ABGR32);
    public static readonly Color CornflowerBlue = new(0xffed9564, ColorFormat.ABGR32);
    public static readonly Color Cornsilk = new(0xffdcf8ff, ColorFormat.ABGR32);
    public static readonly Color Crimson = new(0xff3c14dc, ColorFormat.ABGR32);
    public static readonly Color Cyan = new(0xffffff00, ColorFormat.ABGR32);
    public static readonly Color DarkBlue = new(0xff8b0000, ColorFormat.ABGR32);
    public static readonly Color DarkCyan = new(0xff8b8b00, ColorFormat.ABGR32);
    public static readonly Color DarkGoldenrod = new(0xff0b86b8, ColorFormat.ABGR32);
    public static readonly Color DarkGray = new(0xffa9a9a9, ColorFormat.ABGR32);
    public static readonly Color DarkGreen = new(0xff006400, ColorFormat.ABGR32);
    public static readonly Color DarkKhaki = new(0xff6bb7bd, ColorFormat.ABGR32);
    public static readonly Color DarkMagenta = new(0xff8b008b, ColorFormat.ABGR32);
    public static readonly Color DarkOliveGreen = new(0xff2f6b55, ColorFormat.ABGR32);
    public static readonly Color DarkOrange = new(0xff008cff, ColorFormat.ABGR32);
    public static readonly Color DarkOrchid = new(0xffcc3299, ColorFormat.ABGR32);
    public static readonly Color DarkRed = new(0xff00008b, ColorFormat.ABGR32);
    public static readonly Color DarkSalmon = new(0xff7a96e9, ColorFormat.ABGR32);
    public static readonly Color DarkSeaGreen = new(0xff8bbc8f, ColorFormat.ABGR32);
    public static readonly Color DarkSlateBlue = new(0xff8b3d48, ColorFormat.ABGR32);
    public static readonly Color DarkSlateGray = new(0xff4f4f2f, ColorFormat.ABGR32);
    public static readonly Color DarkTurquoise = new(0xffd1ce00, ColorFormat.ABGR32);
    public static readonly Color DarkViolet = new(0xffd30094, ColorFormat.ABGR32);
    public static readonly Color DeepPink = new(0xff9314ff, ColorFormat.ABGR32);
    public static readonly Color DeepSkyBlue = new(0xffffbf00, ColorFormat.ABGR32);
    public static readonly Color DimGray = new(0xff696969, ColorFormat.ABGR32);
    public static readonly Color DodgerBlue = new(0xffff901e, ColorFormat.ABGR32);
    public static readonly Color Firebrick = new(0xff2222b2, ColorFormat.ABGR32);
    public static readonly Color FloralWhite = new(0xfff0faff, ColorFormat.ABGR32);
    public static readonly Color ForestGreen = new(0xff228b22, ColorFormat.ABGR32);
    public static readonly Color Fuchsia = new(0xffff00ff, ColorFormat.ABGR32);
    public static readonly Color Gainsboro = new(0xffdcdcdc, ColorFormat.ABGR32);
    public static readonly Color GhostWhite = new(0xfffff8f8, ColorFormat.ABGR32);
    public static readonly Color Gold = new(0xff00d7ff, ColorFormat.ABGR32);
    public static readonly Color Goldenrod = new(0xff20a5da, ColorFormat.ABGR32);
    public static readonly Color Gray = new(0xff808080, ColorFormat.ABGR32);
    public static readonly Color Green = new(0xff008000, ColorFormat.ABGR32);
    public static readonly Color GreenYellow = new(0xff2fffad, ColorFormat.ABGR32);
    public static readonly Color Honeydew = new(0xfff0fff0, ColorFormat.ABGR32);
    public static readonly Color HotPink = new(0xffb469ff, ColorFormat.ABGR32);
    public static readonly Color IndianRed = new(0xff5c5ccd, ColorFormat.ABGR32);
    public static readonly Color Indigo = new(0xff82004b, ColorFormat.ABGR32);
    public static readonly Color Ivory = new(0xfff0ffff, ColorFormat.ABGR32);
    public static readonly Color Khaki = new(0xff8ce6f0, ColorFormat.ABGR32);
    public static readonly Color Lavender = new(0xfffae6e6, ColorFormat.ABGR32);
    public static readonly Color LavenderBlush = new(0xfff5f0ff, ColorFormat.ABGR32);
    public static readonly Color LawnGreen = new(0xff00fc7c, ColorFormat.ABGR32);
    public static readonly Color LemonChiffon = new(0xffcdfaff, ColorFormat.ABGR32);
    public static readonly Color LightBlue = new(0xffe6d8ad, ColorFormat.ABGR32);
    public static readonly Color LightCoral = new(0xff8080f0, ColorFormat.ABGR32);
    public static readonly Color LightCyan = new(0xffffffe0, ColorFormat.ABGR32);
    public static readonly Color LightGoldenrodYellow = new(0xffd2fafa, ColorFormat.ABGR32);
    public static readonly Color LightGray = new(0xffd3d3d3, ColorFormat.ABGR32);
    public static readonly Color LightGreen = new(0xff90ee90, ColorFormat.ABGR32);
    public static readonly Color LightPink = new(0xffc1b6ff, ColorFormat.ABGR32);
    public static readonly Color LightSalmon = new(0xff7aa0ff, ColorFormat.ABGR32);
    public static readonly Color LightSeaGreen = new(0xffaab220, ColorFormat.ABGR32);
    public static readonly Color LightSkyBlue = new(0xffface87, ColorFormat.ABGR32);
    public static readonly Color LightSlateGray = new(0xff998877, ColorFormat.ABGR32);
    public static readonly Color LightSteelBlue = new(0xffdec4b0, ColorFormat.ABGR32);
    public static readonly Color LightYellow = new(0xffe0ffff, ColorFormat.ABGR32);
    public static readonly Color Lime = new(0xff00ff00, ColorFormat.ABGR32);
    public static readonly Color LimeGreen = new(0xff32cd32, ColorFormat.ABGR32);
    public static readonly Color Linen = new(0xffe6f0fa, ColorFormat.ABGR32);
    public static readonly Color Magenta = new(0xffff00ff, ColorFormat.ABGR32);
    public static readonly Color Maroon = new(0xff000080, ColorFormat.ABGR32);
    public static readonly Color MediumAquamarine = new(0xffaacd66, ColorFormat.ABGR32);
    public static readonly Color MediumBlue = new(0xffcd0000, ColorFormat.ABGR32);
    public static readonly Color MediumOrchid = new(0xffd355ba, ColorFormat.ABGR32);
    public static readonly Color MediumPurple = new(0xffdb7093, ColorFormat.ABGR32);
    public static readonly Color MediumSeaGreen = new(0xff71b33c, ColorFormat.ABGR32);
    public static readonly Color MediumSlateBlue = new(0xffee687b, ColorFormat.ABGR32);
    public static readonly Color MediumSpringGreen = new(0xff9afa00, ColorFormat.ABGR32);
    public static readonly Color MediumTurquoise = new(0xffccd148, ColorFormat.ABGR32);
    public static readonly Color MediumVioletRed = new(0xff8515c7, ColorFormat.ABGR32);
    public static readonly Color MidnightBlue = new(0xff701919, ColorFormat.ABGR32);
    public static readonly Color MintCream = new(0xfffafff5, ColorFormat.ABGR32);
    public static readonly Color MistyRose = new(0xffe1e4ff, ColorFormat.ABGR32);
    public static readonly Color Moccasin = new(0xffb5e4ff, ColorFormat.ABGR32);
    public static readonly Color MonoGameOrange = new(0xff003ce7, ColorFormat.ABGR32);
    public static readonly Color NavajoWhite = new(0xffaddeff, ColorFormat.ABGR32);
    public static readonly Color Navy = new(0xff800000, ColorFormat.ABGR32);
    public static readonly Color OldLace = new(0xffe6f5fd, ColorFormat.ABGR32);
    public static readonly Color Olive = new(0xff008080, ColorFormat.ABGR32);
    public static readonly Color OliveDrab = new(0xff238e6b, ColorFormat.ABGR32);
    public static readonly Color Orange = new(0xff00a5ff, ColorFormat.ABGR32);
    public static readonly Color OrangeRed = new(0xff0045ff, ColorFormat.ABGR32);
    public static readonly Color Orchid = new(0xffd670da, ColorFormat.ABGR32);
    public static readonly Color PaleGoldenrod = new(0xffaae8ee, ColorFormat.ABGR32);
    public static readonly Color PaleGreen = new(0xff98fb98, ColorFormat.ABGR32);
    public static readonly Color PaleTurquoise = new(0xffeeeeaf, ColorFormat.ABGR32);
    public static readonly Color PaleVioletRed = new(0xff9370db, ColorFormat.ABGR32);
    public static readonly Color PapayaWhip = new(0xffd5efff, ColorFormat.ABGR32);
    public static readonly Color PeachPuff = new(0xffb9daff, ColorFormat.ABGR32);
    public static readonly Color Peru = new(0xff3f85cd, ColorFormat.ABGR32);
    public static readonly Color Pink = new(0xffcbc0ff, ColorFormat.ABGR32);
    public static readonly Color Plum = new(0xffdda0dd, ColorFormat.ABGR32);
    public static readonly Color PowderBlue = new(0xffe6e0b0, ColorFormat.ABGR32);
    public static readonly Color Purple = new(0xff800080, ColorFormat.ABGR32);
    public static readonly Color Red = new(0xff0000ff, ColorFormat.ABGR32);
    public static readonly Color RosyBrown = new(0xff8f8fbc, ColorFormat.ABGR32);
    public static readonly Color RoyalBlue = new(0xffe16941, ColorFormat.ABGR32);
    public static readonly Color SaddleBrown = new(0xff13458b, ColorFormat.ABGR32);
    public static readonly Color Salmon = new(0xff7280fa, ColorFormat.ABGR32);
    public static readonly Color SandyBrown = new(0xff60a4f4, ColorFormat.ABGR32);
    public static readonly Color SeaGreen = new(0xff578b2e, ColorFormat.ABGR32);
    public static readonly Color SeaShell = new(0xffeef5ff, ColorFormat.ABGR32);
    public static readonly Color Sienna = new(0xff2d52a0, ColorFormat.ABGR32);
    public static readonly Color Silver = new(0xffc0c0c0, ColorFormat.ABGR32);
    public static readonly Color SkyBlue = new(0xffebce87, ColorFormat.ABGR32);
    public static readonly Color SlateBlue = new(0xffcd5a6a, ColorFormat.ABGR32);
    public static readonly Color SlateGray = new(0xff908070, ColorFormat.ABGR32);
    public static readonly Color Snow = new(0xfffafaff, ColorFormat.ABGR32);
    public static readonly Color SpringGreen = new(0xff7fff00, ColorFormat.ABGR32);
    public static readonly Color SteelBlue = new(0xffb48246, ColorFormat.ABGR32);
    public static readonly Color Tan = new(0xff8cb4d2, ColorFormat.ABGR32);
    public static readonly Color Teal = new(0xff808000, ColorFormat.ABGR32);
    public static readonly Color Thistle = new(0xffd8bfd8, ColorFormat.ABGR32);
    public static readonly Color Tomato = new(0xff4763ff, ColorFormat.ABGR32);
    public static readonly Color Turquoise = new(0xffd0e040, ColorFormat.ABGR32);
    public static readonly Color Violet = new(0xffee82ee, ColorFormat.ABGR32);
    public static readonly Color Wheat = new(0xffb3def5, ColorFormat.ABGR32);
    public static readonly Color White = new(uint.MaxValue, ColorFormat.ABGR32);
    public static readonly Color WhiteSmoke = new(0xfff5f5f5, ColorFormat.ABGR32);
    public static readonly Color Yellow = new(0xff00ffff, ColorFormat.ABGR32);
    public static readonly Color YellowGreen = new(0xff32cd9a, ColorFormat.ABGR32);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Color" /> structure by unpacking a 32-bit color value in the specified
    ///     format.
    /// </summary>
    /// <param name="packedValue">The 32-bit packed color value.</param>
    /// <param name="colorFormat">The color format.</param>
    public Color(uint packedValue, ColorFormat colorFormat)
    {
        switch (colorFormat)
        {
            case ColorFormat.ARGB32:
                A = (byte) ((packedValue & 0xFF000000) >> 24);
                R = (byte) ((packedValue & 0xFF0000) >> 16);
                G = (byte) ((packedValue & 0xFF00) >> 8);
                B = (byte) (packedValue & 0xFF);
                break;
            case ColorFormat.RGBA32:
                R = (byte) ((packedValue & 0xFF000000) >> 24);
                G = (byte) ((packedValue & 0xFF0000) >> 16);
                B = (byte) ((packedValue & 0xFF00) >> 8);
                A = (byte) (packedValue & 0xFF);
                break;
            case ColorFormat.ABGR32:
                A = (byte) ((packedValue & 0xFF000000) >> 24);
                B = (byte) ((packedValue & 0xFF0000) >> 16);
                G = (byte) ((packedValue & 0xFF00) >> 8);
                R = (byte) (packedValue & 0xFF);
                break;
            case ColorFormat.RGB24:
                R = (byte) ((packedValue & 0xFF0000) >> 16);
                G = (byte) ((packedValue & 0xFF00) >> 8);
                B = (byte) (packedValue & 0xFF);
                A = 255;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(colorFormat), colorFormat, "Invalid color format.");
        }
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Color" /> structure by assigning the R, G, B, and A components to
    ///     specified values.
    /// </summary>
    /// <param name="r">The value to assign to the <see cref="R" /> component.</param>
    /// <param name="g">The value to assign to the <see cref="G" /> component.</param>
    /// <param name="b">The value to assign to the <see cref="B" /> component.</param>
    /// <param name="a">The value to assign to the <see cref="A" /> component.</param>
    public Color(byte r, byte g, byte b, byte a = 255) : this()
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
    public byte A { get; init; }

    /// <summary>
    ///     Gets the blue component of this color.
    /// </summary>
    /// <value>The blue component.</value>
    public byte B { get; init; }

    /// <summary>
    ///     Gets the green component of this color.
    /// </summary>
    /// <value>The green component.</value>
    public byte G { get; init; }

    /// <summary>
    ///     Gets the red component of this color.
    /// </summary>
    /// <value>The red component.</value>
    public byte R { get; init; }

    /// <summary>
    ///     Returns a value indicating whether the two given colors are equal.
    /// </summary>
    /// <param name="left">The first color to compare.</param>
    /// <param name="right">The second color to compare.</param>
    /// <returns><see langword="true" /> if the two colors are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(in Color left, in Color right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Returns a value indicating whether the two given colors are not equal.
    /// </summary>
    /// <param name="left">The first color to compare.</param>
    /// <param name="right">The second color to compare.</param>
    /// <returns><see langword="true" /> if the two colors are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(in Color left, in Color right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Explicitly converts a <see cref="ColorF" /> to a <see cref="Color" />.
    /// </summary>
    /// <param name="value">The color to convert.</param>
    /// <returns>The converted color.</returns>
    public static explicit operator Color(in ColorF value)
    {
        return new Color((byte) (value.R * 255f), (byte) (value.G * 255f), (byte) (value.B * 255f), (byte) (value.A * 255f));
    }

    /// <summary>
    ///     Inverts a given color.
    /// </summary>
    /// <param name="value">The color to invert.</param>
    /// <returns>The inverted vector.</returns>
    public static Color Invert(in Color value)
    {
        return new Color((byte) (255 - value.R), (byte) (255 - value.G), (byte) (255 - value.B), (byte) (255 - value.A));
    }

    /// <summary>
    ///     Performs a linear interpolation between two colors based on a value that specifies the weighting of the second
    ///     color.
    /// </summary>
    /// <param name="left">The first color.</param>
    /// <param name="right">The second color.</param>
    /// <param name="alpha">The relative weight of <paramref name="right" /> in the interpolation.</param>
    /// <returns>The interpolated color.</returns>
    public static Color Lerp(in Color left, in Color right, float alpha)
    {
        return (Color) ColorF.Lerp(left, right, alpha); // leave me alone, it's been a long day.
    }

    /// <summary>
    ///     Returns a color whose elements are the maximum of each of the pairs of elements in the two source colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>The maximized color.</returns>
    public static Color Max(in Color left, in Color right)
    {
        return new Color(
            System.Math.Max(left.R, right.R),
            System.Math.Max(left.G, right.G),
            System.Math.Max(left.B, right.B),
            System.Math.Max(left.A, right.A)
        );
    }

    /// <summary>
    ///     Returns a color whose elements are the minimum of each of the pairs of elements in the two source colors.
    /// </summary>
    /// <param name="left">The first source color.</param>
    /// <param name="right">The second source color.</param>
    /// <returns>The minimized color.</returns>
    public static Color Min(in Color left, in Color right)
    {
        return new Color(
            System.Math.Min(left.R, right.R),
            System.Math.Min(left.G, right.G),
            System.Math.Min(left.B, right.B),
            System.Math.Min(left.A, right.A)
        );
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Color" /> to <see cref="Microsoft.Xna.Framework.Color" />.
    /// </summary>
    /// <param name="value">The color to convert.</param>
    /// <returns>The converted color.</returns>
    public static implicit operator Microsoft.Xna.Framework.Color(in Color value)
    {
        return new Microsoft.Xna.Framework.Color(value.ToPackedValue(ColorFormat.ABGR32));
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Microsoft.Xna.Framework.Color" /> to <see cref="Color" />.
    /// </summary>
    /// <param name="value">The color to convert.</param>
    /// <returns>The converted color.</returns>
    public static implicit operator Color(in Microsoft.Xna.Framework.Color value)
    {
        return new Color(value.PackedValue, ColorFormat.ABGR32);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Color" /> to <see cref="System.Drawing.Color" />.
    /// </summary>
    /// <param name="value">The color to convert.</param>
    /// <returns>The converted color.</returns>
    public static implicit operator System.Drawing.Color(in Color value)
    {
        return System.Drawing.Color.FromArgb((int) value.ToPackedValue(ColorFormat.ARGB32));
    }

    /// <summary>
    ///     Implicitly converts from <see cref="System.Drawing.Color" /> to <see cref="Color" />.
    /// </summary>
    /// <param name="value">The color to convert.</param>
    /// <returns>The converted color.</returns>
    public static implicit operator Color(in System.Drawing.Color value)
    {
        return new Color((uint) value.ToArgb(), ColorFormat.ARGB32);
    }

    /// <summary>
    ///     Returns a value indicating whether this color and another color are equal.
    /// </summary>
    /// <param name="other">The color to compare with this instance.</param>
    /// <returns><see langword="true" /> if the two colors are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(Color other)
    {
        return R == other.R &&
               G == other.G &&
               B == other.B &&
               A == other.A;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Color other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(R, G, B, A);
    }

    /// <summary>
    ///     Packs this color to a 32-bit integer using the specified color format.
    /// </summary>
    /// <param name="colorFormat">The color format.</param>
    /// <returns>The packed color value.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="colorFormat" /> is not a valid format.</exception>
    public uint ToPackedValue(ColorFormat colorFormat)
    {
        return colorFormat switch
        {
            ColorFormat.RGBA32 => (uint) ((R << 24) | (G << 16) | (B << 8) | A),
            ColorFormat.ARGB32 => (uint) ((A << 24) | (R << 16) | (G << 8) | B),
            ColorFormat.ABGR32 => (uint) ((A << 24) | (G << 16) | (B << 8) | R),
            ColorFormat.RGB24 => (uint) ((R << 16) | (G << 8) | B),
            _ => throw new ArgumentOutOfRangeException(nameof(colorFormat), colorFormat, "Invalid color format.")
        };
    }

    /// <summary>
    ///     Converts this color to a hexadecimal color string.
    /// </summary>
    /// <param name="colorFormat">The color format.</param>
    /// <param name="includeHash">Whether or not to prepend the '#' character to the start of the result.</param>
    /// <returns>The hexadecimal representation of this color.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="colorFormat" /> is not a valid format.</exception>
    public string ToHexadecimalString(ColorFormat colorFormat = ColorFormat.ARGB32, bool includeHash = true)
    {
        var builder = new StringBuilder();
        if (includeHash)
        {
            builder.Append('#');
        }

        builder.Append(ToPackedValue(colorFormat).ToString("X2"));
        return builder.ToString();
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
