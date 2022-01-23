﻿using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

/// <summary>
///     Represents a stacked scene manager, which is capable of pushing and popping scenes.
/// </summary>
public sealed class StackedSceneManager : SceneManager
{
    private readonly Stack<Scene> _scenes = new(new[] {new EmptyScene()});

    /// <summary>
    ///     Pushes a new scene onto the stack.
    /// </summary>
    /// <returns>The popped scene, or <see langword="null" /> if there are no more loaded scenes.</returns>
    public Scene? Pop()
    {
        if (_scenes.Count == 0) return null;
        return _scenes.Pop();
    }

    /// <summary>
    ///     Pushes a new scene onto the stack.
    /// </summary>
    /// <param name="scene">The scene to push.</param>
    public void Push(Scene scene)
    {
        scene.Initialize();
        _scenes.Push(scene);
    }

    protected internal override void Draw(GameTime gameTime)
    {
        foreach (Scene scene in _scenes)
        {
            scene.Draw(gameTime);
        }
    }

    protected internal override void Initialize()
    {
        foreach (Scene scene in _scenes)
        {
            scene.Initialize();
        }
    }

    protected internal override void Update(GameTime gameTime)
    {
        foreach (Scene scene in _scenes)
        {
            scene.Update(gameTime);
        }
    }
}
