using System.Globalization;
using System.Text;

namespace Olive.Math;

/// <summary>
///     Represents a vector with four single-precision floating-point values.
/// </summary>
/// <remarks>
///     <para>
///         This structure is designed to closely resemble the APIs of the built in .NET Vector4 structure, with inspiration
///         from MonoGame and Unity. 
///     </para>
/// </remarks>
public readonly struct Vector4 : IEquatable<Vector4>, IFormattable
{
    /// <summary>
    ///     A vector whose 4 elements are equal to one.
    /// </summary>
    /// <value>A vector whose four elements are equal to one (that is, the vector <c>(1, 1, 1, 1)</c>).</value>
    public static readonly Vector4 One = new(1);

    /// <summary>
    ///     The vector <c>(0, 0, 0, 1)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 0, 0, 1)</c>.</value>
    public static readonly Vector4 UnitW = new(0, 0, 0, 1);

    /// <summary>
    ///     The vector <c>(1, 0, 0, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(1, 0, 0, 0)</c>.</value>
    public static readonly Vector4 UnitX = new(1, 0, 0, 0);

    /// <summary>
    ///     The vector <c>(0, 1, 0, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 1, 0, 0)</c>.</value>
    public static readonly Vector4 UnitY = new(0, 1, 0, 0);

    /// <summary>
    ///     The vector <c>(0, 0, 1, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 0, 1, 0)</c>.</value>
    public static readonly Vector4 UnitZ = new(0, 0, 1, 0);

    /// <summary>
    ///     A vector whose 4 elements are equal to zero.
    /// </summary>
    /// <value>A vector whose four elements are equal to zero (that is, the vector <c>(0, 0, 0, 0)</c>).</value>
    public static readonly Vector4 Zero = new(0);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector4" /> structure whose four elements have the same value.
    /// </summary>
    /// <param name="value">The value to assign to all four elements.</param>
    public Vector4(float value) : this(value, value, value, value)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector4" /> structure whose elements have the specified values.
    /// </summary>
    /// <param name="x">The value to assign to <see cref="X" />.</param>
    /// <param name="y">The value to assign to <see cref="Y" />.</param>
    /// <param name="z">The value to assign to <see cref="Z" />.</param>
    /// <param name="w">The value to assign to <see cref="W" />.</param>
    public Vector4(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector4" /> structure by copying the <see cref="Vector3.X" />,
    ///     <see cref="Vector3.Y" /> and <see cref="Vector3.Z" /> of a <see cref="Vector3" /> to their respective component
    ///     values, and assigning <see cref="W" /> a specified value.
    /// </summary>
    /// <param name="vector">The three-dimensional vector to copy.</param>
    /// <param name="w">The value to assign to <see cref="W" />.</param>
    public Vector4(in Vector3 vector, float w) : this(vector.X, vector.Y, vector.Z, w)
    {
    }

    /// <summary>
    ///     Gets a value indicating whether this vector is normalized.
    /// </summary>
    /// <value><see langword="true" /> if this vector is normalized; otherwise, <see langword="false" />.</value>
    public bool IsNormalized => MathUtils.Approximately(LengthSquared, 1);

    /// <summary>
    ///     Gets the length of the vector.
    /// </summary>
    /// <value>The length of the vector.</value>
    /// <seealso cref="LengthSquared" />
    public float Length => Distance(this, Zero);

    /// <summary>
    ///     Gets the squared length of the vector.
    /// </summary>
    /// <value>The squared length of the vector.</value>
    /// <seealso cref="Length" />
    public float LengthSquared => DistanceSquared(this, Zero);

    /// <summary>
    ///     Gets or initializes the value of the W component.
    /// </summary>
    /// <value>The W component.</value>
    public float W { get; init; }

    /// <summary>
    ///     Gets or initializes the value of the X component.
    /// </summary>
    /// <value>The X component.</value>
    public float X { get; init; }

    /// <summary>
    ///     Gets or initializes the value of the Y component.
    /// </summary>
    /// <value>The Y component.</value>
    public float Y { get; init; }

    /// <summary>
    ///     Gets or initializes the value of the Z component.
    /// </summary>
    /// <value>The Z component.</value>
    public float Z { get; init; }

    /// <summary>
    ///     Gets the component value by a specified index.
    /// </summary>
    /// <param name="index">
    ///     <para><c>0</c> to retrieve the <see cref="X" /> component.</para>
    ///     -or-
    ///     <para><c>1</c> to retrieve the <see cref="Y" /> component.</para>
    ///     -or-
    ///     <para><c>2</c> to retrieve the <see cref="Z" /> component.</para>
    ///     -or-
    ///     <para><c>3</c> to retrieve the <see cref="W" /> component.</para>
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is less than 0 or greater than 3.
    /// </exception>
    public float this[int index]
    {
        get
        {
            return index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                3 => W,
                _ => throw new ArgumentOutOfRangeException(nameof(index))
            };
        }
        init
        {
            switch (index)
            {
                case 0:
                    X = value;
                    break;
                case 1:
                    Y = value;
                    break;
                case 2:
                    Z = value;
                    break;
                case 3:
                    W = value;
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }

    /// <summary>
    ///     Adds two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The summed vector.</returns>
    public static Vector4 operator +(in Vector4 left, in Vector4 right)
    {
        return new Vector4(
            left.X + right.X,
            left.Y + right.Y,
            left.Z + right.Z,
            left.W + right.W
        );
    }

    /// <summary>
    ///     Subtracts the second vector from the first.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The difference vector.</returns>
    public static Vector4 operator -(in Vector4 left, in Vector4 right)
    {
        return new Vector4(
            left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z,
            left.W - right.W
        );
    }

    /// <summary>
    ///     Multiples two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The product vector.</returns>
    public static Vector4 operator *(in Vector4 left, in Vector4 right)
    {
        return new Vector4(
            left.X * right.X,
            left.Y * right.Y,
            left.Z * right.Z,
            left.W * right.W
        );
    }

    /// <summary>
    ///     Multiples a vector by the given scalar.
    /// </summary>
    /// <param name="left">The vector value.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector4 operator *(in Vector4 left, float right)
    {
        return new Vector4(
            left.X * right,
            left.Y * right,
            left.Z * right,
            left.W * right
        );
    }

    /// <summary>
    ///     Multiples a vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The vector value.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector4 operator *(float left, in Vector4 right)
    {
        return new Vector4(
            left * right.X,
            left * right.Y,
            left * right.Z,
            left * right.W
        );
    }

    /// <summary>
    ///     Divides the first vector by the second.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector4 operator /(in Vector4 left, in Vector4 right)
    {
        return new Vector4(
            left.X / right.X,
            left.Y / right.Y,
            left.Z / right.Z,
            left.W / right.W
        );
    }

    /// <summary>
    ///     Divides the vector by the given scalar.
    /// </summary>
    /// <param name="left">The vector value.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector4 operator /(in Vector4 left, float right)
    {
        return new Vector4(
            left.X / right,
            left.Y / right,
            left.Z / right,
            left.W / right
        );
    }

    /// <summary>
    ///     Divides the vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The vector value.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector4 operator /(float left, in Vector4 right)
    {
        return new Vector4(
            left / right.X,
            left / right.Y,
            left / right.Z,
            left / right.W
        );
    }

    /// <summary>
    ///     Negates a given vector.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The negated vector.</returns>
    public static Vector4 operator -(in Vector4 value)
    {
        return Zero - value;
    }

    /// <summary>
    ///     Returns a value indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns><see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(in Vector4 left, in Vector4 right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Returns a value indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns><see langword="true" /> if the two vectors are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(in Vector4 left, in Vector4 right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Explicitly converts a <see cref="Vector3" /> to a <see cref="Vector4" />, by assigning <see cref="X" />,
    ///     <see cref="Y" /> and <see cref="Z"/> to their corresponding component values in <paramref name="value" /> and
    ///     assigning 0 to <see cref="W" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Vector4(in Vector3 value)
    {
        return new Vector4(value.X, value.Y, value.Z, 0);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Vector4" /> to <see cref="Microsoft.Xna.Framework.Vector4" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Microsoft.Xna.Framework.Vector4(in Vector4 value)
    {
        return new Microsoft.Xna.Framework.Vector4(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Microsoft.Xna.Framework.Vector4" /> to <see cref="Vector4" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Vector4(in Microsoft.Xna.Framework.Vector4 value)
    {
        return new Vector4(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Vector4" /> to <see cref="System.Numerics.Vector4" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator System.Numerics.Vector4(in Vector4 value)
    {
        return new System.Numerics.Vector4(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="System.Numerics.Vector4" /> to <see cref="Vector4" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Vector4(in System.Numerics.Vector4 value)
    {
        return new Vector4(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The absolute value vector.</returns>
    public static Vector4 Abs(in Vector4 value)
    {
        return new Vector4(
            MathF.Abs(value.X),
            MathF.Abs(value.Y),
            MathF.Abs(value.Z),
            MathF.Abs(value.W)
        );
    }

    /// <summary>
    ///     Restricts a vector between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <param name="min">The minimum vector</param>
    /// <param name="max">The maximum vector</param>
    /// <returns>The restricted vector.</returns>
    public static Vector4 Clamp(in Vector4 value, in Vector4 min, in Vector4 max)
    {
        return Min(Max(value, min), max);
    }

    /// <summary>
    ///     Returns the Euclidean distance between two points.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>The distance.</returns>
    public static float Distance(in Vector4 left, in Vector4 right)
    {
        Vector4 difference = left - right;
        float dot = Dot(difference, difference);
        return MathF.Sqrt(dot);
    }

    /// <summary>
    ///     Returns the squared Euclidean distance between two points.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static float DistanceSquared(in Vector4 left, in Vector4 right)
    {
        Vector4 difference = left - right;
        return Dot(difference, difference);
    }

    /// <summary>
    ///     Returns the dot product of two vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The dot product.</returns>
    public static float Dot(in Vector4 left, in Vector4 right)
    {
        return left.X * right.X +
               left.Y * right.Y +
               left.Z * right.Z +
               left.W * right.W;
    }

    /// <summary>
    ///     Performs a linear interpolation between two vectors based on a value that specifies the weighting of the second
    ///     vector.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <param name="alpha">The relative weight of <paramref name="right" /> in the interpolation.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector4 Lerp(in Vector4 left, in Vector4 right, float alpha)
    {
        Vector4 firstInfluence = left * (1.0f - alpha);
        Vector4 secondInfluence = right * alpha;
        return firstInfluence + secondInfluence;
    }

    /// <summary>
    ///     Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The maximized vector.</returns>
    public static Vector4 Max(in Vector4 left, in Vector4 right)
    {
        return new Vector4(
            MathF.Max(left.X, right.X),
            MathF.Max(left.Y, right.Y),
            MathF.Max(left.Z, right.Z),
            MathF.Max(left.W, right.W)
        );
    }

    /// <summary>
    ///     Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The minimized vector.</returns>
    public static Vector4 Min(in Vector4 left, in Vector4 right)
    {
        return new Vector4(
            MathF.Min(left.X, right.X),
            MathF.Min(left.Y, right.Y),
            MathF.Min(left.Z, right.Z),
            MathF.Min(left.W, right.W)
        );
    }

    /// <summary>
    ///     Returns a vector with the same direction as the given vector, but with a length of 1.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static Vector4 Normalize(in Vector4 value)
    {
        return value / value.Length;
    }

    /// <summary>
    ///     Returns a vector whose elements are the square root of each of the source vector's elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The square root vector.</returns>
    public static Vector4 SquareRoot(in Vector4 value)
    {
        return new(
            MathF.Sqrt(value.X),
            MathF.Sqrt(value.Y),
            MathF.Sqrt(value.Z),
            MathF.Sqrt(value.W)
        );
    }

    /// <summary>
    ///     Copies the contents of the vector into the given array.
    /// </summary>
    /// <param name="array">The destination array.</param>
    /// <param name="index">The starting index in the array to which the values should be written.</param>
    /// <exception cref="ArgumentNullException"><paramref name="array" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is outside of the bounds of the array.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The number of elements in the vector is greater than the size of <paramref name="array" />.
    /// </exception>
    public void CopyTo(float[] array, int index = 0)
    {
        if (array is null)
            throw new ArgumentNullException(nameof(array));

        if (index < 0 || index >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(index), "Specified index was out of the bounds of the array.");

        if (array.Length - index < 4)
            throw new ArgumentException("The number of elements in source vector is greater than the destination array.");

        array[index] = X;
        array[index + 1] = Y;
        array[index + 2] = Z;
        array[index + 3] = W;
    }

    /// <summary>
    ///     Deconstructs this vector.
    /// </summary>
    /// <param name="x">When this method returns, contains the value of the X component.</param>
    /// <param name="y">When this method returns, contains the value of the Y component.</param>
    /// <param name="z">When this method returns, contains the value of the Z component.</param>
    /// <param name="w">When this method returns, contains the value of the W component.</param>
    public void Deconstruct(out float x, out float y, out float z, out float w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }

    /// <summary>
    ///     Returns a value indicating whether this vector and another vector are equal.
    /// </summary>
    /// <param name="other">The vector to compare with this instance.</param>
    /// <returns><see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(Vector4 other)
    {
        return MathUtils.Approximately(X, other.X) &&
               MathUtils.Approximately(Y, other.Y) &&
               MathUtils.Approximately(Z, other.Z) &&
               MathUtils.Approximately(W, other.W);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Vector4 other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Vector4" /> instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Vector4" /> instance, using the specified format to
    ///     format individual elements.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format)
    {
        return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Vector4" /> instance, using the specified format to
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
        builder.Append(X.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(Y.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(Z.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(W.ToString(format, formatProvider));
        builder.Append('>');
        return builder.ToString();
    }
}
