namespace Olive.SceneManagement;

internal sealed class EmptyScene : Scene
{
    internal EmptyScene() : base("Empty Scene")
    {
    }

    internal override void AddGameObject(GameObject gameObject)
    {
        throw new InvalidOperationException("Cannot add a game object to the empty scene.");
    }
}
