using MonoGame.Extended.Input;
using Olive;
using Olive.Math;
using GameTime = Microsoft.Xna.Framework.GameTime;

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

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        MouseStateExtended mouseState = MouseExtended.GetState();
        if (!mouseState.PositionChanged)
        {
            return;
        }

        var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        float mouseX = mouseState.DeltaX * deltaTime * _mouseSensitivity.X;
        float mouseY = mouseState.DeltaY * deltaTime * _mouseSensitivity.Y;

        _xRotation += mouseY;
        _xRotation = Math.Clamp(_xRotation, -90, 90);

        Transform.LocalEulerAngles = new Vector3(_xRotation, 0, 0);
        _playerBody.Rotate(Vector3.Up * mouseX);
    }
}
