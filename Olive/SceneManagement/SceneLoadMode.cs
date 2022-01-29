namespace Olive.SceneManagement;

/// <summary>
///     An enumeration of scene load modes.
/// </summary>
public enum SceneLoadMode
{
    /// <summary>
    ///     Indicates that the scene should be loaded as a single scene, unloading all other scenes.
    /// </summary>
    Single,
    
    /// <summary>
    ///     Indicates that the scene should be loaded additively, without unloading other scenes. 
    /// </summary>
    Additive
}
