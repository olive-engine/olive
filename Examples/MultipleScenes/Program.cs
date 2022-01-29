using MultipleScenes;
using Olive;
using Olive.SceneManagement;

OliveEngine.Initialize("Multiple Scenes Example", 800, 600, DisplayMode.Windowed);
OliveEngine.Run();
SceneManager.LoadScene(new RedScene());
