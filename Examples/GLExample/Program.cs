﻿using Olive;
using Olive.Math;
using Olive.SceneManagement;

OliveEngine.Initialize("GL Example", 800, 600, DisplayMode.Windowed);
OliveEngine.Run();

OliveEngine.SceneManager.LoadScene(new MainScene());

internal sealed class MainScene : Scene
{
    private Transform _cameraTransform = null!;

    protected override void Initialize()
    {
        base.Initialize();

        _cameraTransform = MainCamera.Transform;

        _cameraTransform.Position = Vector3.Backward;
    }

    protected override void Update(FrameContext context)
    {
        base.Update(context);

        _cameraTransform.RotateAround(Vector3.Zero, Vector3.Up, context.DeltaTime);
        _cameraTransform.LookAt(Vector3.Zero);
    }

    protected override void OnPostRender()
    {
        base.OnPostRender();

        GL.DrawLine(Vector3.Zero, Vector3.UnitX, Color.Red);
        GL.DrawLine(Vector3.Zero, Vector3.UnitY, Color.Green);
        GL.DrawLine(Vector3.Zero, Vector3.UnitZ, Color.Blue);

        GL.DrawBox(Vector3.Zero, Vector3.One * 0.5f, Quaternion.Identity, Color.Yellow);
    }
}
