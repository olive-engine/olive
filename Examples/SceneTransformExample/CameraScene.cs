using Microsoft.Xna.Framework;
using Olive;
using Olive.SceneManagement;

namespace SceneTransformExample;

internal sealed class CameraScene : Scene
{
    protected override void Initialize()
    {
        base.Initialize();

        MainCamera.Transform.Position = Vector3.Backward * 2;
        Camera.Main = MainCamera;
    }
}
