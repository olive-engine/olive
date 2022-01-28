namespace Olive.SceneManagement;

/// <summary>
///     Represents an empty scene with no game objects.
/// </summary>
internal sealed class EmptyScene : Scene
{
    internal override void AddGameObject(GameObject gameObject)
    {
        throw new InvalidOperationException("Cannot add a game object to the empty scene.");
    }
}
