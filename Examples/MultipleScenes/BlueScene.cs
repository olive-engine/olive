using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Olive.SceneManagement;

namespace MultipleScenes;

internal sealed class BlueScene : Scene
{
    protected override void Initialize()
    {
        base.Initialize();

        MainCamera.ClearColor = Color.Blue;
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        KeyboardStateExtended keyboardState = KeyboardExtended.GetState();
        if (keyboardState.WasKeyJustDown(Keys.Space))
        {
            SceneManager.LoadScene(new RedScene());
            SceneManager.UnloadScene(this);
        }
    }
}
