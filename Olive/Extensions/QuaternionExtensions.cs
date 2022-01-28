using Microsoft.Xna.Framework;

namespace Olive.Extensions;

internal static class QuaternionExtensions
{
    public static Vector3 Multiply(this Quaternion rotation, Vector3 point)
    {
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

        Vector3 res;
        res.X = (1F - (yy + zz)) * px + (xy - wz) * py + (xz + wy) * pz;
        res.Y = (xy + wz) * px + (1F - (xx + zz)) * py + (yz - wx) * pz;
        res.Z = (xz - wy) * px + (yz + wx) * py + (1F - (xx + yy)) * pz;

        return res;
    }

    public static Quaternion ToQuaternion(this Vector3 value, bool radians = false)
    {
        (float x, float y, float z) = value;
        if (!radians)
        {
            x = MathHelper.ToRadians(x);
            y = MathHelper.ToRadians(y);
            z = MathHelper.ToRadians(z);
        }

        return Quaternion.CreateFromYawPitchRoll(y, x, z);
    }


    public static Vector3 ToEulerAngles(this Quaternion value, bool radians = false)
    {
        // https://www.dreamincode.net/forums/topic/349917-convert-from-quaternion-to-euler-angles-vector3/
        float q0 = value.W;
        float q1 = value.Y;
        float q2 = value.X;
        float q3 = value.Z;

        var result = new Vector3
        {
            Y = MathF.Atan2(2 * (q0 * q1 + q2 * q3), 1 - 2 * (MathF.Pow(q1, 2) + MathF.Pow(q2, 2))),
            X = MathF.Asin(2 * (q0 * q2 - q3 * q1)),
            Z = MathF.Atan2(2 * (q0 * q3 + q1 * q2), 1 - 2 * (MathF.Pow(q2, 2) + MathF.Pow(q3, 2)))
        };

        if (!radians)
        {
            result.X = MathHelper.ToDegrees(result.X);
            result.Y = MathHelper.ToDegrees(result.Y);
            result.Z = MathHelper.ToDegrees(result.Z);
        }

        return result;

        // https://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToEuler/
        float a = 2.0f * value.Y * value.W - 2.0f * value.X * value.Z;
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

        return new Vector3(x, y, z);
    }

    // https://www.euclideanspace.com/maths/geometry/rotations/conversions/quaternionToAngle/index.htm
    public static void ToAxisAngle(this Quaternion value, out Vector3 axis, out float angle)
    {
        angle = 2.0f * MathF.Acos(value.W);

        float x = value.X / MathF.Sqrt(1.0f - value.W * value.W);
        float y = value.Y / MathF.Sqrt(1.0f - value.W * value.W);
        float z = value.Z / MathF.Sqrt(1.0f - value.W * value.W);

        axis = new Vector3(x, y, z);
    }
}
