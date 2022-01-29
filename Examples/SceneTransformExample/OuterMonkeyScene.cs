using Microsoft.Xna.Framework.Graphics;
using Olive;
using Olive.Math;
using Olive.Rendering;
using Olive.SceneManagement;
using GameTime = Microsoft.Xna.Framework.GameTime;

namespace SceneTransformExample;

internal sealed class OuterMonkeyScene : Scene
{
    protected override void Initialize()
    {
        base.Initialize();

        // we don't want more than one camera. let the camera in CameraScene do the work
        MainCamera.GameObject.Dispose();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        var model = LoadContent<Model>("monkey");
        for (float x = -1; x < 2; x += 2)
        {
            var monkey = new GameObject(this);
            monkey.AddComponent<ModelRenderer>().Model = model;
            monkey.Transform.Position = Vector3.Right * x;
            monkey.Transform.Scale = Vector3.One * 0.0005f;
        }
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Transform.Rotate(Vector3.Up, 45 * (float) gameTime.ElapsedGameTime.TotalSeconds);
    }
}
