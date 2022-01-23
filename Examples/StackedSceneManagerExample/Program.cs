using Microsoft.Xna.Framework;
using Olive;
using Olive.Components;
using Olive.SceneManagement;

OliveEngine.Initialize("Hello, Olive!", 800, 600, false);

var sceneManager = new StackedSceneManager();
OliveEngine.SceneManager = sceneManager;

var red = new RedScene();
var blue = new BlueScene();

sceneManager.Push(red);

while (true)
{
    await Task.Delay(1000);
    sceneManager.Push(blue);
    
    await Task.Delay(1000);
    sceneManager.Pop();
}

internal class RedScene : Scene
{
    private Camera _mainCamera;

    protected override void Initialize()
    {
        base.Initialize();

        _mainCamera = new GameObject(this).AddComponent<Camera>();
        _mainCamera.ClearColor = Color.Red;
    }
}

internal class BlueScene : Scene
{
    private Camera _mainCamera;

    protected override void Initialize()
    {
        base.Initialize();

        _mainCamera = new GameObject(this).AddComponent<Camera>();
        _mainCamera.ClearColor = new Color(0, 0, 255, 128);
    }
}
