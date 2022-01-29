using Olive;
using Olive.SceneManagement;

OliveEngine.Initialize("GL Example", 800, 600, DisplayMode.Windowed);
OliveEngine.Run();
SceneManager.LoadScene(new MainScene());