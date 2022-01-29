using Microsoft.Xna.Framework.Input;
using Olive;
using Olive.Math;
using GameTime = Microsoft.Xna.Framework.GameTime;

namespace BehaviorExample.FirstPersonMovement;

internal sealed class FirstPersonMovementBehavior : Behavior
{
    private const float MovementSpeed = 1.0f;

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Vector3 forward = Transform.Forward;
        Vector3 right = Transform.Right;
        Vector3 movement = Vector3.Zero;

        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.W)) movement += forward;
        else if (keyboardState.IsKeyDown(Keys.S)) movement -= forward;

        if (keyboardState.IsKeyDown(Keys.D)) movement += right;
        else if (keyboardState.IsKeyDown(Keys.A)) movement -= right;

        if (keyboardState.IsKeyDown(Keys.E)) movement += Vector3.Up;
        else if (keyboardState.IsKeyDown(Keys.Q)) movement -= Vector3.Up;

        var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        Transform.Translate(movement * (deltaTime * MovementSpeed));
    }
}
