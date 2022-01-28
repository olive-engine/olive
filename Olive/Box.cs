using Microsoft.Xna.Framework;
using Olive.Extensions;

namespace Olive;

/// <summary>
///     Represents a box. Similar to <see cref="BoundingBox" /> except that this struct is primarily used for
///     <see cref="GL.DrawBox(Box, Color)" />.
/// </summary>
public readonly struct Box
{
    public Box(Vector3 origin, Vector3 halfExtents)
    {
        (float x, float y, float z) = halfExtents;
        LocalFrontTopLeft = new Vector3(-x, y, -z);
        LocalFrontTopRight = new Vector3(x, y, -z);
        LocalFrontBottomLeft = new Vector3(-x, -y, -z);
        LocalFrontBottomRight = new Vector3(x, -y, -z);

        Origin = origin;
    }

    public Box(Vector3 origin, Vector3 halfExtents, Quaternion orientation) : this(origin, halfExtents)
    {
        (float x, float y, float z) = halfExtents;

        var localFrontTopLeft = new Vector3(-x, y, -z);
        var localFrontTopRight = new Vector3(-x, y, -z);
        var localFrontBottomLeft = new Vector3(-x, y, -z);
        var localFrontBottomRight = new Vector3(-x, y, -z);

        Rotate(orientation, ref localFrontTopLeft, ref localFrontTopRight, ref localFrontBottomLeft, ref localFrontBottomRight);
        LocalFrontTopLeft = localFrontTopLeft;
    }

    public Vector3 Origin { get; }
    public Vector3 LocalFrontTopLeft { get; }
    public Vector3 LocalFrontTopRight { get; }
    public Vector3 LocalFrontBottomLeft { get; }
    public Vector3 LocalFrontBottomRight { get; }
    public Vector3 LocalBackTopLeft => -LocalFrontBottomRight;
    public Vector3 LocalBackTopRight => -LocalFrontBottomLeft;
    public Vector3 LocalBackBottomLeft => -LocalFrontTopRight;
    public Vector3 LocalBackBottomRight => -LocalFrontTopLeft;

    public Vector3 FrontTopLeft => LocalFrontTopLeft + Origin;
    public Vector3 FrontTopRight => LocalFrontTopRight + Origin;
    public Vector3 FrontBottomLeft => LocalFrontBottomLeft + Origin;
    public Vector3 FrontBottomRight => LocalFrontBottomRight + Origin;
    public Vector3 BackTopLeft => LocalBackTopLeft + Origin;
    public Vector3 BackTopRight => LocalBackTopRight + Origin;
    public Vector3 BackBottomLeft => LocalBackBottomLeft + Origin;
    public Vector3 BackBottomRight => LocalBackBottomRight + Origin;

    public static Vector3 CastCenterOnCollision(Vector3 origin, Vector3 direction, float hitInfoDistance)
    {
        direction.Normalize();
        return origin + direction * hitInfoDistance;
    }

    private static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        Vector3 direction = point - pivot;
        return pivot + rotation.Multiply(direction);
    }

    private static void Rotate(Quaternion orientation, ref Vector3 localFrontTopLeft, ref Vector3 localFrontTopRight,
        ref Vector3 localFrontBottomLeft, ref Vector3 localFrontBottomRight)
    {
        localFrontTopLeft = RotatePointAroundPivot(localFrontTopLeft, Vector3.Zero, orientation);
        localFrontTopRight = RotatePointAroundPivot(localFrontTopRight, Vector3.Zero, orientation);
        localFrontBottomLeft = RotatePointAroundPivot(localFrontBottomLeft, Vector3.Zero, orientation);
        localFrontBottomRight = RotatePointAroundPivot(localFrontBottomRight, Vector3.Zero, orientation);
    }
}
