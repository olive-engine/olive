using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector3 = Olive.Math.Vector3;

namespace Olive.Rendering;

/// <summary>
///     Represents a renderer which is responsible for rendering a <see cref="Microsoft.Xna.Framework.Graphics.Model" />.
/// </summary>
public sealed class ModelRenderer : Renderer
{
    private Model? _model = null;

    /// <summary>
    ///     Gets or sets the model to render.
    /// </summary>
    public Model? Model
    {
        get
        {
            OliveEngine.AssertNonDisposed(this);
            return _model;
        }
        set
        {
            OliveEngine.AssertNonDisposed(this);
            _model = value;
        }
    }

    /// <summary>
    ///     Gets the bounding box of this model.
    /// </summary>
    /// <returns>The bounding box of this model.</returns>
    public BoundingBox GetBounds()
    {
        var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        if (Model == null) return new BoundingBox(min, max);

        foreach (ModelMesh mesh in Model.Meshes)
        {
            foreach (ModelMeshPart meshPart in mesh.MeshParts)
            {
                int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                int vertexBufferSize = meshPart.NumVertices * vertexStride;

                int vertexDataSize = vertexBufferSize / sizeof(float);
                var vertexData = new float[vertexDataSize];
                meshPart.VertexBuffer.GetData(vertexData);

                for (var i = 0; i < vertexDataSize; i += vertexStride / sizeof(float))
                {
                    var vertex = new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]);
                    min = Vector3.Min(min, vertex);
                    max = Vector3.Max(max, vertex);
                }
            }
        }

        return new BoundingBox(min, max);
    }

    protected internal override void Render(GameTime gameTime, Matrix world, Matrix view, Matrix projection)
    {
        world = Transform.GetWorldMatrix(world);
        base.Render(gameTime, world, view, projection);

        if (Model is null) return;

        foreach (ModelMesh mesh in Model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects.OfType<BasicEffect>())
            {
                effect.EnableDefaultLighting();
                effect.TextureEnabled = true;
                effect.World = world;
                effect.View = view;
                effect.Projection = projection;

                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                }
            }

            mesh.Draw();
        }
    }
}
