using Olive;
using Olive.Math;

namespace BehaviorExample;

internal sealed class FloatingBehavior : Behavior
{
    private const float Frequency = 1.0f;
    private const float Amplitude = 1.0f;
    private float _time;
    private Vector3 _initialPos;

    protected override void Initialize()
    {
        base.Initialize();

        _initialPos = Transform.Position;
    }

    protected override void Update(FrameContext context)
    {
        base.Update(context);

        _time = (_time + context.DeltaTime * Frequency) % (MathF.PI * 2);
        Transform.Position = _initialPos + Vector3.Up * (MathF.Sin(_time) * Amplitude);
    }
}
