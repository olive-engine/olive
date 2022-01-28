using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

/// <summary>
///     Represents an additive scene manager, which is capable of managing multiple scenes at once.
/// </summary>
public sealed class AdditiveSceneManager : SceneManager
{
    private readonly List<Scene> _scenes = new();

    public override void LoadScene(Scene scene)
    {
        base.LoadScene(scene);

        if (_scenes.Contains(scene))
        {
            throw new InvalidOperationException("Scene is already loaded.");
        }

        _scenes.Add(scene);
        scene.Initialize();
    }

    public override void UnloadScene(Scene scene)
    {
        if (!_scenes.Contains(scene))
        {
            throw new InvalidOperationException("Specified scene is not loaded by this manager.");
        }

        _scenes.Remove(scene);
    }

    protected internal override void Draw(GameTime gameTime)
    {
        var hasDrawn = false;
        foreach (Scene scene in _scenes.ToArray())
        {
            if (scene.Draw(gameTime))
            {
                hasDrawn = true;
            }
        }

        if (!hasDrawn && OliveEngine.CurrentGame?.GraphicsDevice is { } graphicsDevice)
        {
            graphicsDevice.Clear(Color.Black);
        }
    }

    protected internal override void Initialize()
    {
        base.Initialize();

        foreach (Scene scene in _scenes.ToArray())
        {
            if (!scene.IsInitialized)
            {
                scene.Initialize();
            }
        }
    }

    protected internal override void Update(GameTime gameTime)
    {
        foreach (Scene scene in _scenes.ToArray())
        {
            scene.Update(gameTime);
        }
    }

    protected internal override void OnPostRender()
    {
        foreach (Scene scene in _scenes.ToArray())
        {
            scene.OnPostRender();
        }
    }

    internal override void LoadContent()
    {
        foreach (Scene scene in _scenes)
        {
            scene.LoadContent();
        }
    }
}
