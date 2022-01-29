using Olive;
using Olive.SceneManagement;
using SceneTransformExample;

OliveEngine.Initialize("Scene Transform Example", 800, 600, DisplayMode.Windowed);
OliveEngine.Run();

SceneManager.LoadScene(new CameraScene(), SceneLoadMode.Additive);
SceneManager.LoadScene(new MonkeyPlaneScene(), SceneLoadMode.Additive);
SceneManager.LoadScene(new OuterMonkeyScene(), SceneLoadMode.Additive);
