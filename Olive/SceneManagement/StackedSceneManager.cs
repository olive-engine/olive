using Microsoft.Xna.Framework;

namespace Olive.SceneManagement;

/// <summary>
///     Represents a stacked scene manager, which is capable of pushing and popping scenes.
/// </summary>
public sealed class StackedSceneManager : SceneManager
{
    private readonly Stack<Scene> _scenes = new(new[] {new EmptyScene()});

    /// <summary>
    ///     Gets the topmost scene of the stack.
    /// </summary>
    /// <value>The topmost scene.</value>
    public override Scene? PrimaryScene
    {
        get => _scenes.Count == 0 ? null : _scenes.Peek();
        protected internal set =>
            throw new InvalidOperationException($"Cannot set primary scene. Maybe you meant to use the {nameof(Push)} method?");
    }

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

    internal override void Draw(GameTime gameTime)
    {
        foreach (Scene scene in _scenes)
        {
            scene.Draw(gameTime);
        }
    }
}
