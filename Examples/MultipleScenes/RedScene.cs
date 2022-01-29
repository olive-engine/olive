using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Olive.SceneManagement;
using Color = Microsoft.Xna.Framework.Color;
using GameTime = Microsoft.Xna.Framework.GameTime;

namespace MultipleScenes;

internal sealed class RedScene : Scene
{
    protected override void Initialize()
    {
        base.Initialize();

        MainCamera.ClearColor = Color.Red;
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        KeyboardStateExtended keyboardState = KeyboardExtended.GetState();
        if (keyboardState.WasKeyJustDown(Keys.Space))
        {
            SceneManager.LoadScene(new BlueScene());
            SceneManager.UnloadScene(this);
        }
    }
}
