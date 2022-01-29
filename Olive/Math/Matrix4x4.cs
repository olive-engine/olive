using Microsoft.Xna.Framework;

namespace Olive.Math;

/// <summary>
///     Represents the right-handed 4x4 floating point matrix, which can store translation, scale and rotation information.
/// </summary>
public readonly struct Matrix4x4 : IEquatable<Matrix4x4>
{
    /// <summary>
    ///     The multiplicative identity matrix.
    /// </summary>
    /// <value>
    ///     The matrix with the values:
    ///     <code>
    ///         1, 0, 0, 0,
    ///         0, 1, 0, 0,
    ///         0, 0, 1, 0,
    ///         0, 0, 0, 1.
    ///     </code>
    /// </value>
    public static readonly Matrix4x4 Identity = new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Matrix4x4" /> structure.
    /// </summary>
    /// <param name="m11">The first row, first column value.</param>
    /// <param name="m12">The first row, second column value.</param>
    /// <param name="m13">The first row, third column value.</param>
    /// <param name="m14">The first row, fourth column value.</param>
    /// <param name="m21">The second row, first column value.</param>
    /// <param name="m22">The second row, second column value.</param>
    /// <param name="m23">The second row, third column value.</param>
    /// <param name="m24">The second row, fourth column value.</param>
    /// <param name="m31">The third row, first column value.</param>
    /// <param name="m32">The third row, second column value.</param>
    /// <param name="m33">The third row, third column value.</param>
    /// <param name="m34">The third row, fourth column value.</param>
    /// <param name="m41">The fourth row, first column value.</param>
    /// <param name="m42">The fourth row, second column value.</param>
    /// <param name="m43">The fourth row, third column value.</param>
    /// <param name="m44">The fourth row, fourth column value.</param>
    public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32,
        float m33, float m34, float m41, float m42, float m43, float m44)
    {
        M11 = m11;
        M12 = m12;
        M13 = m13;
        M14 = m14;
        M21 = m21;
        M22 = m22;
        M23 = m23;
        M24 = m24;
        M31 = m31;
        M32 = m32;
        M33 = m33;
        M34 = m34;
        M41 = m41;
        M42 = m42;
        M43 = m43;
        M44 = m44;
    }

    /// <summary>
    ///     Gets the backward vector formed from the third row <see cref="M31" />, <see cref="M32" />, and <see cref="M33" />
    ///     elements.
    /// </summary>
    /// <value>The backward vector.</value>
    public Vector3 Backward => new(M31, M32, M33);

    /// <summary>
    ///     Gets the down vector formed from the second row -<see cref="M21" />, -<see cref="M22" />, and -<see cref="M23"/>
    ///     elements.
    /// </summary>
    /// <value>The down vector.</value>
    public Vector3 Down => new(-M21, -M22, -M23);

    /// <summary>
    ///     Gets the forward vector formed from the second row -<see cref="M31" />, -<see cref="M32" />, and -<see cref="M33"/>
    ///     elements.
    /// </summary>
    /// <value>The forward vector.</value>
    public Vector3 Forward => new(-M31, -M32, -M33);

    /// <summary>
    ///     Gets the left vector formed from the second row -<see cref="M11" />, -<see cref="M12" />, and -<see cref="M13"/>
    ///     elements.
    /// </summary>
    /// <value>The left vector.</value>
    public Vector3 Left => new(-M11, -M12, -M13);

    /// <summary>
    ///     Gets or initializes the first row and first column value.
    /// </summary>
    /// <value>The first row and first column value.</value>
    public float M11 { get; init; }

    /// <summary>
    ///     Gets or initializes the first row and second column value.
    /// </summary>
    /// <value>The first row and second column value.</value>
    public float M12 { get; init; }

    /// <summary>
    ///     Gets or initializes the first row and third column value.
    /// </summary>
    /// <value>The first row and third column value.</value>
    public float M13 { get; init; }

    /// <summary>
    ///     Gets or initializes the first row and fourth column value.
    /// </summary>
    /// <value>The first row and fourth column value.</value>
    public float M14 { get; init; }

    /// <summary>
    ///     Gets or initializes the second row and first column value.
    /// </summary>
    /// <value>The second row and first column value.</value>
    public float M21 { get; init; }

    /// <summary>
    ///     Gets or initializes the second row and second column value.
    /// </summary>
    /// <value>The second row and second column value.</value>
    public float M22 { get; init; }

    /// <summary>
    ///     Gets or initializes the second row and third column value.
    /// </summary>
    /// <value>The second row and third column value.</value>
    public float M23 { get; init; }

    /// <summary>
    ///     Gets or initializes the second row and fourth column value.
    /// </summary>
    /// <value>The second row and fourth column value.</value>
    public float M24 { get; init; }

    /// <summary>
    ///     Gets or initializes the third row and first column value.
    /// </summary>
    /// <value>The third row and first column value.</value>
    public float M31 { get; init; }

    /// <summary>
    ///     Gets or initializes the third row and second column value.
    /// </summary>
    /// <value>The third row and second column value.</value>
    public float M32 { get; init; }

    /// <summary>
    ///     Gets or initializes the third row and third column value.
    /// </summary>
    /// <value>The third row and third column value.</value>
    public float M33 { get; init; }

    /// <summary>
    ///     Gets or initializes the third row and fourth column value.
    /// </summary>
    /// <value>The third row and fourth column value.</value>
    public float M34 { get; init; }

    /// <summary>
    ///     Gets or initializes the fourth row and first column value.
    /// </summary>
    /// <value>The fourth row and first column value.</value>
    public float M41 { get; init; }

    /// <summary>
    ///     Gets or initializes the fourth row and second column value.
    /// </summary>
    /// <value>The fourth row and second column value.</value>
    public float M42 { get; init; }

    /// <summary>
    ///     Gets or initializes the fourth row and third column value.
    /// </summary>
    /// <value>The fourth row and third column value.</value>
    public float M43 { get; init; }

    /// <summary>
    ///     Gets or initializes the fourth row and fourth column value.
    /// </summary>
    /// <value>The fourth row and fourth column value.</value>
    public float M44 { get; init; }

    /// <summary>
    ///     Gets the right vector formed from the second row <see cref="M11" />, <see cref="M12" />, and <see cref="M13"/>
    ///     elements.
    /// </summary>
    /// <value>The right vector.</value>
    public Vector3 Right => new(M11, M12, M13);

    /// <summary>
    ///     Gets the translation component of this matrix.
    /// </summary>
    /// <value>The translation component.</value>
    public Vector3 Translation => new(M41, M42, M43);

    /// <summary>
    ///     Gets the up vector formed from the second row <see cref="M21" />, <see cref="M22" />, and <see cref="M23"/>
    ///     elements.
    /// </summary>
    /// <value>The up vector.</value>
    public Vector3 Up => new(M21, M22, M23);

    /// <summary>
    ///     Gets the component value by a specified index.
    /// </summary>
    /// <param name="index">
    ///     <para><c>0</c> to retrieve the <see cref="M11" /> component.</para>
    ///     -or-
    ///     <para><c>1</c> to retrieve the <see cref="M12" /> component.</para>
    ///     -or-
    ///     <para><c>2</c> to retrieve the <see cref="M13" /> component.</para>
    ///     -or-
    ///     <para><c>3</c> to retrieve the <see cref="M14" /> component.</para>
    ///     -or-
    ///     <para><c>4</c> to retrieve the <see cref="M24" /> component.</para>
    ///     -or-
    ///     <para><c>5</c> to retrieve the <see cref="M24" /> component.</para>
    ///     -or-
    ///     <para><c>6</c> to retrieve the <see cref="M24" /> component.</para>
    ///     -or-
    ///     <para><c>7</c> to retrieve the <see cref="M24" /> component.</para>
    ///     -or-
    ///     <para><c>8</c> to retrieve the <see cref="M34" /> component.</para>
    ///     -or-
    ///     <para><c>9</c> to retrieve the <see cref="M34" /> component.</para>
    ///     -or-
    ///     <para><c>10</c> to retrieve the <see cref="M34" /> component.</para>
    ///     -or-
    ///     <para><c>11</c> to retrieve the <see cref="M34" /> component.</para>
    ///     -or-
    ///     <para><c>12</c> to retrieve the <see cref="M44" /> component.</para>
    ///     -or-
    ///     <para><c>13</c> to retrieve the <see cref="M44" /> component.</para>
    ///     -or-
    ///     <para><c>14</c> to retrieve the <see cref="M44" /> component.</para>
    ///     -or-
    ///     <para><c>15</c> to retrieve the <see cref="M44" /> component.</para>
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is less than 0 or greater than 15.
    /// </exception>
    public float this[int index]
    {
        get
        {
            return index switch
            {
                0 => M11,
                1 => M12,
                2 => M13,
                3 => M14,
                4 => M21,
                5 => M22,
                6 => M23,
                7 => M24,
                8 => M31,
                9 => M32,
                10 => M33,
                11 => M34,
                12 => M41,
                13 => M42,
                14 => M43,
                15 => M44,
                _ => throw new ArgumentOutOfRangeException(nameof(index))
            };
        }
        init
        {
            switch (index)
            {
                case 0:
                    M11 = value;
                    break;
                case 1:
                    M12 = value;
                    break;
                case 2:
                    M13 = value;
                    break;
                case 3:
                    M14 = value;
                    break;
                case 4:
                    M21 = value;
                    break;
                case 5:
                    M22 = value;
                    break;
                case 6:
                    M23 = value;
                    break;
                case 7:
                    M24 = value;
                    break;
                case 8:
                    M31 = value;
                    break;
                case 9:
                    M32 = value;
                    break;
                case 10:
                    M33 = value;
                    break;
                case 11:
                    M34 = value;
                    break;
                case 12:
                    M41 = value;
                    break;
                case 13:
                    M42 = value;
                    break;
                case 14:
                    M43 = value;
                    break;
                case 15:
                    M44 = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }

    /// <summary>
    ///     Gets the component value by a specified row and column.
    /// </summary>
    /// <param name="row">The row to retrieve.</param>
    /// <param name="column">The column to retrieve.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <para><paramref name="row" /> is less than 0 or greater than 3.</para>
    ///     -or-
    ///     <para><paramref name="column" /> is less than 0 or greater than 3.</para>
    /// </exception>
    public float this[int row, int column]
    {
        get
        {
            if (row is < 0 or > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (column is < 0 or > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            return this[row * 4 + column];
        }
        init => this[row * 4 + column] = value;
    }

    /// <summary>
    ///     Adds each element in one matrix with its corresponding element in a second matrix.
    /// </summary>
    /// <param name="left">The first matrix.</param>
    /// <param name="right">The second matrix.</param>
    /// <returns>The summed matrix.</returns>
    public static Matrix4x4 operator +(Matrix4x4 left, Matrix4x4 right)
    {
        return new Matrix4x4(
            left.M11 + right.M11,
            left.M12 + right.M22,
            left.M13 + right.M13,
            left.M14 + right.M24,
            left.M21 + right.M21,
            left.M22 + right.M22,
            left.M23 + right.M23,
            left.M24 + right.M24,
            left.M31 + right.M31,
            left.M32 + right.M32,
            left.M33 + right.M33,
            left.M34 + right.M34,
            left.M41 + right.M41,
            left.M42 + right.M42,
            left.M43 + right.M43,
            left.M44 + right.M44
        );
    }

    /// <summary>
    ///     Subtracts the elements in one matrix from its corresponding element in a second matrix.
    /// </summary>
    /// <param name="left">The first matrix.</param>
    /// <param name="right">The second matrix.</param>
    /// <returns>The difference matrix.</returns>
    public static Matrix4x4 operator -(Matrix4x4 left, Matrix4x4 right)
    {
        return new Matrix4x4(
            left.M11 - right.M11,
            left.M12 - right.M22,
            left.M13 - right.M13,
            left.M14 - right.M24,
            left.M21 - right.M21,
            left.M22 - right.M22,
            left.M23 - right.M23,
            left.M24 - right.M24,
            left.M31 - right.M31,
            left.M32 - right.M32,
            left.M33 - right.M33,
            left.M34 - right.M34,
            left.M41 - right.M41,
            left.M42 - right.M42,
            left.M43 - right.M43,
            left.M44 - right.M44
        );
    }

    /// <summary>
    ///     Returns the matrix that results from multiplying two matrices together.
    /// </summary>
    /// <param name="left">The first matrix.</param>
    /// <param name="right">The second matrix.</param>
    /// <returns>The product matrix.</returns>
    public static Matrix4x4 operator *(Matrix4x4 left, Matrix4x4 right)
    {
        float m11 = left.M11 * right.M11 + left.M12 * right.M21 + left.M13 * right.M31 + left.M14 * right.M41;
        float m12 = left.M11 * right.M12 + left.M12 * right.M22 + left.M13 * right.M32 + left.M14 * right.M42;
        float m13 = left.M11 * right.M13 + left.M12 * right.M23 + left.M13 * right.M33 + left.M14 * right.M43;
        float m14 = left.M11 * right.M14 + left.M12 * right.M24 + left.M13 * right.M34 + left.M14 * right.M44;
        float m21 = left.M21 * right.M11 + left.M22 * right.M21 + left.M23 * right.M31 + left.M24 * right.M41;
        float m22 = left.M21 * right.M12 + left.M22 * right.M22 + left.M23 * right.M32 + left.M24 * right.M42;
        float m23 = left.M21 * right.M13 + left.M22 * right.M23 + left.M23 * right.M33 + left.M24 * right.M43;
        float m24 = left.M21 * right.M14 + left.M22 * right.M24 + left.M23 * right.M34 + left.M24 * right.M44;
        float m31 = left.M31 * right.M11 + left.M32 * right.M21 + left.M33 * right.M31 + left.M34 * right.M41;
        float m32 = left.M31 * right.M12 + left.M32 * right.M22 + left.M33 * right.M32 + left.M34 * right.M42;
        float m33 = left.M31 * right.M13 + left.M32 * right.M23 + left.M33 * right.M33 + left.M34 * right.M43;
        float m34 = left.M31 * right.M14 + left.M32 * right.M24 + left.M33 * right.M34 + left.M34 * right.M44;
        float m41 = left.M41 * right.M11 + left.M42 * right.M21 + left.M43 * right.M31 + left.M44 * right.M41;
        float m42 = left.M41 * right.M12 + left.M42 * right.M22 + left.M43 * right.M32 + left.M44 * right.M42;
        float m43 = left.M41 * right.M13 + left.M42 * right.M23 + left.M43 * right.M33 + left.M44 * right.M43;
        float m44 = left.M41 * right.M14 + left.M42 * right.M24 + left.M43 * right.M34 + left.M44 * right.M44;

        return new Matrix4x4(m11, m12, m13, m14,
            m21, m22, m23, m24,
            m31, m32, m33, m34,
            m41, m42, m43, m44);
    }

    /// <summary>
    ///     Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="scalar">The scale factor.</param>
    /// <returns>The scaled matrix.</returns>
    public static Matrix4x4 operator *(Matrix4x4 matrix, float scalar)
    {
        return new Matrix4x4(
            matrix.M11 * scalar,
            matrix.M12 * scalar,
            matrix.M13 * scalar,
            matrix.M14 * scalar,
            matrix.M21 * scalar,
            matrix.M22 * scalar,
            matrix.M23 * scalar,
            matrix.M24 * scalar,
            matrix.M31 * scalar,
            matrix.M32 * scalar,
            matrix.M33 * scalar,
            matrix.M34 * scalar,
            matrix.M41 * scalar,
            matrix.M42 * scalar,
            matrix.M43 * scalar,
            matrix.M44 * scalar
        );
    }

    /// <summary>
    ///     Negates the specified matrix.
    /// </summary>
    /// <param name="value">The matrix to negate.</param>
    /// <returns>The negated matrix.</returns>
    public static Matrix4x4 operator -(Matrix4x4 value)
    {
        return new Matrix4x4(
            -value.M11,
            -value.M22,
            -value.M13,
            -value.M24,
            -value.M21,
            -value.M22,
            -value.M23,
            -value.M24,
            -value.M31,
            -value.M32,
            -value.M33,
            -value.M34,
            -value.M41,
            -value.M42,
            -value.M43,
            -value.M44
        );
    }

    /// <summary>
    ///     Returns a value indicating whether the two given matrices are equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns><see langword="true" /> if the two matrices are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(Matrix4x4 left, Matrix4x4 right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Returns a value indicating whether the two given matrices are not equal.
    /// </summary>
    /// <param name="left">The first matrix to compare.</param>
    /// <param name="right">The second matrix to compare.</param>
    /// <returns><see langword="true" /> if the two matrices are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(Matrix4x4 left, Matrix4x4 right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Matrix4x4" /> to <see cref="Microsoft.Xna.Framework.Matrix" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator Matrix(Matrix4x4 value)
    {
        return new Matrix(value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M43, value.M44);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Microsoft.Xna.Framework.Matrix" /> to <see cref="Matrix4x4" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator Matrix4x4(Matrix value)
    {
        return new Matrix4x4(value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M43, value.M44);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Matrix4x4" /> to <see cref="System.Numerics.Matrix4x4" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator System.Numerics.Matrix4x4(Matrix4x4 value)
    {
        return new System.Numerics.Matrix4x4(value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M43, value.M44);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="System.Numerics.Matrix4x4" /> to <see cref="Matrix4x4" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator Matrix4x4(System.Numerics.Matrix4x4 value)
    {
        return new Matrix4x4(value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M43, value.M44);
    }

    /// <summary>
    ///     Transposes the rows and columns of a matrix.
    /// </summary>
    /// <param name="value">The matrix to transpose.</param>
    /// <returns>The transposed matrix.</returns>
    public static Matrix4x4 Transpose(Matrix4x4 value)
    {
        return new Matrix4x4(value.M11, value.M21, value.M31, value.M41,
            value.M12, value.M22, value.M32, value.M42,
            value.M13, value.M23, value.M33, value.M43,
            value.M14, value.M24, value.M34, value.M44);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Matrix4x4 other && Equals(other);
    }

    /// <summary>
    ///     Returns a value indicating whether this matrix and another matrix are equal.
    /// </summary>
    /// <param name="other">The matrix to compare with this instance.</param>
    /// <returns><see langword="true" /> if the two matrices are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(Matrix4x4 other)
    {
        return MathUtils.Approximately(M11, other.M11) &&
               MathUtils.Approximately(M12, other.M12) &&
               MathUtils.Approximately(M13, other.M13) &&
               MathUtils.Approximately(M14, other.M14) &&
               MathUtils.Approximately(M21, other.M21) &&
               MathUtils.Approximately(M22, other.M22) &&
               MathUtils.Approximately(M23, other.M23) &&
               MathUtils.Approximately(M24, other.M24) &&
               MathUtils.Approximately(M31, other.M31) &&
               MathUtils.Approximately(M32, other.M32) &&
               MathUtils.Approximately(M33, other.M33) &&
               MathUtils.Approximately(M34, other.M34) &&
               MathUtils.Approximately(M41, other.M41) &&
               MathUtils.Approximately(M42, other.M42) &&
               MathUtils.Approximately(M43, other.M43) &&
               MathUtils.Approximately(M44, other.M44);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(M11);
        hashCode.Add(M12);
        hashCode.Add(M13);
        hashCode.Add(M14);
        hashCode.Add(M21);
        hashCode.Add(M22);
        hashCode.Add(M23);
        hashCode.Add(M24);
        hashCode.Add(M31);
        hashCode.Add(M32);
        hashCode.Add(M33);
        hashCode.Add(M34);
        hashCode.Add(M41);
        hashCode.Add(M42);
        hashCode.Add(M43);
        hashCode.Add(M44);
        return hashCode.ToHashCode();
    }
}
