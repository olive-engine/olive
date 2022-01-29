using Microsoft.Xna.Framework.Graphics;
using Olive;
using Olive.Math;
using Olive.Rendering;
using Olive.SceneManagement;
using GameTime = Microsoft.Xna.Framework.GameTime;

namespace SceneTransformExample;

internal sealed class MonkeyPlaneScene : Scene
{
    private float _time;

    protected override void Initialize()
    {
        base.Initialize();

        // we don't want more than one camera. let the camera in CameraScene do the work
        MainCamera.GameObject.Dispose();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        var plane = new GameObject(this);
        plane.Transform.Position = Vector3.Down * 3;
        plane.AddComponent<ModelRenderer>().Model = LoadContent<Model>("Plane");

        var centerMonkey = new GameObject(this);
        centerMonkey.Transform.Scale = Vector3.One * 0.0005f;
        centerMonkey.AddComponent<ModelRenderer>().Model = LoadContent<Model>("monkey");
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _time = (float) (_time + gameTime.ElapsedGameTime.TotalSeconds) % (MathF.PI * 2);
        Transform.Position = Vector3.Up * MathF.Sin(_time);
    }
}
