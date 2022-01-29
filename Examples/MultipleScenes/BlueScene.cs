using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Olive;
using Olive.SceneManagement;

namespace MultipleScenes;

internal sealed class BlueScene : Scene
{
    public BlueScene() : base("Blue Scene")
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        var camera = new GameObject(this);
        camera.AddComponent<Camera>().ClearColor = Color.Blue;
    }

    protected override void Update(FrameContext context)
    {
        base.Update(context);

        KeyboardStateExtended keyboardState = KeyboardExtended.GetState();
        if (keyboardState.WasKeyJustDown(Keys.Space))
        {
            SceneManager.LoadScene(new RedScene());
            SceneManager.UnloadScene(this);
        }
    }
}
