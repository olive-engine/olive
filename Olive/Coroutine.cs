using System.Collections;

namespace Olive;

/// <summary>
///     Represents a coroutine.
/// </summary>
public sealed class Coroutine
{
    internal Coroutine(IEnumerator enumerator)
    {
        CallStack = new Stack<IEnumerator>(new[] {enumerator});
    }

    internal Stack<IEnumerator> CallStack { get; }
}
