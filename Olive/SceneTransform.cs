using Microsoft.Xna.Framework;
using Olive.Extensions;
using Olive.SceneManagement;

namespace Olive;

/// <summary>
///     Represents a class which provides transform data about a <see cref="Scene" />.
/// </summary>
public sealed class SceneTransform
{
    /// <summary>
    ///     Gets the rotation of the scene, as represented by Euler angles.
    /// </summary>
    /// <value>The rotation of the scene, as represented by Euler angles.</value>
    public Vector3 EulerAngles
    {
        get => Rotation.ToEulerAngles();
        set => Rotation = value.ToQuaternion();
    }

    /// <summary>
    ///     Gets or sets the position of the scene.
    /// </summary>
    /// <value>The position of the scene.</value>
    public Vector3 Position { get; set; } = Vector3.Zero;

    /// <summary>
    ///     Gets the rotation of the scene.
    /// </summary>
    /// <value>The rotation of the scene.</value>
    public Quaternion Rotation { get; set; } = Quaternion.Identity;

    /// <summary>
    ///     Gets or sets the scale of this scene.
    /// </summary>
    /// <value>The scale of the scene.</value>
    public Vector3 Scale { get; set; } = Vector3.One;

    /// <summary>
    ///     Rotates the transform so that the forward vector points towards the position of another transform.
    /// </summary>
    /// <param name="target">The transform to look at.</param>
    public void LookAt(Transform target)
    {
        LookAt(target.Position);
    }

    /// <summary>
    ///     Rotates the transform so that the forward vector points towards a specified position.
    /// </summary>
    /// <param name="position">The position to look at.</param>
    public void LookAt(Vector3 position)
    {
        LookAt(position, Vector3.Up);
    }

    /// <summary>
    ///     Rotates the transform so that the forward vector points towards a specified position.
    /// </summary>
    /// <param name="position">The position to look at.</param>
    /// <param name="up">The world up vector.</param>
    public void LookAt(Vector3 position, Vector3 up)
    {
        // https://www.scratchapixel.com/lessons/mathematics-physics-for-computer-graphics/lookat-function

        Vector3 from = Position;
        Vector3 forward = (from - position).Normalized();
        Vector3 right = Vector3.Cross(up, forward);
        up = Vector3.Cross(forward, right);

        var camToWorld = new Matrix
        {
            [0, 0] = right.X,
            [0, 1] = right.Y,
            [0, 2] = right.Z,
            [1, 0] = up.X,
            [1, 1] = up.Y,
            [1, 2] = up.Z,
            [2, 0] = forward.X,
            [2, 1] = forward.Y,
            [2, 2] = forward.Z,
            [3, 0] = from.X,
            [3, 1] = from.Y,
            [3, 2] = from.Z
        };

        Rotation = Quaternion.CreateFromRotationMatrix(camToWorld);
    }

    /// <summary>
    ///     Translates the transform by the specified delta.
    /// </summary>
    /// <param name="translation">The world-space translation.</param>
    public void Translate(Vector3 translation)
    {
        Position += translation;
    }

    /// <summary>
    ///     Rotates the transform by the specified delta.
    /// </summary>
    /// <param name="rotation">The rotation.</param>
    public void Rotate(Quaternion rotation)
    {
        Rotation *= rotation;
    }

    /// <summary>
    ///     Rotates the transform by the specified delta.
    /// </summary>
    /// <param name="rotation">The world-space rotation.</param>
    public void Rotate(Vector3 rotation)
    {
        Rotation *= rotation.ToQuaternion();
    }

    /// <summary>
    ///     Rotates the transform about a specified axis.
    /// </summary>
    /// <param name="axis">The axis of rotation.</param>
    /// <param name="angle">The angle in degrees.</param>
    public void Rotate(Vector3 axis, float angle)
    {
        angle = MathHelper.ToRadians(angle);
        Rotation *= Quaternion.CreateFromAxisAngle(axis, angle);
    }

    /// <summary>
    ///     Rotates the transform about a specified axis through a specified point.
    /// </summary>
    /// <param name="point">The world-space point around which to rotate.</param>
    /// <param name="axis">The axis of rotation.</param>
    /// <param name="angle">The angle in degrees.</param>
    public void RotateAround(Vector3 point, Vector3 axis, float angle)
    {
        var rotation = Quaternion.CreateFromAxisAngle(axis, angle);
        Vector3 position = Position;
        Vector3 offset = position - point;
        offset = rotation.Multiply(offset);
        position = point + offset;
        Position = position;
    }

    internal Matrix GetWorldMatrix(Matrix worldMatrix)
    {
        return Matrix.CreateTranslation(Position)
               * Matrix.CreateFromQuaternion(Rotation)
               * Matrix.CreateScale(Scale)
               * worldMatrix;
    }
}
