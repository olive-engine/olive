using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Olive;
using Olive.SceneManagement;

namespace MultipleScenes;

internal sealed class RedScene : Scene
{
    public RedScene() : base("Red Scene")
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        var camera = new GameObject(this);
        camera.AddComponent<Camera>().ClearColor = Color.Red;
    }

    protected override void Update(FrameContext context)
    {
        base.Update(context);

        KeyboardStateExtended keyboardState = KeyboardExtended.GetState();
        if (keyboardState.WasKeyJustDown(Keys.Space))
        {
            SceneManager.LoadScene(new BlueScene());
            SceneManager.UnloadScene(this);
        }
    }
}
