using Microsoft.Xna.Framework;
using Olive.Extensions;

namespace Olive;

/// <summary>
///     Represents a component which is attached to all gameobjects by default, providing them with transform data.
/// </summary>
public class Transform : Component
{
    private Transform? _parent;
    private Vector3 _localPosition = Vector3.Zero;
    private Quaternion _localRotation = Quaternion.Identity;
    private Vector3 _localScale = Vector3.One;

    /// <summary>
    ///     Gets the world-space rotation of this object, as represented by Euler angles.
    /// </summary>
    /// <value>The world-space rotation.</value>
    public Vector3 EulerAngles
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return Rotation.ToEulerAngles();
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            Rotation = value.ToQuaternion();
        }
    }

    /// <summary>
    ///     Gets the forward facing direction of this transform.
    /// </summary>
    /// <value>The forward facing direction.</value>
    public Vector3 Forward
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return Rotation.Multiply(Vector3.Forward);
        }
    }

    /// <summary>
    ///     Gets or sets the parent of this transform.
    /// </summary>
    /// <value>The parent transform.</value>
    public Transform? Parent
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _parent;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _parent = value;
        }
    }

    /// <summary>
    ///     Gets the local-space rotation of this object, as represented by Euler angles.
    /// </summary>
    /// <value>The local-space rotation.</value>
    public Vector3 LocalEulerAngles
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return LocalRotation.ToEulerAngles();
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            LocalRotation = value.ToQuaternion();
        }
    }

    /// <summary>
    ///     Gets or sets the local-space position of this transform.
    /// </summary>
    /// <value>The local-space position.</value>
    public Vector3 LocalPosition
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _localPosition;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _localPosition = value;
        }
    }

    /// <summary>
    ///     Gets or sets the local-space rotation of this transform.
    /// </summary>
    /// <value>The local-space rotation.</value>
    public Quaternion LocalRotation
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _localRotation;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _localRotation = value;
        }
    }

    /// <summary>
    ///     Gets or sets the local-space scale of this transform.
    /// </summary>
    /// <value>The local-space scale.</value>
    public Vector3 LocalScale
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _localScale;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _localScale = value;
        }
    }

    /// <summary>
    ///     Gets the world-space position of this transform.
    /// </summary>
    /// <value>The world-space position.</value>
    public Vector3 Position
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);

            if (Parent is null) return LocalPosition;
            return Parent.Position + LocalPosition;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);

            if (Parent is null)
            {
                LocalPosition = value;
                return;
            }

            LocalPosition = value - Parent.Position;
        }
    }

    /// <summary>
    ///     Gets the right facing direction of this transform.
    /// </summary>
    /// <value>The right facing direction.</value>
    public Vector3 Right
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return Rotation.Multiply(Vector3.Right);
        }
    }

    /// <summary>
    ///     Gets the world-space position of this transform.
    /// </summary>
    /// <value>The world-space position.</value>
    public Quaternion Rotation
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);

            if (Parent is null) return LocalRotation;
            return Parent.Rotation * LocalRotation;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);

            if (Parent is null)
            {
                LocalRotation = value;
                return;
            }

            LocalRotation = value / Parent.Rotation;
        }
    }

    /// <summary>
    ///     Gets the world-space scale of this transform.
    /// </summary>
    /// <value>The world-space scale.</value>
    public Vector3 Scale
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);

            if (Parent is null) return LocalScale;
            return Parent.Scale * LocalScale;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);

            if (Parent is null)
            {
                LocalScale = value;
                return;
            }

            LocalScale = value / Parent.Scale;
        }
    }

    /// <summary>
    ///     Gets the up facing direction of this transform.
    /// </summary>
    /// <value>The up facing direction.</value>
    public Vector3 Up
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return Rotation.Multiply(Vector3.Up);
        }
    }

    public override object Clone()
    {
        OliveEngine.AssertNonDisposed(this);

        return new Transform
        {
            Parent = Parent,
            Position = Position,
            Rotation = Rotation,
            Scale = Scale
        };
    }

    /// <summary>
    ///     Translates the transform by the specified delta.
    /// </summary>
    /// <param name="translation">The world-space translation.</param>
    public void Translate(Vector3 translation)
    {
        OliveEngine.AssertNonDisposed(this);
        Position += translation;
    }

    /// <summary>
    ///     Rotates the transform by the specified delta.
    /// </summary>
    /// <param name="rotation">The rotation.</param>
    public void Rotate(Quaternion rotation)
    {
        OliveEngine.AssertNonDisposed(this);
        Rotation *= rotation;
    }

    /// <summary>
    ///     Rotates the transform by the specified delta.
    /// </summary>
    /// <param name="rotation">The world-space rotation.</param>
    public void Rotate(Vector3 rotation)
    {
        OliveEngine.AssertNonDisposed(this);
        (float pitch, float yaw, float roll) = rotation;
        Rotation *= Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
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
        OliveEngine.AssertNonDisposed(this);
        return Matrix.CreateFromQuaternion(Rotation)
               * Matrix.CreateScale(Scale)
               * Matrix.CreateTranslation(Position)
               * worldMatrix;
    }
}
