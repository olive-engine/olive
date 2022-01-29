using BehaviorExample;
using Olive;

OliveEngine.Initialize("Behavior Example", 800, 600, DisplayMode.Windowed);
OliveEngine.Run();

OliveEngine.SceneManager.LoadScene(new MainScene());
