﻿using Microsoft.Xna.Framework.Graphics;
using Olive;
using Olive.Math;
using Olive.Rendering;
using Olive.SceneManagement;

namespace ModelLoading;

internal sealed class MainScene : Scene
{
    private GameObject? _monkey;

    public MainScene() : base("Main Scene")
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        (_monkey = new GameObject(this)).AddComponent<ModelRenderer>();
        new GameObject(this).AddComponent<Camera>().Transform.Position = Vector3.Backward;
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        var model = LoadContent<Model>("monkey");
        if (_monkey?.TryGetComponent(out ModelRenderer? renderer) ?? false)
        {
            renderer.Model = model;
        }
    }
}
