using System.Globalization;
using System.Text;

namespace Olive.Math;

/// <summary>
///     Represents a vector with three single-precision floating-point values.
/// </summary>
/// <remarks>
///     <para>
///         This structure is designed to closely resemble the APIs of the built in .NET Vector3 structure, with inspiration
///         from MonoGame and Unity. 
///     </para>
/// </remarks>
public readonly struct Vector3 : IEquatable<Vector3>, IFormattable
{
    /// <summary>
    ///     The vector <c>(0, 0, 1)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 0, 1)</c>.</value>
    public static readonly Vector3 Backward = new(0, 0, 1);

    /// <summary>
    ///     The vector <c>(0, -1, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(0, -1, 0)</c>.</value>
    public static readonly Vector3 Down = new(0, -1, 0);

    /// <summary>
    ///     The vector <c>(0, 0, -1)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 0, -1)</c>.</value>
    public static readonly Vector3 Forward = new(0, 0, -1);

    /// <summary>
    ///     The vector <c>(-1, 0, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(-1, 0, 0)</c>.</value>
    public static readonly Vector3 Left = new(-1, 0, 0);

    /// <summary>
    ///     A vector whose 3 elements are equal to one.
    /// </summary>
    /// <value>A vector whose three elements are equal to one (that is, the vector <c>(1, 1, 1)</c>).</value>
    public static readonly Vector3 One = new(1);

    /// <summary>
    ///     The vector <c>(1, 0, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(1, 0, 0)</c>.</value>
    public static readonly Vector3 Right = new(1, 0, 0);

    /// <summary>
    ///     The vector <c>(1, 0, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(1, 0, 0)</c>.</value>
    public static readonly Vector3 UnitX = new(1, 0, 0);

    /// <summary>
    ///     The vector <c>(0, 1, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 1, 0)</c>.</value>
    public static readonly Vector3 UnitY = new(0, 1, 0);

    /// <summary>
    ///     The vector <c>(0, 0, 1)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 0, 1)</c>.</value>
    public static readonly Vector3 UnitZ = new(0, 0, 1);

    /// <summary>
    ///     The vector <c>(0, 1, 0)</c>.
    /// </summary>
    /// <value>The vector <c>(0, 1, 0)</c>.</value>
    public static readonly Vector3 Up = new(0, 1, 0);

    /// <summary>
    ///     A vector whose 3 elements are equal to zero.
    /// </summary>
    /// <value>A vector whose three elements are equal to zero (that is, the vector <c>(0, 0, 0)</c>).</value>
    public static readonly Vector3 Zero = new(0);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector3" /> structure whose three elements have the same value.
    /// </summary>
    /// <param name="value">The value to assign to all three elements.</param>
    public Vector3(float value) : this(value, value, value)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector3" /> structure whose elements have the specified values.
    /// </summary>
    /// <param name="x">The value to assign to <see cref="X" />.</param>
    /// <param name="y">The value to assign to <see cref="Y" />.</param>
    /// <param name="z">The value to assign to <see cref="Z" />.</param>
    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Vector3" /> structure by copying the <see cref="Vector2.X" /> and
    ///     <see cref="Vector2.Y" /> of a <see cref="Vector2" /> to their respective component values, and assigning
    ///     <see cref="Z" /> a specified value.
    /// </summary>
    /// <param name="vector">The two-dimensional vector to copy.</param>
    /// <param name="z">The value to assign to <see cref="Z" />.</param>
    public Vector3(in Vector2 vector, float z) : this(vector.X, vector.Y, z)
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
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is less than 0 or greater than 2.
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
    public static Vector3 operator +(in Vector3 left, in Vector3 right)
    {
        return new Vector3(
            left.X + right.X,
            left.Y + right.Y,
            left.Z + right.Z
        );
    }

    /// <summary>
    ///     Subtracts the second vector from the first.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The difference vector.</returns>
    public static Vector3 operator -(in Vector3 left, in Vector3 right)
    {
        return new Vector3(
            left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z
        );
    }

    /// <summary>
    ///     Multiples two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The product vector.</returns>
    public static Vector3 operator *(in Vector3 left, in Vector3 right)
    {
        return new Vector3(
            left.X * right.X,
            left.Y * right.Y,
            left.Z * right.Z
        );
    }

    /// <summary>
    ///     Multiples a vector by the given scalar.
    /// </summary>
    /// <param name="left">The vector value.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector3 operator *(in Vector3 left, float right)
    {
        return new Vector3(
            left.X * right,
            left.Y * right,
            left.Z * right
        );
    }

    /// <summary>
    ///     Multiples a vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The vector value.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector3 operator *(float left, in Vector3 right)
    {
        return new Vector3(
            left * right.X,
            left * right.Y,
            left * right.Z
        );
    }

    /// <summary>
    ///     Rotates the specified point with the specified rotation. 
    /// </summary>
    /// <param name="rotation">The rotation.</param>
    /// <param name="point">The point.</param>
    /// <returns>The rotated point.</returns>
    public static Vector3 operator *(in Quaternion rotation, in Vector3 point)
    {
        // the internet wrote it, I just handed it in.
        // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Quaternion.cs
        float x = rotation.X * 2F;
        float y = rotation.Y * 2F;
        float z = rotation.Z * 2F;
        float xx = rotation.X * x;
        float yy = rotation.Y * y;
        float zz = rotation.Z * z;
        float xy = rotation.X * y;
        float xz = rotation.X * z;
        float yz = rotation.Y * z;
        float wx = rotation.W * x;
        float wy = rotation.W * y;
        float wz = rotation.W * z;

        (float px, float py, float pz) = point;

        return new Vector3(
            (1F - (yy + zz)) * px + (xy - wz) * py + (xz + wy) * pz,
            (xy + wz) * px + (1F - (xx + zz)) * py + (yz - wx) * pz,
            (xz - wy) * px + (yz + wx) * py + (1F - (xx + yy)) * pz);
    }

    /// <summary>
    ///     Divides the first vector by the second.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector3 operator /(in Vector3 left, in Vector3 right)
    {
        return new Vector3(
            left.X / right.X,
            left.Y / right.Y,
            left.Z / right.Z
        );
    }

    /// <summary>
    ///     Divides the vector by the given scalar.
    /// </summary>
    /// <param name="left">The vector value.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector3 operator /(in Vector3 left, float right)
    {
        return new Vector3(
            left.X / right,
            left.Y / right,
            left.Z / right
        );
    }

    /// <summary>
    ///     Divides the vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The vector value.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector3 operator /(float left, in Vector3 right)
    {
        return new Vector3(
            left / right.X,
            left / right.Y,
            left / right.Z
        );
    }

    /// <summary>
    ///     Negates a given vector.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The negated vector.</returns>
    public static Vector3 operator -(in Vector3 value)
    {
        return Zero - value;
    }

    /// <summary>
    ///     Returns a value indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns><see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(in Vector3 left, in Vector3 right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Returns a value indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns><see langword="true" /> if the two vectors are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(in Vector3 left, in Vector3 right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Explicitly converts a <see cref="Vector3" /> to a <see cref="Vector2" />, by assigning <see cref="X" /> and
    ///     <see cref="Y" /> to their corresponding component values in <paramref name="value" /> and assigning 0 to
    ///     <see cref="Z" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Vector3(in Vector2 value)
    {
        return new Vector3(value.X, value.Y, 0);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Vector3" /> to <see cref="Microsoft.Xna.Framework.Vector3" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Microsoft.Xna.Framework.Vector3(in Vector3 value)
    {
        return new Microsoft.Xna.Framework.Vector3(value.X, value.Y, value.Z);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Microsoft.Xna.Framework.Vector3" /> to <see cref="Vector3" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Vector3(in Microsoft.Xna.Framework.Vector3 value)
    {
        return new Vector3(value.X, value.Y, value.Z);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Vector3" /> to <see cref="System.Numerics.Vector3" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator System.Numerics.Vector3(in Vector3 value)
    {
        return new System.Numerics.Vector3(value.X, value.Y, value.Z);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="System.Numerics.Vector3" /> to <see cref="Vector3" />.
    /// </summary>
    /// <param name="value">The vector to convert.</param>
    /// <returns>The converted vector.</returns>
    public static implicit operator Vector3(in System.Numerics.Vector3 value)
    {
        return new Vector3(value.X, value.Y, value.Z);
    }

    /// <summary>
    ///     Returns a vector whose elements are the absolute values of each of the source vector's elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The absolute value vector.</returns>
    public static Vector3 Abs(in Vector3 value)
    {
        return new Vector3(
            MathF.Abs(value.X),
            MathF.Abs(value.Y),
            MathF.Abs(value.Z)
        );
    }

    /// <summary>
    ///     Restricts a vector between a minimum and maximum value.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <param name="min">The minimum vector</param>
    /// <param name="max">The maximum vector</param>
    /// <returns>The restricted vector.</returns>
    public static Vector3 Clamp(in Vector3 value, in Vector3 min, in Vector3 max)
    {
        return Min(Max(value, min), max);
    }

    /// <summary>
    ///     Computes the cross product of two vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The cross product.</returns>
    public static Vector3 Cross(in Vector3 left, in Vector3 right)
    {
        return new Vector3(
            left.Y * right.Z - left.Z * right.Y,
            left.Z * right.X - left.X * right.Z,
            left.X * right.Y - left.Y * right.X
        );
    }

    /// <summary>
    ///     Returns the Euclidean distance between two points.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>The distance.</returns>
    public static float Distance(in Vector3 left, in Vector3 right)
    {
        Vector3 difference = left - right;
        float dot = Dot(difference, difference);
        return MathF.Sqrt(dot);
    }

    /// <summary>
    ///     Returns the squared Euclidean distance between two points.
    /// </summary>
    /// <param name="left">The first point.</param>
    /// <param name="right">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static float DistanceSquared(in Vector3 left, in Vector3 right)
    {
        Vector3 difference = left - right;
        return Dot(difference, difference);
    }

    /// <summary>
    ///     Returns the dot product of two vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The dot product.</returns>
    public static float Dot(in Vector3 left, in Vector3 right)
    {
        return left.X * right.X +
               left.Y * right.Y +
               left.Z * right.Z;
    }

    /// <summary>
    ///     Performs a linear interpolation between two vectors based on a value that specifies the weighting of the second
    ///     vector.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <param name="alpha">The relative weight of <paramref name="right" /> in the interpolation.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector3 Lerp(in Vector3 left, in Vector3 right, float alpha)
    {
        Vector3 firstInfluence = left * (1.0f - alpha);
        Vector3 secondInfluence = right * alpha;
        return firstInfluence + secondInfluence;
    }

    /// <summary>
    ///     Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The maximized vector.</returns>
    public static Vector3 Max(in Vector3 left, in Vector3 right)
    {
        return new Vector3(
            MathF.Max(left.X, right.X),
            MathF.Max(left.Y, right.Y),
            MathF.Max(left.Z, right.Z)
        );
    }

    /// <summary>
    ///     Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The minimized vector.</returns>
    public static Vector3 Min(in Vector3 left, in Vector3 right)
    {
        return new Vector3(
            MathF.Min(left.X, right.X),
            MathF.Min(left.Y, right.Y),
            MathF.Min(left.Z, right.Z)
        );
    }

    /// <summary>
    ///     Returns a vector with the same direction as the given vector, but with a length of 1.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static Vector3 Normalize(in Vector3 value)
    {
        return value / value.Length;
    }

    /// <summary>
    ///     Returns the reflection of a vector off a surface that has the specified normal.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="normal">The normal of the surface off of which the vector is being reflected.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector3 Reflect(in Vector3 vector, in Vector3 normal)
    {
        float dot = Dot(vector, normal);
        Vector3 temp = normal * dot * 2.0f;
        return vector - temp;
    }

    /// <summary>
    ///     Returns a vector whose elements are the square root of each of the source vector's elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The square root vector.</returns>
    public static Vector3 SquareRoot(in Vector3 value)
    {
        return new(
            MathF.Sqrt(value.X),
            MathF.Sqrt(value.Y),
            MathF.Sqrt(value.Z)
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

        if (array.Length - index < 3)
            throw new ArgumentException("The number of elements in source vector is greater than the destination array.");

        array[index] = X;
        array[index + 1] = Y;
        array[index + 2] = Z;
    }

    /// <summary>
    ///     Deconstructs this vector.
    /// </summary>
    /// <param name="x">The X component value.</param>
    /// <param name="y">The Y component value.</param>
    /// <param name="z">The Z component value.</param>
    public void Deconstruct(out float x, out float y, out float z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    /// <summary>
    ///     Returns a value indicating whether this vector and another vector are equal.
    /// </summary>
    /// <param name="other">The vector to compare with this instance.</param>
    /// <returns><see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(Vector3 other)
    {
        return MathUtils.Approximately(X, other.X) &&
               MathUtils.Approximately(Y, other.Y) &&
               MathUtils.Approximately(Z, other.Z);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Vector3 other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
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
        builder.Append(X.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(Y.ToString(format, formatProvider));
        builder.Append(separator);
        builder.Append(' ');
        builder.Append(Z.ToString(format, formatProvider));
        builder.Append('>');
        return builder.ToString();
    }
}
