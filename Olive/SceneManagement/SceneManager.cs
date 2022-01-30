namespace Olive.SceneManagement;

/// <summary>
///     Represents a class which provides methods and properties related to scene management.
/// </summary>
public static class SceneManager
{
    private static readonly List<Scene> InternalLoadedScenes = new();

    /// <summary>
    ///     Gets a read-only view of the loaded scenes.
    /// </summary>
    /// <value>A read-only view of the loaded scenes.</value>
    public static IReadOnlyList<Scene> LoadedScenes => InternalLoadedScenes.AsReadOnly();

    /// <summary>
    ///     Returns a value indicating whether the specified scene has been loaded.
    /// </summary>
    /// <param name="scene">The scene to check.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="scene" /> is currently loaded; otherwise, <see langword="false" />.
    /// </returns>
    public static bool IsSceneLoaded(Scene scene)
    {
        return InternalLoadedScenes.Contains(scene);
    }

    /// <summary>
    ///     Loads the specified scene.
    /// </summary>
    /// <param name="scene">The scene to load.</param>
    /// <param name="sceneLoadMode">The scene load mode. Defaults to <see cref="SceneLoadMode.Single" />.</param>
    public static void LoadScene(Scene scene, SceneLoadMode sceneLoadMode = SceneLoadMode.Single)
    {
        if (InternalLoadedScenes.Contains(scene))
        {
            throw new InvalidOperationException($"Cannot load scene '{scene.Name}': Scene already loaded");
        }

        if (sceneLoadMode == SceneLoadMode.Single)
        {
            for (int index = InternalLoadedScenes.Count - 1; index >= 0; index--) // enumerator alloc is wasteful
            {
                UnloadScene(InternalLoadedScenes[index]);
            }
        }

        InternalLoadedScenes.Add(scene);

        if (!scene.IsInitialized)
        {
            scene.Initialize();
        }
    }

    /// <summary>
    ///     Unloads the scene which was loaded last, essentially "popping" it from the scene stack.
    /// </summary>
    public static void PopScene()
    {
        UnloadScene(InternalLoadedScenes[^1]);
    }

    /// <summary>
    ///     Unloads the specified scene.
    /// </summary>
    /// <param name="scene">The scene to load.</param>
    public static void UnloadScene(Scene scene)
    {
        if (!IsSceneLoaded(scene))
        {
            throw new InvalidOperationException($"Cannot unload scene '{scene.Name}': Scene not loaded");
        }

        InternalLoadedScenes.Remove(scene);
    }
}
