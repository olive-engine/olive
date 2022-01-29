using Olive;
using Olive.Math;
using Olive.SceneManagement;

namespace SceneTransformExample;

internal sealed class CameraScene : Scene
{
    public CameraScene() : base("Camera Scene")
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        var camera = new GameObject(this).AddComponent<Camera>();
        camera.Transform.Position = Vector3.Backward * 2;
    }
}
