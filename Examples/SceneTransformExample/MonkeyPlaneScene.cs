using Microsoft.Xna.Framework.Graphics;
using Olive;
using Olive.Math;
using Olive.Rendering;
using Olive.SceneManagement;

namespace SceneTransformExample;

internal sealed class MonkeyPlaneScene : Scene
{
    private float _time;

    public MonkeyPlaneScene() : base("Monkey Monkey Scene")
    {
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

    protected override void Update(FrameContext context)
    {
        base.Update(context);

        _time = (_time + context.DeltaTime) % (MathF.PI * 2);
        Transform.Position = Vector3.Up * MathF.Sin(_time);
    }
}
