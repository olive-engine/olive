using BehaviorExample;
using Olive;
using DisplayMode = Olive.DisplayMode;

OliveEngine.Initialize("Behavior Example", 800, 600, DisplayMode.Windowed);
OliveEngine.Run();

OliveEngine.SceneManager.LoadScene(new MainScene());
