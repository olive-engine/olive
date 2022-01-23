using System.Collections;

namespace Olive.Components;

public abstract class Behavior : Component
{
    /// <summary>
    ///     Called when the behavior has been awoken.
    /// </summary>
    protected internal virtual void Awake()
    {
    }

    /// <summary>
    ///     Called before the first frame.
    /// </summary>
    protected internal virtual void Start()
    {
    }

    /// <summary>
    ///     Called once per frame.
    /// </summary>
    protected internal virtual void Update()
    {
    }

    /// <summary>
    ///     Starts a coroutine.
    /// </summary>
    /// <param name="enumerator">The enumerator.</param>
    /// <returns>The new coroutine instance.</returns>
    protected Coroutine StartCoroutine(IEnumerator enumerator)
    {
        return GameObject.StartCoroutine(enumerator);
    }

    /// <summary>
    ///     Stops the specified coroutine.
    /// </summary>
    /// <param name="coroutine">The coroutine to stop.</param>
    protected void StopCoroutine(Coroutine coroutine)
    {
        GameObject.StopCoroutine(coroutine);
    }
}
