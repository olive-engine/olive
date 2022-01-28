﻿using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

public sealed class SimpleSceneManager : SceneManager
{
    /// <summary>
    ///     Gets the currently active scene.
    /// </summary>
    /// <value>The currently active scene.</value>
    public Scene CurrentScene { get; protected internal set; } = new EmptyScene();

    public override void LoadScene(Scene scene)
    {
        if (scene == CurrentScene)
            throw new InvalidOperationException("Scene is already loaded.");

        CurrentScene = scene;
        scene.Initialize();
    }

    /// <summary>
    ///     Unloads the current scene with the current scene manager's strategy.
    /// </summary>
    public void UnloadScene()
    {
        UnloadScene(CurrentScene);
    }

    public override void UnloadScene(Scene scene)
    {
        if (scene != CurrentScene)
            throw new ArgumentException("Specified scene is not loaded by this manager.", nameof(scene));

        CurrentScene = new EmptyScene();
    }

    protected internal override void Draw(GameTime gameTime)
    {
        if (!(CurrentScene?.Draw(gameTime) ?? false) && OliveEngine.CurrentGame?.GraphicsDevice is { } graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);
        }
    }

    protected internal override void Initialize()
    {
        base.Initialize();
        CurrentScene?.Initialize();
    }

    protected internal override void OnPostRender()
    {
        CurrentScene?.OnPostRender();
    }

    protected internal override void Update(GameTime gameTime)
    {
        CurrentScene?.Update(gameTime);
    }

    internal override void LoadContent()
    {
        CurrentScene?.LoadContent();
    }
}
