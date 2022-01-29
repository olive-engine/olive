using System.Globalization;
using System.Text;
using Microsoft.Xna.Framework;

namespace Olive.Math;

// see: https://source.dot.net/#System.Private.CoreLib/Quaternion.cs
// also: https://github.com/MonoGame/MonoGame/blob/develop/MonoGame.Framework/Quaternion.cs
// thank goodness for open source, am I right?

/// <summary>
///     Represents a vector that is used to encode three-dimensional physical rotations.
/// </summary>
/// <remarks>
///     <para>
///         The <see cref="Quaternion" /> structure is used to efficiently rotate an object about the (x,y,z) vector by the angle
///         theta, where: <c>w = cos(theta/2)</c>.
///     </para>
///     <para>
///         This structure is designed to closely resemble the APIs of the built in .NET Quaternion structure, with inspiration
///         from MonoGame and Unity. 
///     </para>
/// </remarks>
public readonly struct Quaternion : IEquatable<Quaternion>, IFormattable
{
    private const float SlerpEpsilon = 1e-6f;

    /// <summary>
    ///     A quaternion that represents no rotation.
    /// </summary>
    /// <value>A quaternion whose values are <c>(0, 0, 0, 1)</c>.</value>
    public static readonly Quaternion Identity = new(0, 0, 0, 1);

    /// <summary>
    ///     A quaternion that represents a zero.
    /// </summary>
    /// <value>A quaternion whose values are <c>(0, 0, 0, 0)</c>.</value>
    public static readonly Quaternion Zero = new(0, 0, 0, 0);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Quaternion" /> structure from specified vector and scalar component
    ///     values.
    /// </summary>
    /// <param name="vector">The vector part of the quaternion.</param>
    /// <param name="scalar">The scalar part of the quaternion.</param>
    public Quaternion(Vector3 vector, float scalar) : this(vector.X, vector.Y, vector.Z, scalar)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Quaternion" /> structure whose elements have the specified values.
    /// </summary>
    /// <param name="x">The value to assign to <see cref="X" />.</param>
    /// <param name="y">The value to assign to <see cref="Y" />.</param>
    /// <param name="z">The value to assign to <see cref="Z" />.</param>
    /// <param name="w">The value to assign to <see cref="W" />.</param>
    public Quaternion(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    ///     Gets the euler angle representation of this quaternion.
    /// </summary>
    /// <value>A <see cref="Vector3" /> representing the euler angles of this quaternion.</value>
    public Vector3 EulerAngles
    {
        get
        {
            // the internet wrote it, I just handed it in.
            // https://www.dreamincode.net/forums/topic/349917-convert-from-quaternion-to-euler-angles-vector3/
            float q0 = W;
            float q1 = Y;
            float q2 = X;
            float q3 = Z;

            float x = MathF.Atan2(2 * (q0 * q1 + q2 * q3), 1 - 2 * (MathF.Pow(q1, 2) + MathF.Pow(q2, 2)));
            float y = MathF.Asin(2 * (q0 * q2 - q3 * q1));
            float z = MathF.Atan2(2 * (q0 * q3 + q1 * q2), 1 - 2 * (MathF.Pow(q2, 2) + MathF.Pow(q3, 2)));

            x = MathHelper.ToDegrees(x);
            y = MathHelper.ToDegrees(y);
            z = MathHelper.ToDegrees(z);

            return new Vector3(x, y, z);

            // legacy implementation may come in handy at some point (?)
            // yeah, yeah, zombie code is bad code. to that I say: fuck you.
            // https://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
            /*float a = 2.0f * value.Y * value.W - 2.0f * value.X * value.Z;
            float b = 1.0f - 2.0f * value.Y * value.Y - 2.0f * value.Z * value.Z;
            float y = -MathF.Atan2(a, b);

            a = 2.0f * value.X * value.Y;
            b = 2.0f * value.Z * value.W;
            float z = MathF.Asin(a + b);

            a = 2.0f * value.X * value.W - 2.0f * value.Y * value.Z;
            b = 1.0f - 2.0f * value.X * value.X - 2.0f * value.Z * value.Z;
            float x = MathF.Atan2(a, b);

            if (!radians)
            {
                x = MathHelper.ToDegrees(x);
                y = MathHelper.ToDegrees(y);
                z = MathHelper.ToDegrees(z);
            }

            return new Vector3(x, y, z);*/
        }
    }

    /// <summary>
    ///     Gets a value indicating whether this quaternion is the identity quaternion.
    /// </summary>
    /// <value><see langword="true" /> if this instance is the identity quaternion; otherwise, <see langword="false" />.</value>
    public bool IsIdentity => this == Identity;

    /// <summary>
    ///     Gets the length of the quaternion.
    /// </summary>
    /// <value>The length of the quaternion.</value>
    /// <seealso cref="LengthSquared" />
    public float Length => MathF.Sqrt(LengthSquared);

    /// <summary>
    ///     Gets the squared length of the quaternion.
    /// </summary>
    /// <value>The squared length of the quaternion.</value>
    /// <seealso cref="Length" />
    public float LengthSquared => X * X + Y * Y + Z * Z + W * W;

    /// <summary>
    ///     Gets or initializes the value of the X component.
    /// </summary>
    /// <value>The value of the X component.</value>
    public float X { get; init; }

    /// <summary>
    ///     Gets or initializes the value of the Y component.
    /// </summary>
    /// <value>The value of the Y component.</value>
    public float Y { get; init; }

    /// <summary>
    ///     Gets or initializes the value of the Z component.
    /// </summary>
    /// <value>The value of the Z component.</value>
    public float Z { get; init; }

    /// <summary>
    ///     Gets or initializes the value of the W component.
    /// </summary>
    /// <value>The value of the W component.</value>
    public float W { get; init; }

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
    ///     Adds each element in one quaternion with its corresponding element in a second quaternion.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <returns>
    ///     The quaternion that contains the summed values of <paramref name="left" /> and <paramref name="right" />.
    /// </returns>
    /// <remarks>
    ///     The <see cref="op_Addition" /> method defines the operation of the addition operator for <see cref="Quaternion" />
    ///     objects.
    /// </remarks>
    public static Quaternion operator +(Quaternion left, Quaternion right)
    {
        float x = left.X + right.X;
        float y = left.Y + right.Y;
        float z = left.Z + right.Z;
        float w = left.W + right.W;
        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Adds each element in one quaternion with its corresponding element in a second quaternion.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <returns>The quaternion that contains the summed values of <paramref name="left" /> and <paramref name="right" />.</returns>
    /// <remarks>
    ///     The <see cref="op_Subtraction" /> method defines the operation of the subtraction operator for
    ///     <see cref="Quaternion" /> objects.
    /// </remarks>
    public static Quaternion operator -(Quaternion left, Quaternion right)
    {
        float x = left.X - right.X;
        float y = left.Y - right.Y;
        float z = left.Z - right.Z;
        float w = left.W - right.W;
        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Divides one quaternion by a second quaternion.
    /// </summary>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>The quaternion that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
    /// <remarks>
    ///     The <see cref="op_Division" /> method defines the operation of the subtraction operator for
    ///     <see cref="Quaternion" /> objects.
    /// </remarks>
    public static Quaternion operator /(Quaternion left, Quaternion right)
    {
        (float q1X, float q1Y, float q1Z, float q1W) = left;

        //-------------------------------------
        // Inverse part.
        float ls = right.X * right.X + right.Y * right.Y +
                   right.Z * right.Z + right.W * right.W;
        float invNorm = 1.0f / ls;

        float q2X = -right.X * invNorm;
        float q2Y = -right.Y * invNorm;
        float q2Z = -right.Z * invNorm;
        float q2W = right.W * invNorm;

        //-------------------------------------
        // Multiply part.

        // cross(av, bv)
        float cx = q1Y * q2Z - q1Z * q2Y;
        float cy = q1Z * q2X - q1X * q2Z;
        float cz = q1X * q2Y - q1Y * q2X;

        float dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

        float x = q1X * q2W + q2X * q1W + cx;
        float y = q1Y * q2W + q2Y * q1W + cy;
        float z = q1Z * q2W + q2Z * q1W + cz;
        float w = q1W * q2W - dot;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Returns the quaternion that results from multiplying two quaternions together.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <returns>The product quaternion.</returns>
    /// <remarks>
    ///     The <see cref="op_Multiply(Quaternion, Quaternion)" /> method defines the operation of the multiplication operator for
    ///     <see cref="Quaternion" /> objects.
    /// </remarks>
    public static Quaternion operator *(Quaternion left, Quaternion right)
    {
        (float q1X, float q1Y, float q1Z, float q1W) = left;
        (float q2X, float q2Y, float q2Z, float q2W) = right;

        // cross(av, bv)
        float cx = q1Y * q2Z - q1Z * q2Y;
        float cy = q1Z * q2X - q1X * q2Z;
        float cz = q1X * q2Y - q1Y * q2X;

        float dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

        float x = q1X * q2W + q2X * q1W + cx;
        float y = q1Y * q2W + q2Y * q1W + cy;
        float z = q1Z * q2W + q2Z * q1W + cz;
        float w = q1W * q2W - dot;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Returns the quaternion that results from scaling all the components of a specified quaternion by a scalar factor.
    /// </summary>
    /// <param name="quaternion">The source quaternion.</param>
    /// <param name="scalar">The scalar value.</param>
    /// <returns>The scaled quaternion.</returns>
    /// <remarks>
    ///     The <see cref="op_Multiply(Quaternion, float)" /> method defines the operation of the multiplication operator for
    ///     <see cref="Quaternion" /> objects.
    /// </remarks>
    public static Quaternion operator *(Quaternion quaternion, float scalar)
    {
        float x = quaternion.X * scalar;
        float y = quaternion.Y * scalar;
        float z = quaternion.Z * scalar;
        float w = quaternion.W * scalar;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Reverses the sign of each component of the quaternion.
    /// </summary>
    /// <param name="value">The quaternion to negate.</param>
    /// <returns>The negated quaternion.</returns>
    /// <remarks>
    ///     The <see cref="op_UnaryNegation" /> method defines the operation of the unary negation operator for
    ///     <see cref="Quaternion" /> objects.
    /// </remarks>
    public static Quaternion operator -(Quaternion value)
    {
        return Zero - value;
    }

    /// <summary>
    ///     Returns a value indicating whether the two given quaternions are equal.
    /// </summary>
    /// <param name="left">The first quaternion to compare.</param>
    /// <param name="right">The second quaternion to compare.</param>
    /// <returns><see langword="true" /> if the two quaternions are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(Quaternion left, Quaternion right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Returns a value indicating whether the two given quaternions are not equal.
    /// </summary>
    /// <param name="left">The first quaternion to compare.</param>
    /// <param name="right">The second quaternion to compare.</param>
    /// <returns><see langword="true" /> if the two quaternions are not equal; otherwise, <see langword="false" />.</returns>
    public static bool operator !=(Quaternion left, Quaternion right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Quaternion" /> to <see cref="Microsoft.Xna.Framework.Quaternion" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator Microsoft.Xna.Framework.Quaternion(Quaternion value)
    {
        return new Microsoft.Xna.Framework.Quaternion(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Microsoft.Xna.Framework.Quaternion" /> to <see cref="Quaternion" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator Quaternion(Microsoft.Xna.Framework.Quaternion value)
    {
        return new Quaternion(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="Quaternion" /> to <see cref="System.Numerics.Quaternion" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator System.Numerics.Quaternion(Quaternion value)
    {
        return new System.Numerics.Quaternion(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Implicitly converts from <see cref="System.Numerics.Quaternion" /> to <see cref="Quaternion" />.
    /// </summary>
    /// <param name="value">The quaternion to convert.</param>
    /// <returns>The converted quaternion.</returns>
    public static implicit operator Quaternion(System.Numerics.Quaternion value)
    {
        return new Quaternion(value.X, value.Y, value.Z, value.W);
    }

    /// <summary>
    ///     Creates a quaternion from a unit vector and an angle to rotate around the vector.
    /// </summary>
    /// <param name="axis">The unit vector to rotate around.</param>
    /// <param name="angle">The angle, in radians, to rotate around the vector.</param>
    /// <returns>The newly created quaternion.</returns>
    /// <remarks>
    ///     <paramref name="axis" /> vector must be normalized before calling this method or the resulting
    ///     <see cref="Quaternion" /> will be incorrect.
    /// </remarks>
    public static Quaternion AxisAngle(Vector3 axis, float angle)
    {
        float halfAngle = angle * 0.5f;
        float s = MathF.Sin(halfAngle);
        float c = MathF.Cos(halfAngle);

        float x = axis.X * s;
        float y = axis.Y * s;
        float z = axis.Z * s;
        float w = c;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Concatenates two quaternions.
    /// </summary>
    /// <param name="left">The first quaternion rotation in the series.</param>
    /// <param name="right">The second quaternion rotation in the series.</param>
    /// <returns>
    ///     A new quaternion representing the concatenation of the <paramref name="left" /> rotation followed by the
    ///     <paramref name="right" /> rotation.
    /// </returns>
    public static Quaternion Concatenate(Quaternion left, Quaternion right)
    {
        // concatenate rotation is actually q2 * q1 instead of q1 * q2.
        return right * left;
    }

    /// <summary>
    ///     Returns the conjugate of a specified quaternion.
    /// </summary>
    /// <param name="value">The quaternion.</param>
    /// <returns>A new quaternion that is the conjugate of <see langword="value" />.</returns>
    public static Quaternion Conjugate(Quaternion value)
    {
        float x = -value.X;
        float y = -value.Y;
        float z = -value.Z;
        float w = value.W;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Calculates the dot product of two quaternions.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <returns>The dot product.</returns>
    public static float Dot(Quaternion left, Quaternion right)
    {
        return left.X * right.X +
               left.Y * right.Y +
               left.Z * right.Z +
               left.W * right.W;
    }

    /// <summary>
    ///     Creates a new quaternion from the given yaw, pitch, and roll.
    /// </summary>
    /// <param name="x">The pitch angle, in degrees, around the X axis.</param>
    /// <param name="y">The yaw angle, in degrees, around the Y axis.</param>
    /// <param name="z">The roll angle, in degrees, around the Z axis.</param>
    /// <returns>The resulting quaternion.</returns>
    public static Quaternion Euler(float x, float y, float z)
    {
        //  Roll first, about axis the object is facing, then
        //  pitch upward, then yaw to face into the new heading
        float halfRoll = z * 0.5f;
        float sr = MathF.Sin(halfRoll);
        float cr = MathF.Cos(halfRoll);

        float halfPitch = x * 0.5f;
        float sp = MathF.Sin(halfPitch);
        float cp = MathF.Cos(halfPitch);

        float halfYaw = y * 0.5f;
        float sy = MathF.Sin(halfYaw);
        float cy = MathF.Cos(halfYaw);

        x = cy * sp * cr + sy * cp * sr;
        y = sy * cp * cr - cy * sp * sr;
        z = cy * cp * sr - sy * sp * cr;
        float w = cy * cp * cr + sy * sp * sr;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Creates a new quaternion from the given euler angles.
    /// </summary>
    /// <param name="euler">The euler angles.</param>
    /// <returns>The resulting quaternion.</returns>
    public static Quaternion Euler(Vector3 euler)
    {
        (float x, float y, float z) = euler;
        return Euler(x, y, z);
    }

    /// <summary>
    ///     Returns the inverse of a quaternion.
    /// </summary>
    /// <param name="value">The quaternion.</param>
    /// <returns>The inverted quaternion.</returns>
    public static Quaternion Inverse(Quaternion value)
    {
        //  -1   (       a              -v       )
        // q   = ( -------------   ------------- )
        //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
        (float x, float y, float z, float w) = value;
        float ls = x * x + y * y + z * z + w * w;
        float invNorm = 1.0f / ls;

        x = -x * invNorm;
        y = -y * invNorm;
        z = -z * invNorm;
        w *= invNorm;
        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Performs a linear interpolation between two quaternions based on a value that specifies the weighting of the second
    ///     quaternion.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <param name="alpha">The relative weight of <paramref name="right" /> in the interpolation.</param>
    /// <returns>The interpolated quaternion.</returns>
    public static Quaternion Lerp(Quaternion left, Quaternion right, float alpha)
    {
        float oneMinusAlpha = 1.0f - alpha;
        float x, y, z, w;

        float dot = left.X * right.X + left.Y * right.Y +
                    left.Z * right.Z + left.W * right.W;

        if (dot >= 0.0f)
        {
            x = oneMinusAlpha * left.X + alpha * right.X;
            y = oneMinusAlpha * left.Y + alpha * right.Y;
            z = oneMinusAlpha * left.Z + alpha * right.Z;
            w = oneMinusAlpha * left.W + alpha * right.W;
        }
        else
        {
            x = oneMinusAlpha * left.X - alpha * right.X;
            y = oneMinusAlpha * left.Y - alpha * right.Y;
            z = oneMinusAlpha * left.Z - alpha * right.Z;
            w = oneMinusAlpha * left.W - alpha * right.W;
        }

        // Normalize it.
        float ls = x * x + y * y + z * z + w * w;
        float invNorm = 1.0f / MathF.Sqrt(ls);

        x *= invNorm;
        y *= invNorm;
        z *= invNorm;
        w *= invNorm;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Creates a new <see cref="Quaternion" /> from the specified <see cref="Matrix4x4" />.
    /// </summary>
    /// <param name="matrix">The rotation matrix.</param>
    /// <returns>A quaternion composed from the rotation part of the matrix.</returns>
    public static Quaternion Matrix(Matrix4x4 matrix)
    {
        float x, y, w, z;
        float sqrt;
        float half;
        float scale = matrix.M11 + matrix.M22 + matrix.M33;

        if (scale > 0.0f)
        {
            sqrt = MathF.Sqrt(scale + 1.0f);
            w = sqrt * 0.5f;
            sqrt = 0.5f / sqrt;

            x = (matrix.M23 - matrix.M32) * sqrt;
            y = (matrix.M31 - matrix.M13) * sqrt;
            z = (matrix.M12 - matrix.M21) * sqrt;
            return new Quaternion(x, y, z, w);
        }

        if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
        {
            sqrt = MathF.Sqrt(1.0f + matrix.M11 - matrix.M22 - matrix.M33);
            half = 0.5f / sqrt;

            x = 0.5f * sqrt;
            y = (matrix.M12 + matrix.M21) * half;
            z = (matrix.M13 + matrix.M31) * half;
            w = (matrix.M23 - matrix.M32) * half;
            return new Quaternion(x, y, z, w);
        }

        if (matrix.M22 > matrix.M33)
        {
            sqrt = MathF.Sqrt(1.0f + matrix.M22 - matrix.M11 - matrix.M33);
            half = 0.5f / sqrt;

            x = (matrix.M21 + matrix.M12) * half;
            y = 0.5f * sqrt;
            z = (matrix.M32 + matrix.M23) * half;
            w = (matrix.M31 - matrix.M13) * half;
            return new Quaternion(x, y, z, w);
        }

        sqrt = MathF.Sqrt(1.0f + matrix.M33 - matrix.M11 - matrix.M22);
        half = 0.5f / sqrt;

        x = (matrix.M31 + matrix.M13) * half;
        y = (matrix.M32 + matrix.M23) * half;
        z = 0.5f * sqrt;
        w = (matrix.M12 - matrix.M21) * half;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Divides each component of a specified <see cref="Quaternion" /> by its length.
    /// </summary>
    /// <param name="value">The quaternion to normalize.</param>
    /// <returns>The normalized quaternion.</returns>
    public static Quaternion Normalize(Quaternion value)
    {
        (float x, float y, float z, float w) = value;
        float ls = x * x + y * y + z * z + w * w;
        float invNorm = 1.0f / MathF.Sqrt(ls);

        x *= invNorm;
        y *= invNorm;
        z *= invNorm;
        w *= invNorm;

        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Interpolates between two quaternions, using spherical linear interpolation.
    /// </summary>
    /// <param name="left">The first quaternion.</param>
    /// <param name="right">The second quaternion.</param>
    /// <param name="alpha">The relative weight of the second quaternion in the interpolation.</param>
    /// <returns>The interpolated quaternion.</returns>
    public static Quaternion Slerp(Quaternion left, Quaternion right, float alpha)
    {
        float cosOmega = left.X * right.X + left.Y * right.Y +
                         left.Z * right.Z + left.W * right.W;

        var flip = false;

        if (cosOmega < 0.0f)
        {
            flip = true;
            cosOmega = -cosOmega;
        }

        float s1, s2;

        if (cosOmega > 1.0f - SlerpEpsilon)
        {
            // Too close, do straight linear interpolation.
            s1 = 1.0f - alpha;
            s2 = flip ? -alpha : alpha;
        }
        else
        {
            float omega = MathF.Acos(cosOmega);
            float invSinOmega = 1 / MathF.Sin(omega);

            s1 = MathF.Sin((1.0f - alpha) * omega) * invSinOmega;
            s2 = flip
                ? -MathF.Sin(alpha * omega) * invSinOmega
                : MathF.Sin(alpha * omega) * invSinOmega;
        }

        float x = s1 * left.X + s2 * right.X;
        float y = s1 * left.Y + s2 * right.Y;
        float z = s1 * left.Z + s2 * right.Z;
        float w = s1 * left.W + s2 * right.W;
        return new Quaternion(x, y, z, w);
    }

    /// <summary>
    ///     Copies the contents of the quaternion into the given array.
    /// </summary>
    /// <param name="array">The destination array.</param>
    /// <param name="index">The starting index in the array to which the values should be written.</param>
    /// <exception cref="ArgumentNullException"><paramref name="array" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="index" /> is outside of the bounds of the array.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     The number of elements in the quaternion is greater than the size of <paramref name="array" />.
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
    ///     Deconstructs this quaternion.
    /// </summary>
    /// <param name="x">The X component value.</param>
    /// <param name="y">The Y component value.</param>
    /// <param name="z">The Z component value.</param>
    /// <param name="w">The W component value.</param>
    public void Deconstruct(out float x, out float y, out float z, out float w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }

    /// <summary>
    ///     Deconstructs this quaternion.
    /// </summary>
    /// <param name="vector">The vector value.</param>
    /// <param name="scalar">The scalar value.</param>
    /// <remarks>
    ///     This method constructs a <see cref="Vector3" /> out of the <see cref="X" />, <see cref="Y" />, and <see cref="Z" />
    ///     components, and does not provide axis/angle output. See <see cref="ToAxisAngle" />.
    /// </remarks>
    /// <seealso cref="ToAxisAngle" />
    public void Deconstruct(out Vector3 vector, out float scalar)
    {
        vector = new Vector3(X, Y, Z);
        scalar = W;
    }

    /// <summary>
    ///     Returns a value indicating whether this quaternion and another quaternion are equal.
    /// </summary>
    /// <param name="other">The quaternion to compare with this instance.</param>
    /// <returns><see langword="true" /> if the two quaternions are equal; otherwise, <see langword="false" />.</returns>
    public bool Equals(Quaternion other)
    {
        return MathUtils.Approximately(X, other.X) &&
               MathUtils.Approximately(Y, other.Y) &&
               MathUtils.Approximately(Z, other.Z) &&
               MathUtils.Approximately(W, other.W);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Quaternion other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }

    /// <summary>
    ///     Converts this quaternion to axis/angle values.
    /// </summary>
    /// <param name="axis">The destination of the axis component.</param>
    /// <param name="angle">The destination of the angle component.</param>
    /// <remarks>
    ///     This method provides axis/angle output, and does not construct the axis out of the <see cref="X" />, <see cref="Y" />,
    ///     and <see cref="Z" /> components. See <see cref="Deconstruct(out float, out float, out float, out float)" />.
    /// </remarks>
    /// <seealso cref="Deconstruct(out float, out float, out float, out float)" />
    public void ToAxisAngle(out Vector3 axis, out float angle)
    {
        // the internet wrote it, I just handed it in.
        // https://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToAngle/index.htm
        angle = 2.0f * MathF.Acos(W);

        float x = X / MathF.Sqrt(1.0f - W * W);
        float y = Y / MathF.Sqrt(1.0f - W * W);
        float z = Z / MathF.Sqrt(1.0f - W * W);

        axis = new Vector3(x, y, z);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Quaternion" /> instance.
    /// </summary>
    /// <returns>The string representation.</returns>
    public override string ToString()
    {
        return ToString("G", CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Quaternion" /> instance, using the specified format to
    ///     format individual elements.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <returns>The string representation.</returns>
    public string ToString(string? format)
    {
        return ToString(format, CultureInfo.CurrentCulture);
    }

    /// <summary>
    ///     Returns a <see cref="string" /> representing this <see cref="Quaternion" /> instance, using the specified format to
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
