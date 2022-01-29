using Olive;
using Olive.SceneManagement;
using SceneTransformExample;

OliveEngine.Initialize("Scene Transform Example", 800, 600, DisplayMode.Windowed);
var sceneManager = new AdditiveSceneManager();
OliveEngine.SceneManager = sceneManager;

var monkeyPlaneScene = new MonkeyPlaneScene();
var outerMonkeyScene = new OuterMonkeyScene(monkeyPlaneScene);
var cameraScene = new CameraScene();

sceneManager.LoadScene(cameraScene);
sceneManager.LoadScene(monkeyPlaneScene);
sceneManager.LoadScene(outerMonkeyScene);

OliveEngine.Run();
