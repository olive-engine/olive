using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Olive;

public static class GL
{
    private static GraphicsDevice? s_graphicsDevice;

    public static void DrawBox(Box box, Color color)
    {
        DrawLine(box.FrontTopLeft, box.FrontTopRight, color);
        DrawLine(box.FrontTopRight, box.FrontBottomRight, color);
        DrawLine(box.FrontBottomRight, box.FrontBottomLeft, color);
        DrawLine(box.FrontBottomLeft, box.FrontTopLeft, color);

        DrawLine(box.BackTopLeft, box.BackTopRight, color);
        DrawLine(box.BackTopRight, box.BackBottomRight, color);
        DrawLine(box.BackBottomRight, box.BackBottomLeft, color);
        DrawLine(box.BackBottomLeft, box.BackTopLeft, color);

        DrawLine(box.FrontTopLeft, box.BackTopLeft, color);
        DrawLine(box.FrontTopRight, box.BackTopRight, color);
        DrawLine(box.FrontBottomRight, box.BackBottomRight, color);
        DrawLine(box.FrontBottomLeft, box.BackBottomLeft, color);
    }

    public static void DrawBox(Vector3 origin, Vector3 halfExtents, Quaternion orientation, Color color)
    {
        DrawBox(new Box(origin, halfExtents, orientation), color);
    }

    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        if (Camera.Main is not { } camera || camera.IsDisposed)
        {
            return;
        }

        if (s_graphicsDevice is null)
        {
            return;
        }

        var basicEffect = new BasicEffect(s_graphicsDevice);
        basicEffect.World = camera.GetWorldMatrix();
        basicEffect.View = camera.GetViewMatrix();
        basicEffect.Projection = camera.GetProjectionMatrix(s_graphicsDevice);
        basicEffect.VertexColorEnabled = true;

        foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
        {
            pass.Apply();
        }

        var vertices = new[] {new VertexPositionColor(start, color), new VertexPositionColor(end, color)};
        s_graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
    }

    internal static void Initialize(GraphicsDevice graphicsDevice)
    {
        s_graphicsDevice = graphicsDevice;
    }
}
