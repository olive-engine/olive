using System.Collections;
using Microsoft.Xna.Framework;

namespace Olive;

public abstract class Behavior : Component
{
    /// <summary>
    ///     Called when the behavior has been awoken.
    /// </summary>
    protected internal virtual void Initialize()
    {
    }

    /// <summary>
    ///     Called once per frame.
    /// </summary>
    protected internal virtual void Update(GameTime gameTime)
    {
    }

    /// <summary>
    ///     Starts a coroutine.
    /// </summary>
    /// <param name="enumerator">The enumerator.</param>
    /// <returns>The new coroutine instance.</returns>
    protected Coroutine StartCoroutine(IEnumerator enumerator)
    {
        OliveEngine.AssertNonDisposed(this);
        return GameObject.StartCoroutine(enumerator);
    }

    /// <summary>
    ///     Stops the specified coroutine.
    /// </summary>
    /// <param name="coroutine">The coroutine to stop.</param>
    protected void StopCoroutine(Coroutine coroutine)
    {
        OliveEngine.AssertNonDisposed(this);
        GameObject.StopCoroutine(coroutine);
    }
}
