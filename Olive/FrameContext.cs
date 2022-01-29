namespace Olive;

/// <summary>
///     Represents a class which provides state information about a frame. 
/// </summary>
public sealed class FrameContext
{
    internal FrameContext()
    {
    }

    /// <summary>
    ///     Gets the number of seconds that have elapsed since the last frame.
    /// </summary>
    /// <value>The number of seconds that have elapsed since the last frame.</value>
    public float DeltaTime { get; internal init; }

    /// <summary>
    ///     Gets the number of seconds that have elapsed since the game began.
    /// </summary>
    /// <value>The number of seconds that have elapsed since the game began.</value>
    public float TotalTime { get; internal init; }
}
