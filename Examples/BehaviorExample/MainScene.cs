using BehaviorExample.FirstPersonMovement;
using Microsoft.Xna.Framework.Graphics;
using Olive;
using Olive.Math;
using Olive.Rendering;
using Olive.SceneManagement;

namespace BehaviorExample;

internal sealed class MainScene : Scene
{
    private GameObject _monkey = null!;
    private Transform _playerBody = null!;

    protected override void Initialize()
    {
        base.Initialize();

        _monkey = new GameObject(this);
        _monkey.AddComponent<ModelRenderer>();
        _monkey.AddComponent<FloatingBehavior>();

        _playerBody = new GameObject(this).Transform;
        _playerBody.Position = Vector3.Backward;

        MainCamera.AddComponent<FirstPersonMovementBehavior>();
        MainCamera.AddComponent(() => new MouseLookBehavior(_playerBody)); // AddComponent supports DI!
        MainCamera.Transform.Parent = _playerBody;
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        if (_monkey.TryGetComponent(out ModelRenderer? renderer))
        {
            renderer.Model = LoadContent<Model>("monkey");
        }
    }
}
