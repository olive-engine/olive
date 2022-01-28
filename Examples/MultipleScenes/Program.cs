using MultipleScenes;
using Olive;
using Olive.SceneManagement;

OliveEngine.Initialize("Multiple Scenes Example", 800, 600, DisplayMode.Windowed);

var sceneManager = new AdditiveSceneManager();
OliveEngine.SceneManager = sceneManager;
OliveEngine.Run();

sceneManager.LoadScene(new RedScene());
