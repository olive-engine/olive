using MonoGame.Extended.Input;
using Olive;
using Olive.Math;

namespace BehaviorExample.FirstPersonMovement;

internal sealed class MouseLookBehavior : Behavior
{
    private readonly Vector2 _mouseSensitivity = new(0.5f, 20.0f);
    private readonly Transform _playerBody;
    private float _xRotation;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MouseLookBehavior" /> class.
    /// </summary>
    /// <param name="playerBody"></param>
    public MouseLookBehavior(Transform playerBody)
    {
        _playerBody = playerBody;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _playerBody.Rotation = Quaternion.Identity;
        Transform.Rotation = Quaternion.Identity;
    }

    protected override void Update(FrameContext context)
    {
        base.Update(context);

        MouseStateExtended mouseState = MouseExtended.GetState();
        if (!mouseState.PositionChanged)
        {
            return;
        }

        float mouseX = mouseState.DeltaX * context.DeltaTime * _mouseSensitivity.X;
        float mouseY = mouseState.DeltaY * context.DeltaTime * _mouseSensitivity.Y;

        _xRotation += mouseY;
        _xRotation = Math.Clamp(_xRotation, -90, 90);

        Transform.LocalEulerAngles = new Vector3(_xRotation, 0, 0);
        _playerBody.Rotate(Vector3.Up * mouseX);
    }
}
