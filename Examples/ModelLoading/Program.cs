using ModelLoading;
using Olive;

OliveEngine.Initialize("Model Example", 800, 600, DisplayMode.Windowed);
OliveEngine.SceneManager.LoadScene(new MainScene());

OliveEngine.Run();