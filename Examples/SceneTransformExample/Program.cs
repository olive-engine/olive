using Olive;
using Olive.SceneManagement;
using SceneTransformExample;

OliveEngine.Initialize("Scene Transform Example", 800, 600, DisplayMode.Windowed);
var sceneManager = new AdditiveSceneManager();
OliveEngine.SceneManager = sceneManager;

sceneManager.LoadScene(new CameraScene());
sceneManager.LoadScene(new MonkeyPlaneScene());
sceneManager.LoadScene(new OuterMonkeyScene());

OliveEngine.Run();
